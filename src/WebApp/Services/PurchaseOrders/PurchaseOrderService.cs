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
using AutoMapper;
using WebApp.App_Helpers.third_party.api;
using System.Data.Entity.SqlServer;

namespace WebApp.Services
{
  /// <summary>
  /// File: PurchaseOrderService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 3/7/2020 7:44:50 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class PurchaseOrderService : Service<PurchaseOrder>, IPurchaseOrderService
  {
    private readonly IRepositoryAsync<PurchaseOrder> repository;
    private readonly INotificationService notificationService;
    private readonly ICompanyService companyService;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IBiddingService biddingService;
    private readonly ITenderService tenderService;
    private readonly NLog.ILogger logger;
    private readonly IMapper mapper;
    private readonly IShippingOrderService shippingOrderService;
    private readonly IContactService contactService;
    private readonly ICategoryAllocationService categoryAllocationService;
    private readonly IInventoryService inventoryService;
    public PurchaseOrderService(
      IInventoryService inventoryService,
      ICategoryAllocationService categoryAllocationService,
      ICompanyService companyService,
      INotificationService notificationService,
      IRepositoryAsync<PurchaseOrder> repository,
      IDataTableImportMappingService mappingservice,
      ITenderService tenderService,
      IBiddingService biddingService,
      IShippingOrderService shippingOrderService,
      IContactService contactService,
      IMapper mapper,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.inventoryService = inventoryService;
      this.categoryAllocationService = categoryAllocationService;
      this.mapper = mapper;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
      this.tenderService = tenderService;
      this.biddingService = biddingService;
      this.companyService = companyService;
      this.shippingOrderService = shippingOrderService;
      this.contactService = contactService;
      this.notificationService = notificationService;
    }
    public async Task<IEnumerable<Tender>> GetTendersByPurchaseOrderIdAsync(int purchaseorderid) => await repository.GetTendersByPurchaseOrderIdAsync(purchaseorderid);



    public async Task ImportDataTableAsync(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "PurchaseOrder" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到PurchaseOrder对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      var list = new List<PurchaseOrder>();
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null || !row.IsNull(requiredfield))
        {
          var item = new PurchaseOrder();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName))
            {
              var purchaseordertype = item.GetType();
              var propertyInfo = purchaseordertype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var purchaseordertype = item.GetType();
              var propertyInfo = purchaseordertype.GetProperty(field.FieldName);
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
          if (string.IsNullOrEmpty(item.BuyerPhone))
          {
            item.BuyerPhone = ( await this.GetContact(item.Buyer) )?.PhoneNumber;
          }
          list.Add(item);
        }
      }
      if (list.Count > 0)
      {
        
            this.InsertRange(list);
        
        
      }
    }

    public async Task ImportDataTableAsync1(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "PurchaseOrder" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到PurchaseOrder对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      var list = new List<PurchaseOrder>();
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null || !row.IsNull(requiredfield))
        {
          var item = new PurchaseOrder();
          item.BidedPrice = Convert.ToDecimal(row["实际价格"] ?? 0);
          item.SupplierName = row["供应商"].ToString();
          item.Buyer = row["采购员说明"].ToString();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain && !row.IsNull(field.SourceFieldName))
            {
              var purchaseordertype = item.GetType();
              var propertyInfo = purchaseordertype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = ( row[field.SourceFieldName] == null ) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var purchaseordertype = item.GetType();
              var propertyInfo = purchaseordertype.GetProperty(field.FieldName);
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
          if (string.IsNullOrEmpty(item.BuyerPhone))
          {
            item.BuyerPhone = ( await this.GetContact(item.Buyer) )?.PhoneNumber;
          }
          item.Status = "待收货";
          //item.BidedPrice = item.ControlledPrice;
          list.Add(item);
        }
      }
      if (list.Count > 0)
      {
        
            this.InsertRange(list);
         
      }
    }
    private async Task<Contact> GetContact(string name)
    {
      return await this.contactService.Queryable().Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<Stream> ExportExcelAsync(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var expcolopts = await this.mappingservice.Queryable()
             .Where(x => x.EntitySetName == "PurchaseOrder")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var purchaseorders = this.Query(new PurchaseOrderQuery().Withfilter(filters)).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
      var datarows = purchaseorders.Select(n => new
      {

        Id = n.Id,
        PO = n.PO,
        LineNum = n.LineNum,
        PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
        BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Status = n.Status,
        DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ProductNo = n.ProductNo,
        ProductName = n.ProductName,
        Spec = n.Spec,
        Feature = n.Feature,
        BrandName = this.getbidBrandName(n.Id),
        Unit = n.Unit,
        Qty = n.Qty,
        BidedPrice = n.BidedPrice,
        ControlledPrice = ( n.Qty * n.BidedPrice ).ToString(),
        Description = n.Description,
        Buyer = n.Buyer,
        Reason = n.Reason,
        SupplierName = n.SupplierName,
        BuyerPhone = n.BuyerPhone,
        DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        SO = n.SO,
        InvoiceNo = n.InvoiceNo,
        InvoiceAmount = n.InvoiceAmount,
        ClosedDate = n.ClosedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        BiddingTime = n.BiddingTime,
        BiddingUsers = n.BiddingUsers,
        MinPrice = n.MinPrice,
        MaxPrice = n.MaxPrice,
        ReceiptQty=n.ReceiptQty,
        OpenQty=n.OpenQty,
        Dept = n.Dept,
        Section = n.Section,
        GrantNo = n.GrantNo,
        Category=n.Category,


      }).ToList();
      return await NPOIHelper.ExportExcelAsync("PurchaseOrder", datarows, expcolopts);
    }
    private string getbidBrandName(int id)
    {
      var arr = new string[] { "中标", "已确认", "发货中", "结案中", "已结案" };
      var bid = this.biddingService.Queryable()
        .Where(x => x.PurchaseOrderId == id && arr.Contains(x.Status)).FirstOrDefault();
      return bid?.BrandName;

    }
    public void Delete(int[] id)
    {
      var items = this.Queryable().Where(x => id.Contains(x.Id));
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task DoBidding(BiddingViewModel viewmodel, string user)
    {
      var items = await this.Queryable().Where(x => viewmodel.PurcshaseOrderId.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        //如果 改采购类别还没有维护供应商的情况报错
        var exist = await this.categoryAllocationService.Queryable()
             .Where(x => x.CategoryName == item.Category)
             .AnyAsync();
        if (!exist)
        {
          throw new Exception($"采购类型:[ {item.Category} ] 还没分配供应商，无法完成发标. ");
        }
        item.DocNo = viewmodel.DocNo;
        item.Status = "发标中";
        item.BiddingDate = viewmodel.BiddingDate;
        item.DueDate = viewmodel.DueDate;
        this.Update(item);
        this.logger.Info($"{item.PO}-{item.LineNum} 发标完成.");
        foreach (var sid in viewmodel.SupplierId)
        {
          var supplier = await this.companyService.FindAsync(sid);
          //判断供应商是否有选取投标
          var any = await this.categoryAllocationService.Queryable()
            .Where(x => x.CategoryName == item.Category && x.CompanyId == sid)
            .AnyAsync();
          if (any)
          {
            var tender = new Tender()
            {
              SupplierId = sid,
              DocNo = viewmodel.DocNo,
              PurchaseOrderId = item.Id,
              Category = item.Category
            };
            this.tenderService.Insert(tender);
          }
          //运价发布短信通知
          //var result = zhixinHelper.Send(supplier.PhoneNumber, "TP20032512");
          //await this.notificationService.AddSMS(supplier.PhoneNumber, "TP20032512" + " 结果:" + result, "发标通知", user);
        }

      }
      var allow = Dashboard.Instance.SMS();
      if (allow > 0)
      {


        foreach (var sid in viewmodel.SupplierId)
        {
          var supplier = await this.companyService.FindAsync(sid);
          //if (!this.notificationService.Queryable().Where(x => x.To == supplier.PhoneNumber && SqlFunctions.DateDiff("day", x.Created, DateTime.Now) == 0).Any())
          //{
          //运价发布短信通知
          var result = zhixinHelper.Send(supplier.PhoneNumber, "TP21071531");
          await this.notificationService.AddSMS($"{supplier.Name}:{supplier.PhoneNumber}", "短信模板:TP21071531" + " 结果:" + result, "发标通知", user);
          //}
        }

      }

    }

    public async Task<BidViewModel> GetBidViewModel(int id, string username, int companyid)
    {

      var item = await this.FindAsync(id);
      var company = await this.companyService.FindAsync(companyid);
      var viewmodel = this.mapper.Map<BidViewModel>(item);
      viewmodel.B_BiddingPrice = 0;
      viewmodel.B_BrandName = item.BrandName;
      viewmodel.B_Buyer = item.Buyer;
      viewmodel.B_BuyerPhone = item.BuyerPhone;
      viewmodel.B_ProductName = item.ProductName;
      viewmodel.B_Spec = item.Spec;
      viewmodel.B_Feature = item.Feature;
      viewmodel.B_Description = item.Description;
      viewmodel.B_Qty = item.Qty;
      viewmodel.B_Unit = item.Unit;
      viewmodel.B_SupplierId = companyid;
      viewmodel.B_SupplierName = company.Name;
      viewmodel.B_UserName = username;
      return viewmodel;
    }
    public async Task Bid(BidRequestViewModel request)
    {
      var order = await this.FindAsync(request.PurchaseOrderId);
      var dueDate = order.DueDate;
      if (dueDate < DateTime.Now)
      {
        throw new Exception("该采购单询已结束, 不允许再投标.");
      }
      var company = await this.companyService.FindAsync(request.SupplierId);
      var bids = await this.biddingService.GetPreviousBids(request.PurchaseOrderId, request.SupplierId);
      foreach (var bid in bids)
      {
        //bid.Status = "作废";
        //bid.Description = "作废";
        bid.Status = "废标";
        bid.Description = "作废,并重新投标";
        this.biddingService.Update(bid);
      }

      var bidding = this.mapper.Map<Bidding>(order);
      bidding.Status = "待开标";
      bidding.Description = request.Description;
      bidding.PurchaseOrderId = request.PurchaseOrderId;
      bidding.BiddingDate = DateTime.Now;
      bidding.BiddingPrice = request.BiddingPrice;
      bidding.BrandName = request.BrandName;
      bidding.Feature = request.Feature;
      bidding.Spec = request.Spec;
      bidding.SupplierId = company.Id;
      bidding.SupplierName = company.Name;
      bidding.TotalPrice = request.TotalPrice;
      bidding.UserName = request.UserName;
      bidding.DeliveryCycle = request.DeliveryCycle;
      this.biddingService.Insert(bidding);
      this.logger.Info($"{order.PO}-{order.LineNum} 投标价:${request.BiddingPrice}");
    }


    public async Task OutBid(int purchaseorderid, int bidid, string reason, string user)
    {
      var order = await this.FindAsync(purchaseorderid);
      var bid = await this.biddingService.FindAsync(bidid);
      bid.Status = "中标";
      order.Status = "中标待确认";
      order.SupplierName = bid.SupplierName;
      order.BidedPrice = bid.BiddingPrice;
      order.Reason = reason;
      order.OpenDate = DateTime.Now;
      this.Update(order);
      this.biddingService.Update(bid);
      var notbid = await this.biddingService.Queryable()
        .Where(x => x.Id != bidid
        && x.PurchaseOrderId == purchaseorderid
        && x.Status != "作废"
        && !x.Status.Contains("废标"))
        .ToListAsync();
      foreach (var item in notbid)
      {
        item.Status = "出局";
        this.biddingService.Update(item);
      }
      var allow = Dashboard.Instance.SMS();
      if (allow > 0)
      {
        var supplier = await this.companyService.FindAsync(bid.SupplierId);
        //中标短信通知
        var result = zhixinHelper.Send(supplier.PhoneNumber, "TP21071530");
        await this.notificationService.AddSMS(supplier.PhoneNumber, "TP21071530" + " 结果:" + result, "中标通知", user);
      }
    }

    public async Task ConfirmBid(int[] bidid)
    {
      var bids = await this.biddingService.Queryable()
        .Where(x => bidid.Contains(x.Id)).ToListAsync();
      foreach (var bid in bids)
      {
        if (bid.Status == "废标")
        {
          bid.Status = "废标已确认";
          this.biddingService.Update(bid);
          var order = await this.FindAsync(bid.PurchaseOrderId);
          order.Status = "废标已确认";
          this.Update(order);
        }
        else
        {
          bid.Status = "已确认";
          this.biddingService.Update(bid);
          var order = await this.FindAsync(bid.PurchaseOrderId);
          //order.Status = "发货中";
          order.Status = "待发货";
          this.Update(order);
        }

      }
    }

    public async Task ConfirmRejectBid(int[] bidid, string reason)
    {
      var bids = await this.biddingService.Queryable()
        .Where(x => bidid.Contains(x.Id)).ToListAsync();
      foreach (var bid in bids)
      {

        bid.Status = "拒绝";
        bid.Description = reason;
        this.biddingService.Update(bid);
        var order = await this.FindAsync(bid.PurchaseOrderId);
        //order.Status = "发货中";
        order.Status = "开标中";
        this.Update(order);
        var others = await this.biddingService.Queryable().Where(x => x.PurchaseOrderId == bid.PurchaseOrderId && x.Id != bid.Id && x.Status== "出局").ToListAsync();
        foreach(var item in others)
        {
          item.Status = "待开标";
          this.biddingService.Update(item);
        }


      }
    }
    public async Task BidedSumarry(int id)
    {
      var item = await this.FindAsync(id);
      var times = await this.biddingService.Queryable().Where(x => x.PurchaseOrderId == id
        && x.Status != "作废"
        && !x.Status.Contains("废标"))
        .CountAsync();
      var users = await this.biddingService.Queryable().Where(x => x.PurchaseOrderId == id
        && x.Status != "作废"
        && !x.Status.Contains("废标"))
            .GroupBy(x => x.SupplierId).CountAsync();
      var min = await this.biddingService.Queryable().Where(x => x.PurchaseOrderId == id
        && x.Status != "作废"
        && !x.Status.Contains("废标"))
            .MinAsync(x => x.BiddingPrice);
      var max = await this.biddingService.Queryable().Where(x => x.PurchaseOrderId == id
        && x.Status != "作废"
        && !x.Status.Contains("废标"))
            .MaxAsync(x => x.BiddingPrice);

      item.BiddingTime = times;
      item.BiddingUsers = users;
      item.MinPrice = min;
      item.MaxPrice = max;
      this.Update(item);
    }


    public async Task CreateShippingOrder(SORequestViewModel viewmodel)
    {
      var so = this.mapper.Map<ShippingOrder>(viewmodel);
      this.shippingOrderService.Insert(so);
      var bids = await this.biddingService.Queryable()
        .Where(x => viewmodel.BiddingId.Contains(x.Id))
        .ToListAsync();
      foreach (var bid in bids)
      {
        bid.Status = "已发货";
        bid.SO = so.SO;
        bid.ShippingDate = so.ShippingDate;
        this.biddingService.Update(bid);

        var po = await this.FindAsync(bid.PurchaseOrderId);
        po.Status = "已发货";
        po.SO = so.SO;
        po.ShippingDate = so.ShippingDate;
        this.Update(po);



      }
    }

    public async Task Shipped(int[] soid)
    {
      var sorders = await this.shippingOrderService.Queryable()
        .Where(x => soid.Contains(x.Id)).ToListAsync();
      foreach (var order in sorders)
      {
        order.Status = "已发货";
        this.shippingOrderService.Update(order);
        var bids = await this.biddingService.Queryable()
            .Where(x => x.SO == order.SO).ToListAsync();
        foreach (var bid in bids)
        {
          bid.Status = "发货中";
          bid.ShippingDate = order.ShippingDate;
          this.biddingService.Update(bid);
        }
        var poid = await this.biddingService.Queryable()
          .Where(x => x.SO == order.SO)
          .Select(x => x.PurchaseOrderId)
          .ToArrayAsync();
        var porders = await this.Queryable().Where(x => poid.Contains(x.Id))
          .ToListAsync();
        foreach (var po in porders)
        {
          po.Status = "发货中";
          po.SO = order.SO;
          po.ShippingDate = order.ShippingDate;
          po.InvoiceNo = order.InvoiceNo;
          this.Update(po);
        }
      }

    }
    //收货 添加入库功能;
    public async Task Received(int[] soid, DateTime date, string user)
    {
      var now = DateTime.Now;
      var sorders = await this.shippingOrderService.Queryable()
        .Where(x => soid.Contains(x.Id)).ToListAsync();
      foreach (var order in sorders)
      {
        order.Status = "结案中";
        order.ReceivedDate = date;
        this.shippingOrderService.Update(order);
        var bids = await this.biddingService.Queryable()
            .Where(x => x.SO == order.SO).ToListAsync();
        foreach (var bid in bids)
        {
          bid.Status = "结案中";
          bid.ShippingDate = order.ShippingDate;
          bid.ReceivedDate = date;
          this.biddingService.Update(bid);
          var pid = bid.PurchaseOrderId;
          var po = await this.FindAsync(pid);
          po.Status = "结案中";
          po.SO = order.SO;
          po.ShippingDate = order.ShippingDate;
          po.ReceivedDate = date;
          this.Update(po);
          var exist = await this.inventoryService.Queryable()
            .Where(x => x.PO == po.PO && x.LineNum == po.LineNum)
            .AnyAsync();
          if (exist)
          {
            throw new Exception($"{po.PO}-{po.LineNum} {po.ProductName} 已经入库,不允许重复操作");
          }
          var inv = this.mapper.Map<Inventory>(po);
          inv.Receiver = user;
          inv.BrandName = bid.BrandName;
          inv.InvStatus = "正常";
          inv.PurchaseOrderId = po.Id;
          this.inventoryService.Insert(inv);
        }

        //var poid = await this.biddingService.Queryable()
        //  .Where(x => x.SO == order.SO)
        //  .Select(x => x.PurchaseOrderId)
        //  .ToArrayAsync();
        //var porders = await this.Queryable().Where(x => poid.Contains(x.Id))
        //  .ToListAsync();
        //foreach (var po in porders)
        //{


        //}
      }

    }

    public async Task Closed(int[] soid)
    {
      var now = DateTime.Now;
      var sorders = await this.shippingOrderService.Queryable()
        .Where(x => soid.Contains(x.Id)).ToListAsync();
      foreach (var order in sorders)
      {
        order.Status = "已结案";
        order.ClosedDate = now;
        this.shippingOrderService.Update(order);
        var bids = await this.biddingService.Queryable()
            .Where(x => x.SO == order.SO).ToListAsync();
        foreach (var bid in bids)
        {
          bid.Status = "已结案";
          bid.ShippingDate = order.ShippingDate;
          bid.ClosedDate = now;
          this.biddingService.Update(bid);
        }

        var poid = await this.biddingService.Queryable()
          .Where(x => x.SO == order.SO)
          .Select(x => x.PurchaseOrderId)
          .ToArrayAsync();
        var porders = await this.Queryable().Where(x => poid.Contains(x.Id))
          .ToListAsync();
        foreach (var po in porders)
        {
          po.Status = "已结案";
          po.SO = order.SO;
          po.ShippingDate = order.ShippingDate;
          po.ClosedDate = now;
          this.Update(po);
        }
      }

    }

    public async Task DeleteShippingOrders(int[] id)
    {
      var sorders = await this.shippingOrderService.Queryable()
        .Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var order in sorders)
      {
        var bids = await this.biddingService.Queryable()
          .Where(x => x.SO == order.SO).ToListAsync();
        foreach (var bid in bids)
        {
          bid.Status = "已确认";
          bid.SO = null;
          bid.ShippingDate = null;
          this.biddingService.Update(bid);
        }

        this.shippingOrderService.Delete(order);
      }
    }
    public async Task ChagneDueDate(int id, DateTime duedate)
    {
      var item = await this.FindAsync(id);
      item.DueDate = duedate;
      if (duedate <= DateTime.Now)
      {
        item.Status = "开标中";
      }
      else
      {
        item.Status = "发标中";
      }
      this.Update(item);


    }

    //废标，如果已投需要供应商待确认
    public async Task Destroy(int[] id, string reason)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      var num = 0;
      foreach (var item in items)
      {
        var bids = await this.biddingService
          .Queryable()
          .Where(x => x.PurchaseOrderId == item.Id && x.Status != "作废")
          .ToListAsync();
        if (bids.Any())
        {
          num = bids.Count;
          //item.Status = "废标待确认"; 新需求直接废标 后从新发标
          item.Status = "废标";
          foreach (var bid in bids)
          {
            bid.Status = "废标";
            bid.Description = reason;
            this.biddingService.Update(bid);
          }
        }
        else
        {
          item.Status = "废标";

        }
        item.Status = "废标";
        item.Reason = reason;
        item.BiddingUsers = 0;
        item.BiddingTime = 0;
        item.MaxPrice = 0;
        item.MinPrice = 0;
        item.DueDate = null;
        this.Update(item);
        this.logger.Info($"{item.PO}-{item.LineNum}-竞标人数:{num} 废标!理由:{reason}");
        //删除以前的投标分配供应商的记录
        var tenders = await this.tenderService.Queryable().Where(x => x.PurchaseOrderId == item.Id).ToListAsync();
        foreach (var ten in tenders)
        {
          this.tenderService.Delete(ten);
        }
      }

    }

    //重新发标
    public async Task ReSend(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();


      foreach (var item in items)
      {

        item.Status = "待处理";
        this.Update(item);

      }

    }

    public async Task DeleteReceiptData(int[] id) {
      var items =await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach(var item in items)
      {
        if(item.Status=="待收货" && (item.ReceiptQty==null || item.ReceiptQty==0 )){
          this.Delete(item);
        }
        else
        {
          throw new Exception($"{item.PO}/{item.LineNum}:不允许删除已收货和询价的采购单记录");
        }
      }
    }

    public async Task Receipt(ReceiptDto[] request,string username) {
      foreach(var dto in request)
      {
        var datetime = DateTime.Now;
        var item =await this.FindAsync(dto.PurchaseOrderId);
        //var openqty = item.OpenQty == null ? item.Qty : item.OpenQty;
        var receiptqty = item.ReceiptQty==null? 0m:item.ReceiptQty;
        item.ReceiptQty = receiptqty + dto.ReceiptQty;
        item.OpenQty = item.Qty - item.ReceiptQty;

        if (item.OpenQty <= 0 && string.IsNullOrEmpty(item.SO))
        {
          item.SO = dto.SO;
        }
        if (item.OpenQty <= 0 && string.IsNullOrEmpty(item.SupplierName))
        {
          item.SO = dto.SO;
          item.SupplierName = dto.SupplierName;
        }
        if(item.OpenQty <= 0)
        {
          item.Status = "结案中";
        }
        item.ReceivedDate = datetime;
        this.Update(item);
        var inbound = this.mapper.Map<Inventory>(item);
        inbound.SO = dto.SO;
        inbound.SupplierName = dto.SupplierName;
        inbound.PurchaseOrderId = item.Id;
        inbound.InvStatus = "正常";
        inbound.Qty = dto.ReceiptQty;
        this.inventoryService.Insert(inbound);
       

      }


      }

    public async Task ConfirmClose(CloseRequestDto request) {
      var id = request.PurchaseOrderId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x=>Convert.ToInt32(x)).ToArray();
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach(var item in items)
      {
        item.InvoiceAmount = request.InvoiceAmount;
        item.InvoiceNo = request.InvoiceNo;
        item.Status = "已结案";
        this.Update(item);
          
      }
      }
  }
}



