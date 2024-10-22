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

namespace WebApp.Services
{
/// <summary>
/// File: TenderService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 3/7/2020 9:41:46 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class TenderService : Service< Tender >, ITenderService
    {
        private readonly IRepositoryAsync<Tender> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  TenderService(
          IRepositoryAsync< Tender> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                 public async  Task<IEnumerable<Tender>> GetByPurchaseOrderIdAsync(int  purchaseorderid) => await repository.GetByPurchaseOrderIdAsync(purchaseorderid);
                  public async  Task<IEnumerable<Tender>> GetBySupplierIdAsync(int  supplierid) => await repository.GetBySupplierIdAsync(supplierid);
                   
        
        		 
                private async Task<int> getPurchaseOrderIdByPOAsync(string po)
        {
            var purchaseorderRepository = this.repository.GetRepositoryAsync<PurchaseOrder>();
            var purchaseorder = await  purchaseorderRepository.Queryable().Where(x => x.PO == po).FirstOrDefaultAsync();
            if (purchaseorder == null)
            {
                throw new Exception("not found ForeignKey:PurchaseOrderId with " + po);
            }
            else
            {
                return purchaseorder.Id;
            }
        }
                private async Task<int> getSupplierIdByNameAsync(string name)
        {
            var companyRepository = this.repository.GetRepositoryAsync<Company>();
            var company = await  companyRepository.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
            if (company == null)
            {
                throw new Exception("not found ForeignKey:SupplierId with " + name);
            }
            else
            {
                return company.Id;
            }
        }
                public async Task ImportDataTableAsync(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "Tender" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到Tender对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
            }
            foreach (DataRow row in datatable.Rows)
            {
                
                var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled==true && x.DefaultValue==null).FirstOrDefault()?.SourceFieldName;
                if (requiredfield != null || !row.IsNull(requiredfield))
                {
                    var item = new Tender();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain && !row.IsNull(field.SourceFieldName) )
						{
                            var tendertype = item.GetType();
							var propertyInfo = tendertype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "PurchaseOrderId":
                                     var po =  row[field.SourceFieldName].ToString();
                                     var purchaseorderid = await this.getPurchaseOrderIdByPOAsync(po);
                                     propertyInfo.SetValue(item, Convert.ChangeType(purchaseorderid, propertyInfo.PropertyType), null);
                                     break;
                                                                case "SupplierId":
                                     var name =  row[field.SourceFieldName].ToString();
                                     var supplierid = await this.getSupplierIdByNameAsync(name);
                                     propertyInfo.SetValue(item, Convert.ChangeType(supplierid, propertyInfo.PropertyType), null);
                                     break;
                                                                default:
                                    var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                    var safeValue = (row[field.SourceFieldName] == null) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
                                    propertyInfo.SetValue(item, safeValue, null);
                                    break;
                            }
                                                    }
						else if (!string.IsNullOrEmpty(defval))
						{
							var tendertype = item.GetType();
							var propertyInfo = tendertype.GetProperty(field.FieldName);
							if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && (propertyInfo.PropertyType ==typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>)))
                            {
                                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                                propertyInfo.SetValue(item, safeValue, null);
                            }
                            else if(string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
                            {
                                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
                            }
                            else if(string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
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
				public async Task<Stream> ExportExcelAsync(string filterRules = "",string sort = "Id", string order = "asc")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            var expcolopts= await this.mappingservice.Queryable()
                   .Where(x => x.EntitySetName == "Tender")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var tenders  = await this.Query(new TenderQuery().Withfilter(filters)).Include(p => p.PurchaseOrder).Include(p => p.Supplier).OrderBy(n=>n.OrderBy(sort,order)).SelectAsync();
            
            var datarows = tenders .Select(  n => new { 

    PurchaseOrderPO = n.PurchaseOrder?.PO,
    SupplierName = n.Supplier?.Name,
    Id = n.Id,
    DocNo = n.DocNo,
    PurchaseOrderId = n.PurchaseOrderId,
    SupplierId = n.SupplierId
}).ToList();
            return await NPOIHelper.ExportExcelAsync("Tender", datarows,expcolopts);
        }
        public void Delete(int[] id) {
            var items = this.Queryable().Where(x => id.Contains(x.Id));
            foreach (var item in items)
            {
               this.Delete(item);
            }

        }
    }
}



