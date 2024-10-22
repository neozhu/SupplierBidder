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
using Microsoft.Ajax.Utilities;
using AutoMapper;

namespace WebApp.Services
{
  /// <summary>
  /// File: InventoryService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 2020/9/3 10:07:21
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class InventoryService : Service<Inventory>, IInventoryService
  {
    private readonly IRepositoryAsync<Inventory> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IAllocateService allocateService;
    private readonly NLog.ILogger logger;
    private readonly IMapper mapper;
    private readonly IOutboundService outboundService;
    public InventoryService(
       IMapper mapper,
       IOutboundService outboundService,
      IRepositoryAsync<Inventory> repository,
      IDataTableImportMappingService mappingservice,
      IAllocateService allocateService,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.outboundService = outboundService;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.allocateService = allocateService;
      this.logger = logger;
      this.mapper = mapper;
    }



    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "Inventory" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到Inventory对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
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
          var item = new Inventory();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var inventorytype = item.GetType();
              var propertyInfo = inventorytype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var inventorytype = item.GetType();
              var propertyInfo = inventorytype.GetProperty(field.FieldName);
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
             .Where(x => x.EntitySetName == "Inventory")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var inventories = this.Query(new InventoryQuery().Withfilter(false,filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = inventories.Select(n => new
      {

        Id = n.Id,
        PO = n.PO,
        LineNum = n.LineNum,
        PODate = n.PODate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Receiver = n.Receiver,
        Status = n.Status,
        InvStatus = n.InvStatus,
        OutboundDate = n.OutboundDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ProductName = n.ProductName,
        Spec = n.Spec,
        BrandName = n.BrandName,
        Unit = n.Unit,
        Qty = n.Qty,
        ControlledPrice = n.ControlledPrice,
        Feature = n.Feature,
        Description = n.Description,
        Buyer = n.Buyer,
        BuyerPhone = n.BuyerPhone,
        BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ProductNo = n.ProductNo,
        ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        SO = n.SO,
        InvoiceNo = n.InvoiceNo,
        OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        BiddingTime = n.BiddingTime,
        BiddingUsers = n.BiddingUsers,
        MinPrice = n.MinPrice,
        MaxPrice = n.MaxPrice,
        BidedPrice = n.BidedPrice,
        SupplierName = n.SupplierName,
        DocNo = n.DocNo,
        ClosedDate = n.ClosedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Category = n.Category,
        Dept = n.Dept,
        Section = n.Section,
        GrantNo = n.GrantNo,
        Reason = n.Reason
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("Inventory", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task Outbound(outboundviewmodel[] data, string v)
    {
      var now = DateTime.Now;
      foreach (var item in data)
      {
        var inv = await this.FindAsync(item.InvId);
        if (inv.Qty < item.OutboundQty)
        {
          throw new Exception($"{inv.PO}-{inv.LineNum} 库存数量 {inv.Qty} < 申请数量 {item.OutboundQty}");
        }
        inv.Qty = inv.Qty - item.OutboundQty;
        if (inv.Qty > 0)
        {
          inv.InvStatus = "部分出库";
        }
        else
        {
          inv.InvStatus = "已出库";
        }
        inv.OutboundDate = now;
        this.Update(inv);
        var outbound = this.mapper.Map<Outbound>(inv);
        outbound.Qty = item.OutboundQty;
        outbound.StockQty = inv.Qty;
        outbound.RecordUser =  string.IsNullOrEmpty(item.RecordUser)?v: item.RecordUser;
        outbound.OuboundDate = item.OutboundDate==null?now:item.OutboundDate.Value;
        outbound.Remark = item.Remark;
        this.outboundService.Insert(outbound);
      }
    }

    public async Task<(IEnumerable<PurchaseOrder>, IEnumerable<Outbound>,IEnumerable<Allocate>)> GetHistories(int id)
    {
      var inv = await this.FindAsync(id);
      var poid = inv.PurchaseOrderId;
      var inbound =await this.repository.GetRepositoryAsync<PurchaseOrder>()
        .Queryable()
        .Where(x => x.Id == poid)
        .ToListAsync();
      var outbound = await this.outboundService.Queryable()
        .Where(x => x.PurchaseOrderId == poid)
        .ToListAsync();

      var allocate = await this.allocateService.Queryable()
       .Where(x => x.PurchaseOrderId == poid)
       .ToListAsync();
      return (inbound, outbound, allocate);
    }

    public async Task Allocated(AllocateDto[] request, string v) {
      var date = DateTime.Now;
      foreach(var dto in request)
      {
        var item =await this.FindAsync(dto.InvId);
        item.Qty = item.Qty - dto.AllocateQty;
        item.OutboundDate = date;
        if (item.Qty > 0)
        {
          item.InvStatus = "部分出库";
        }
        else
        {
          item.InvStatus = "已出库";
        }
        this.Update(item);
        var allocate = this.mapper.Map<Allocate>(item);
        allocate.Qty = dto.AllocateQty;
        allocate.RefId = dto.RefId;
        allocate.Remark = dto.ProductName;
        allocate.Status = "待确认";
        allocate.RecordUser = v;
        allocate.OuboundDate = date;
        allocate.StockQty = item.Qty;
        allocate.Loc = dto.Loc;
        allocate.ProductInfo = dto.ProductName;
        this.allocateService.Insert(allocate);

      }
    }

   
  }
}



