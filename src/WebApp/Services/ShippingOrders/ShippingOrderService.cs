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
/// File: ShippingOrderService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 3/10/2020 9:40:42 AM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class ShippingOrderService : Service< ShippingOrder >, IShippingOrderService
    {
        private readonly IRepositoryAsync<ShippingOrder> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  ShippingOrderService(
          IRepositoryAsync< ShippingOrder> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                  
        
        		 
                public async Task ImportDataTableAsync(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "ShippingOrder" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到ShippingOrder对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
            }
            foreach (DataRow row in datatable.Rows)
            {
                
                var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled==true && x.DefaultValue==null).FirstOrDefault()?.SourceFieldName;
                if (requiredfield != null || !row.IsNull(requiredfield))
                {
                    var item = new ShippingOrder();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain && !row.IsNull(field.SourceFieldName) )
						{
                            var shippingordertype = item.GetType();
							var propertyInfo = shippingordertype.GetProperty(field.FieldName);
                            							        var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                    var safeValue = (row[field.SourceFieldName] == null) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
                                    propertyInfo.SetValue(item, safeValue, null);
						                            }
						else if (!string.IsNullOrEmpty(defval))
						{
							var shippingordertype = item.GetType();
							var propertyInfo = shippingordertype.GetProperty(field.FieldName);
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
                   .Where(x => x.EntitySetName == "ShippingOrder")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var shippingorders  = this.Query(new ShippingOrderQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).Select().ToList();
            var datarows = shippingorders .Select(  n => new { 

    Id = n.Id,
    SO = n.SO,
    Status = n.Status,
    ShippingDate = n.ShippingDate.ToString("yyyy-MM-dd HH:mm:ss"),
    DeliveryAddress = n.DeliveryAddress,
    Buyer = n.Buyer,
    BuyerPhone = n.BuyerPhone,
    Packages = n.Packages,
    TotalAmount = n.TotalAmount,
    InvoiceNo = n.InvoiceNo,
    InvoiceAmount = n.InvoiceAmount,
    TaxRate = n.TaxRate,
    Tax = n.Tax,
    Remark = n.Remark,
    ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    ClosedDate = n.ClosedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    UserName = n.UserName,
    SupplierId = n.SupplierId,
    SupplierName = n.SupplierName
}).ToList();
            return await NPOIHelper.ExportExcelAsync("ShippingOrder", datarows,expcolopts);
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



