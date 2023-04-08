using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Z.EntityFramework.Plus;
using TrackableEntities;
using WebApp.Models;
using WebApp.Services;
using WebApp.Repositories;
using WebApp.Models.ViewModel;
using SqlHelper2;
using Microsoft.Owin.Security.Provider;

namespace WebApp.Controllers
{
  /// <summary>
  /// File: PurchaseOrdersController.cs
  /// Purpose:采购中心/采购单信息
  /// Created Date: 3/7/2020 7:44:53 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<PurchaseOrder>, Repository<PurchaseOrder>>();
  ///    container.RegisterType<IPurchaseOrderService, PurchaseOrderService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("PurchaseOrders")]
  public class PurchaseOrdersController : Controller
  {
    private readonly IPurchaseOrderService purchaseOrderService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly IDatabaseAsync db;
    private readonly NLog.ILogger logger;
    private readonly SqlSugar.ISqlSugarClient sqlclient;
    public PurchaseOrdersController(
          IPurchaseOrderService purchaseOrderService,
          IDatabaseAsync db,
          IUnitOfWorkAsync unitOfWork,
           SqlSugar.ISqlSugarClient sqlclient,
    NLog.ILogger logger
          )
    {
      this.purchaseOrderService = purchaseOrderService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.db = db;
      this.sqlclient = sqlclient;// SqlSugarFactory.CreateSqlSugarClient();
    }
    //GET: PurchaseOrders/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "采购单信息", Order = 1)]
    [Authorize(Roles = "admin,厂商")]
    public ActionResult Index() => this.View();
    [Route("SummaryReport", Name = "汇总报表", Order = 11)]
    public ActionResult SummaryReport() {
      var sql = @"select status,count(1) [count],sum(qty) qty,sum(total) total from(
select id, status, qty, BidedPrice, qty*BidedPrice total from[dbo].[PurchaseOrders]
) t
group by t.status
order by sum(total) desc";
      var list = this.sqlclient.Ado.SqlQuery<SummaryReportViewModel>(sql);
      return View(list);
      }

