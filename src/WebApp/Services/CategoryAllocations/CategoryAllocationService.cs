using System;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Models.ViewModel;

namespace WebApp.Services
{
  /// <summary>
  /// File: CategoryAllocationService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/7/20 13:32:25
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class CategoryAllocationService : Service<CategoryAllocation>, ICategoryAllocationService
  {
    private readonly ICompanyService companyService;
    private readonly ICategoryService categoryService;
    private readonly IRepositoryAsync<CategoryAllocation> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly NLog.ILogger logger;
    public CategoryAllocationService(
       ICompanyService companyService,
       ICategoryService categoryService,
      IRepositoryAsync<CategoryAllocation> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
      this.categoryService = categoryService;
      this.companyService = companyService;
    }
    public async Task<IEnumerable<CategoryAllocation>> GetByCategoryIdAsync(int categoryid) => await repository.GetByCategoryIdAsync(categoryid);
    public async Task<IEnumerable<CategoryAllocation>> GetByCompanyIdAsync(int companyid) => await repository.GetByCompanyIdAsync(companyid);



    private async Task<int> getCategoryIdByNameAsync(string name)
    {
      var categoryRepository = this.repository.GetRepositoryAsync<Category>();
      var category = await categoryRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
      if (category == null)
      {
        throw new Exception("not found ForeignKey:CategoryId with " + name);
      }
      else
      {
        return category.Id;
      }
    }
    private async Task<int> getCompanyIdByNameAsync(string name)
    {
      var companyRepository = this.repository.GetRepositoryAsync<Company>();
      var company = await companyRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
      if (company == null)
      {
        throw new Exception("not found ForeignKey:CompanyId with " + name);
      }
      else
      {
        return company.Id;
      }
    }
    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "CategoryAllocation" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到CategoryAllocation对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null ||
              ( !row.IsNull(requiredfield) &&
               !string.IsNullOrEmpty(row[requiredfield].ToString())
              )
            )
        {
          var item = new CategoryAllocation();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var categoryallocationtype = item.GetType();
              var propertyInfo = categoryallocationtype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "CategoryId":
                  var categoryname = row[field.SourceFieldName].ToString();
                  var categoryid = await this.getCategoryIdByNameAsync(categoryname);
                  propertyInfo.SetValue(item, Convert.ChangeType(categoryid, propertyInfo.PropertyType), null);
                  break;
                case "CompanyId":
                  var companyname = row[field.SourceFieldName].ToString();
                  var companyid = await this.getCompanyIdByNameAsync(companyname);
                  propertyInfo.SetValue(item, Convert.ChangeType(companyid, propertyInfo.PropertyType), null);
                  break;
                default:
                  var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                  var safeValue = Convert.ChangeType(row[field.SourceFieldName], safetype);
                  propertyInfo.SetValue(item, safeValue, null);
                  break;
              }
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var categoryallocationtype = item.GetType();
              var propertyInfo = categoryallocationtype.GetProperty(field.FieldName);
              if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && ( propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>) ))
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
              else if (string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
              }
              else if (string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, username, null);
              }
              else
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(defval, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
            }
          }
          this.Insert(item);
        }
      }
    }
    public async Task<Stream> ExportExcelAsync(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var expcolopts = await this.mappingservice.Queryable()
             .Where(x => x.EntitySetName == "CategoryAllocation")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var categoryallocations = await this.Query(new CategoryAllocationQuery().Withfilter(filters)).Include(p => p.Category).Include(p => p.Company).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = categoryallocations.Select(n => new
      {

        CategoryName = n.Category?.Name,
        CompanyName = n.Company?.Name,
        Id = n.Id,
        CategoryId = n.CategoryId,
        CompanyId = n.CompanyId,
        Remark = n.Remark,
        IsDisabled = n.IsDisabled
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("CategoryAllocation", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task Allocation(AllocationRequest request)
    {
      var category =await this.categoryService.FindAsync(request.CategoryId);
      var count = category.AllowSuppliers;
      foreach (var sid in request.SupplierId)
      {
        var exist =await this.Queryable().Where(x => x.CategoryId == category.Id && x.CompanyId == sid).AnyAsync();
        if (!exist)
        {
          count = count + 1;
          var supllier = await this.companyService.FindAsync(sid);

          var allocation = new CategoryAllocation()
          {
            CategoryId = category.Id,
             CategoryName= category.Name,
              CompanyId=supllier.Id,
              CompanyName=supllier.Name
          };
          this.Insert(allocation);
        }
      }
      category.AllowSuppliers = count;
      this.categoryService. Update(category);
    }
  }
}