    //每月汇总中标的采购总价
    [HttpGet]
    public JsonResult GetMonthChartData() {
      var sql = @"select  [year] ,[month] ,count(1) [count],sum(qty) qty,sum(total) total from (
select id, YEAR(OpenDate) [year] ,MONTH(OpenDate) [month], qty, BidedPrice, qty*BidedPrice total from [dbo].[PurchaseOrders]
where BidedPrice > 0 and OpenDate is not null ) t
group by t.[year],t.[month]
order by t.[year],t.[month]";
      var data= this.sqlclient.Ado.SqlQuery<SummaryMonthViewModel>(sql);
      return Json(data, JsonRequestBehavior.AllowGet);
    }

    [Route("BidRecord", Name = "查看投标情况", Order = 1)]
    [Authorize(Roles = "admin,厂商")]
    public ActionResult BidRecord() => this.View();

    [Route("Send", Name = "发标", Order = 1)]
    [Authorize(Roles = "admin,厂商")]
    public ActionResult Send() => this.View();

    [Route("Bidding", Name = "竞标", Order = 2)]
    [Authorize(Roles = "admin,供应商")]
    public async Task<ActionResult> Bidding() {
      var sql = "update  [dbo].[PurchaseOrders] set Status=N'开标中' WHERE DueDate <= GETDATE()  and Status=N'发标中'";
     await  this.db.ExecuteNonQueryAsync(sql);
      return View();
     }
    [Authorize(Roles = "admin,厂商")]
    [Route("Opening", Name = "开标", Order = 3)]
    public async Task<ActionResult> Opening()
    {
      var sql = "update  [dbo].[PurchaseOrders] set Status=N'开标中' WHERE DueDate <= GETDATE()  and Status=N'发标中'";
      await this.db.ExecuteNonQueryAsync(sql);
      return this.View();
    }
    [Authorize(Roles = "admin,厂商")]

    [Route("Opened", Name = "中标情况", Order = 3)]
    public ActionResult Opened() => this.View();
    //删除收货记录
    public async Task<JsonResult> DeleteReceiptData(int[] id)
    {
      try
      {
        await this.purchaseOrderService.DeleteReceiptData(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //采购单收货
    public async Task<JsonResult> Receipt(ReceiptDto[] request)
    {
      try
      {
        var user = Auth.GetFullName();
        await this.purchaseOrderService.Receipt(request, user);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }

    //重新发标
    public async Task<JsonResult> ReSend(int[] id) {
      try
      {
        await this.purchaseOrderService.ReSend(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //获取特殊情况“同价二次竞价”重新发标的供应商
    [HttpGet]
    public JsonResult GetSuppliers(int id) {
      var sql = @"select distinct t1.SupplierId CompanyId,t1.SupplierName CompanyName  from [dbo].[Biddings] t1
          inner join [dbo].[PurchaseOrders] t2 on t1.PurchaseOrderId=t2.Id  
          where t2.Id=@id and
          t1.BiddingPrice=(select min(t3.BiddingPrice)
          from [dbo].[Biddings] t3 where t3.PurchaseOrderId=t1.PurchaseOrderId and
          t3.DocNo = t2.DocNo
          )";
      var result = this.db.ExecuteDataReader<companydto>(sql, new { id = id }, dr =>
      {
        return new companydto()
        {
          CompanyId = dr.GetInt32(0),
          CompanyName = dr.GetString(1)
        };
      });
      return Json(result, JsonRequestBehavior.AllowGet);
      }
    //获取收货人
    public async Task<JsonResult> GetReceiver() {
      var sql = "select [code],[text] from [dbo].[CodeItems] where [codetype]='receiver'";
      var data =await this.sqlclient.Ado.SqlQueryAsync<dynamic>(sql);
      var rows = data.Select(x => new { Name = (string)x.code, PhoneNumber = (string)x.text });
      return Json(rows.FirstOrDefault(), JsonRequestBehavior.AllowGet);
     }
    public async Task<JsonResult> DoBidding(BiddingViewModel viewmodel) {
      try
      {
        await this.purchaseOrderService.DoBidding(viewmodel,Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }


    }
    public JsonResult GetDocNo() {
      var key= KeyGenerator.GetNextDocNo();
      return Json(new { key=key }, JsonRequestBehavior.AllowGet);
     }
    public JsonResult GetSONo()
    {
      var key = KeyGenerator.GetNextSONo();
      return Json(new { key = key }, JsonRequestBehavior.AllowGet);
    }
    //生成出货单
    public async Task<JsonResult> CreateShippingOrder(SORequestViewModel viewmodel) {
      try
      {
        await this.purchaseOrderService.CreateShippingOrder(viewmodel);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //删除出货单
    public async Task<JsonResult> DeleteShippingOrders(int[] id)
    {
      try
      {
        await this.purchaseOrderService.DeleteShippingOrders(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //出货确认
    public async Task<JsonResult> Shipped(int[] id)
    {
      try
      {
        await this.purchaseOrderService.Shipped(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //收货确认
    public async Task<JsonResult> Received(int[] id,DateTime date)
    {
      try
      {
        await this.purchaseOrderService.Received(id,date,Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //结案确认回填发票号
    public async Task<JsonResult> ConfirmClose(CloseRequestDto request)
    {
      try
      {
        await this.purchaseOrderService.ConfirmClose(request);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //结案确认
    public async Task<JsonResult> Closed(int[] id)
    {
      try
      {
        await this.purchaseOrderService.Closed(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get :PurchaseOrders/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDestroyData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().DestroyWithfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         n.Reason,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }


    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().Withfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category=n.Category??"",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         n.Reason,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo??"",
                                         ProductName = n.ProductName??"",
                                         Spec = n.Spec??"",
                                         BrandName = n.BrandName??"",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature??"",
                                         Description = n.Description??"",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    //获取状态是“未发货”和“发货中” 供应商没有发货的中标确认的采购单
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetNoShippingData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithNoShippingDatafilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         n.Reason,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    //获取状态是“待收货”和“已发货” 已发货代表投标采购单供应商已发货
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetReceiveData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithReceiveDatafilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         n.Reason,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         OpenQty=n.OpenQty,
                                         ReceiptQty=n.ReceiptQty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }


    //获取状态是“结案中”和“已结案”  财务结案操作
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetCloseData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithCloseDatafilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         n.Reason,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         OpenQty = n.OpenQty,
                                         ReceiptQty = n.ReceiptQty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }



    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetBidRecordData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithBidRecordfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category=n.Category??"",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         DocNo = n.DocNo,
                                         n.Reason,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }


    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetSendData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithSendfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         DocNo = n.DocNo,
                                         n.Reason,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo ?? "",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetBiddingData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithBiddingfilter(filters,Auth.GetCompanyId()))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         Dept = n.Dept ?? "",
                                         Section = n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo??"",
                                         ProductName = n.ProductName??"",
                                         Spec = n.Spec??"",
                                         BrandName = n.BrandName??"",
                                         Unit = n.Unit??"",
                                         n.Reason,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature??"",
                                         Description = n.Description??"",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName,
                                         Bidedate= this.GetLastBiddingDate(n.Id, Auth.GetCompanyId()),
                                         BiddingPrice = this.GetLastBiddingPrice(n.Id,Auth.GetCompanyId())
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    private decimal GetLastBiddingPrice(int purchaseorderid, int supplierid) {
      var query = this.unitOfWork.RepositoryAsync<Bidding>();
      var result =query.Queryable().Where(x => x.PurchaseOrderId == purchaseorderid
                  && x.SupplierId == supplierid
                  && x.Status != "作废"
                  && !x.Status.Contains("废标") )
              .OrderByDescending(x => x.Id)
              .Select(x=>x.BiddingPrice)
              .FirstOrDefault();
      return result;
    }
    private DateTime? GetLastBiddingDate(int purchaseorderid, int supplierid)
    {
      var query = this.unitOfWork.RepositoryAsync<Bidding>();
      var result = query.Queryable().Where(x => x.PurchaseOrderId == purchaseorderid
                   && x.SupplierId == supplierid
                   && x.Status != "作废"
                   && !x.Status.Contains("废标"))
              .OrderByDescending(x => x.Id)
              .Select(x => x.BiddingDate)
              .FirstOrDefault();
      return result;
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetOpeningData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                           .Query(new PurchaseOrderQuery().WithOpeningfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         DocNo = n.DocNo,
                                         Dept = n.Dept ?? "",
                                         Section= n.Section ?? "",
                                         GrantNo = n.GrantNo ?? "",
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo??"",
                                         ProductName = n.ProductName ?? "",
                                         Spec = n.Spec ?? "",
                                         BrandName = n.BrandName ?? "",
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         n.Reason,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature ?? "",
                                         Description = n.Description ?? "",
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetOpenedData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.purchaseOrderService
                         .Query(new PurchaseOrderQuery().WithOpenedfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Tenders = n.Tenders,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Category = n.Category ?? "",
                                         n.Dept,
                                         n.Section,
                                         n.GrantNo,
                                         DocNo = n.DocNo,
                                         PODate = n.PODate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo,
                                         ProductName = n.ProductName,
                                         Spec = n.Spec,
                                         BrandName = n.BrandName,
                                         Unit = n.Unit,
                                         n.Reason,
                                         Qty = n.Qty,
                                         ControlledPrice = n.ControlledPrice,
                                         Feature = n.Feature,
                                         Description = n.Description,
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         BiddingDate = n.BiddingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         DueDate = n.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         SO = n.SO,
                                         InvoiceNo = n.InvoiceNo,
                                         OpenDate = n.OpenDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BiddingTime = n.BiddingTime,
                                         BiddingUsers = n.BiddingUsers,
                                         MinPrice = n.MinPrice,
                                         MaxPrice = n.MaxPrice,
                                         BidedPrice = n.BidedPrice,
                                         SupplierName = n.SupplierName
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    public async Task<JsonResult> GetBidViewModelItem(int id) {
      var data = await this.purchaseOrderService.GetBidViewModel(id,Auth.GetFullName(),Auth.GetCompanyId());
      return Json(data, JsonRequestBehavior.AllowGet);

      }
    public async Task<JsonResult> Bid(BidRequestViewModel request) {
      try
      {
        request.UserName = Auth.GetFullName(this.User.Identity.Name);
        request.SupplierId = Auth.GetCompanyId(this.User.Identity.Name);
        await this.purchaseOrderService.Bid(request);
        await this.unitOfWork.SaveChangesAsync();
        await this.purchaseOrderService.BidedSumarry(request.PurchaseOrderId);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //废标
    public async Task<JsonResult> Destroy(int[] id,string reason)
    {
      try
      {

        await this.purchaseOrderService.Destroy(id, reason);
        await this.unitOfWork.SaveChangesAsync();
        //foreach (var key in id)
        //{
        //  await this.purchaseOrderService.BidedSumarry(key);
        //}
        //await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //中标
    public async Task<JsonResult> OutBid(int purchaseorderid, int bidid,string reason) {
      try
      {
  
        await this.purchaseOrderService.OutBid(purchaseorderid,bidid, reason, Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();

        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //中标拒绝
    public async Task<JsonResult> ConfirmRejectBid(int[] bidid, string reason) {
      try
      {

        await this.purchaseOrderService.ConfirmRejectBid(bidid,reason);
        await this.unitOfWork.SaveChangesAsync();

        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //中标确认
    public async Task<JsonResult> ConfirmBid(int[] bidid) {

      try
      {

        await this.purchaseOrderService.ConfirmBid( bidid);
        await this.unitOfWork.SaveChangesAsync();

        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //修改截至日期
    public async Task<JsonResult> ChagneDueDate(int id,DateTime duedate)
    {

      try
      {

        await this.purchaseOrderService.ChagneDueDate(id, duedate);
        await this.unitOfWork.SaveChangesAsync();

        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(PurchaseOrder[] purchaseorders)
    {
      if (purchaseorders == null)
      {
        throw new ArgumentNullException(nameof(purchaseorders));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in purchaseorders)
          {
            this.purchaseOrderService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
      }

    }
    //GET: PurchaseOrders/Details/:id
    public ActionResult Details(int id)
    {

      var purchaseOrder = this.purchaseOrderService.Find(id);
      if (purchaseOrder == null)
      {
        return HttpNotFound();
      }
      return View(purchaseOrder);
    }
    //GET: PurchaseOrders/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var purchaseOrder = await this.purchaseOrderService.FindAsync(id);
      return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
    }
    //GET: PurchaseOrders/Create
    public ActionResult Create()
    {
      var purchaseOrder = new PurchaseOrder();
      //set default value
      return View(purchaseOrder);
    }
    //POST: PurchaseOrders/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(PurchaseOrder purchaseOrder)
    {
      if (purchaseOrder == null)
      {
        throw new ArgumentNullException(nameof(purchaseOrder));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.purchaseOrderService.Insert(purchaseOrder);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a purchaseOrder record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(purchaseOrder);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var purchaseOrder = await Task.Run(() =>
      {
        return new PurchaseOrder();
      });
      return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
    }


    //GET: PurchaseOrders/Edit/:id
    public ActionResult Edit(int id)
    {
      var purchaseOrder = this.purchaseOrderService.Find(id);
      if (purchaseOrder == null)
      {
        return HttpNotFound();
      }
      return View(purchaseOrder);
    }
    //POST: PurchaseOrders/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(PurchaseOrder purchaseOrder)
    {
      if (purchaseOrder == null)
      {
        throw new ArgumentNullException(nameof(purchaseOrder));
      }
      if (ModelState.IsValid)
      {
        purchaseOrder.TrackingState = TrackingState.Modified;
        try
        {
          this.purchaseOrderService.Update(purchaseOrder);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException e)
        {
          var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
          return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a PurchaseOrder record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(purchaseOrder);
    }
    //删除当前记录
    //GET: PurchaseOrders/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.purchaseOrderService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }




    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      if (id == null)
      {
        throw new ArgumentNullException(nameof(id));
      }
      try
      {
        this.purchaseOrderService.Delete(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
        return Json(new { success = false, err = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }
    //导出Excel
    [HttpPost]
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "purchaseorders_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.purchaseOrderService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
