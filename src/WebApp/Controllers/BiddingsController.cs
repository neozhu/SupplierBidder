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
namespace WebApp.Controllers
{
  /// <summary>
  /// File: BiddingsController.cs
  /// Purpose:竞标操作/出价记录
  /// Created Date: 3/8/2020 7:58:09 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Bidding>, Repository<Bidding>>();
  ///    container.RegisterType<IBiddingService, BiddingService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Biddings")]
  public class BiddingsController : Controller
  {
    private readonly IBiddingService biddingService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public BiddingsController(
          IBiddingService biddingService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.biddingService = biddingService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: Biddings/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "出价记录", Order = 1)]
    [Authorize(Roles ="admin,厂商")]
    public ActionResult Index() => this.View();
    [Route("Me", Name = "我的出价记录", Order = 1)]
    [Authorize(Roles = "admin,供应商")]
    public ActionResult Me() => this.View();
    [Route("OutBid", Name = "中标记录", Order = 1)]
    [Authorize(Roles = "admin,供应商")]
    public ActionResult OutBid() => this.View();
    [Route("Shipping", Name = "发货", Order = 1)]
    [Authorize(Roles = "admin,供应商")]
    public ActionResult Shipping() => this.View();
    //获取我的投标记录
    public async Task<JsonResult> GetBidingHistoryWithMe(int purchaseorderid)
    {
      var supplierId = Auth.GetCompanyId();
      var data = (await this.biddingService.Queryable()
        .Where(x => x.SupplierId == supplierId && x.PurchaseOrderId == purchaseorderid
       
        )
        .ToListAsync())
        .Select(n => new {
          Id = n.Id,
          BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
          Status = n.Status,
          PO = n.PO,
          n.Dept,
           n.Section,
          n.DeliveryCycle,
          n.GrantNo,
          //DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
          LineNum = n.LineNum,
          ProductNo = n.ProductNo ?? "",
          ProductName = n.ProductName ?? "",
          Spec = n.Spec ?? "",
          BrandName = n.BrandName ?? "",
          Unit = n.Unit ?? "",
          Qty = n.Qty,
          BiddingPrice = n.BiddingPrice,
          TotalPrice = n.TotalPrice,
          Feature = n.Feature ?? "",
          Description = n.Description ?? "",
          Buyer = n.Buyer,
          BuyerPhone = n.BuyerPhone,
          UserName = n.UserName,
          SupplierId = n.SupplierId,
          SupplierName = n.SupplierName,
          DocNo = n.DocNo,
          PurchaseOrderId = n.PurchaseOrderId,
          SO = n.SO,
          ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
        })
        .ToList();
      return Json(data, JsonRequestBehavior.AllowGet);

    }

    //获取投标记录
    public async Task<JsonResult> GetBidingHistory(int purchaseorderid)
    {
      var supplierId = Auth.GetCompanyId();
      var data =(await this.biddingService.Queryable()
        .Where(x => x.PurchaseOrderId == purchaseorderid && !x.Status.Contains("废标") && x.Status!="作废" )
        .ToListAsync())
        .Select(n=>new {
          Id = n.Id,
          BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
          Status = n.Status,
          PO = n.PO,
          n.Dept,
          n.Section,
          n.DeliveryCycle,
          n.GrantNo,
          //DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
          LineNum = n.LineNum,
          ProductNo = n.ProductNo??"",
          ProductName = n.ProductName??"",
          Spec = n.Spec??"",
          BrandName = n.BrandName??"",
          Unit = n.Unit ?? "",
          Qty = n.Qty,
          BiddingPrice = n.BiddingPrice,
          TotalPrice = n.TotalPrice,
          Feature = n.Feature ?? "",
          Description = n.Description ?? "",
          Buyer = n.Buyer,
          BuyerPhone = n.BuyerPhone,
          UserName = n.UserName,
          SupplierId = n.SupplierId,
          SupplierName = n.SupplierName,
          DocNo = n.DocNo,
          PurchaseOrderId = n.PurchaseOrderId,
          SO = n.SO,
          ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
        })
        .OrderBy(x => x.BiddingPrice)
        .ThenBy(x => x.BiddingDate)
        .ToList();
      return Json(data, JsonRequestBehavior.AllowGet);

    }

    //获取投所有标记录(包含作废废标)
    public async Task<JsonResult> GetBidingHistoryAll(int purchaseorderid)
    {
      var supplierId = Auth.GetCompanyId();
      var data = ( await this.biddingService.Queryable()
        .Where(x => x.PurchaseOrderId == purchaseorderid )
        .ToListAsync() )
        .Select(n => new {
          Id = n.Id,
          BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
          Status = n.Status,
          PO = n.PO,
          n.Dept,
          n.Section,
          n.GrantNo,
          n.DeliveryCycle,
          //DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
          LineNum = n.LineNum,
          ProductNo = n.ProductNo ?? "",
          ProductName = n.ProductName ?? "",
          Spec = n.Spec ?? "",
          BrandName = n.BrandName ?? "",
          Unit = n.Unit ?? "",
          Qty = n.Qty,
          BiddingPrice = n.BiddingPrice,
          TotalPrice = n.TotalPrice,
          Feature = n.Feature ?? "",
          Description = n.Description ?? "",
          Buyer = n.Buyer,
        
          BuyerPhone = n.BuyerPhone,
          UserName = n.UserName,
          SupplierId = n.SupplierId,
          SupplierName = n.SupplierName,
          DocNo = n.DocNo,
          PurchaseOrderId = n.PurchaseOrderId,
          SO = n.SO,
          ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
        })
        .OrderBy(x => x.BiddingPrice)
        .ThenBy(x => x.BiddingDate)
        .ToList();
      return Json(data, JsonRequestBehavior.AllowGet);

    }

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetOutBidData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                       .Query(new BiddingQuery().WithOutBidfilter(Auth.GetCompanyId(), filters))
                       .Include(b => b.PurchaseOrder)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {
                                     PurchaseOrderPO = n.PurchaseOrder?.PO,
                                     Id = n.Id,
                                     BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Status = n.Status,
                                     PO = n.PO,
                                     n.Dept,
                                     n.Section,
                                     n.GrantNo,
                                     DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     LineNum = n.LineNum,
                                     ProductNo = n.ProductNo??"",
                                     ProductName = n.ProductName??"",
                                     Spec = n.Spec ?? "",
                                     BrandName = n.BrandName ?? "",
                                     Unit = n.Unit,
                                     Qty = n.Qty,
                                     n.DeliveryCycle,
                                     BiddingPrice = n.BiddingPrice,
                                     TotalPrice = n.TotalPrice,
                                     Feature = n.Feature ?? "",
                                     Description = n.Description ?? "",
                                     Buyer = n.Buyer,
                                     BuyerPhone = n.BuyerPhone,
                                     UserName = n.UserName,
                                     SupplierId = n.SupplierId,
                                     SupplierName = n.SupplierName,
                                     DocNo = n.DocNo,
                                     PurchaseOrderId = n.PurchaseOrderId,
                                     SO = n.SO,
                                     ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetShippingData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                       .Query(new BiddingQuery().WithShippingfilter(Auth.GetCompanyId(), filters))
                       .Include(b => b.PurchaseOrder)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {
                                     PurchaseOrderPO = n.PurchaseOrder?.PO,
                                     Id = n.Id,
                                     n.Dept,
                                     n.Section,
                                     n.GrantNo,
                                     n.DeliveryCycle,
                                     BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Status = n.Status,
                                     PO = n.PO,
                                     DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     LineNum = n.LineNum,
                                     ProductNo = n.ProductNo ?? "",
                                     ProductName = n.ProductName ?? "",
                                     Spec = n.Spec ?? "",
                                     BrandName = n.BrandName ?? "",
                                     Unit = n.Unit,
                                     Qty = n.Qty,
                                     BiddingPrice = n.BiddingPrice,
                                     TotalPrice = n.TotalPrice,
                                     Feature = n.Feature ?? "",
                                     Description = n.Description ?? "",
                                     Buyer = n.Buyer,
                                     BuyerPhone = n.BuyerPhone,
                                     UserName = n.UserName,
                                     SupplierId = n.SupplierId,
                                     SupplierName = n.SupplierName,
                                     DocNo = n.DocNo,
                                     PurchaseOrderId = n.PurchaseOrderId,
                                     SO = n.SO,
                                     ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataWithSo(string so)
    {
       var data = (await this.biddingService.Queryable().Where(x=>x.SO==so)
                            .ToListAsync())
                                   .Select(n => new
                                   {
                                     Id = n.Id,
                                     BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Status = n.Status,
                                     PO = n.PO,
                                     n.Dept,
                                     n.Section,
                                     n.GrantNo,
                                     DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     LineNum = n.LineNum,
                                     ProductNo = n.ProductNo ?? "",
                                     ProductName = n.ProductName ?? "",
                                     Spec = n.Spec ?? "",
                                     BrandName = n.BrandName ?? "",
                                     Unit = n.Unit,
                                     Qty = n.Qty,
                                     BiddingPrice = n.BiddingPrice,
                                     TotalPrice = n.TotalPrice,
                                     Feature = n.Feature ?? "",
                                     Description = n.Description ?? "",
                                     Buyer = n.Buyer,
                                     n.DeliveryCycle,
                                     BuyerPhone = n.BuyerPhone,
                                     UserName = n.UserName,
                                     SupplierId = n.SupplierId,
                                     SupplierName = n.SupplierName,
                                     DocNo = n.DocNo,
                                     PurchaseOrderId = n.PurchaseOrderId,
                                     SO = n.SO,
                                     ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                   }).ToList();
    
      return Json(data, JsonRequestBehavior.AllowGet);
    }


    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDestroyData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                           .Query(new BiddingQuery().WithMeDestroyfilter((User.IsInRole("admin")?0: Auth.GetCompanyId()), filters)).Include(b => b.PurchaseOrder)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         PurchaseOrderPO = n.PurchaseOrder?.PO,
                                         Id = n.Id,
                                         BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         PO = n.PO,
                                         n.Dept,
                                         n.DeliveryCycle,
                                         n.Section,
                                         n.GrantNo,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo,
                                         ProductName = n.ProductName,
                                         Spec = n.Spec,
                                         BrandName = n.BrandName,
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         BiddingPrice = n.BiddingPrice,
                                         TotalPrice = n.TotalPrice,
                                         Feature = n.Feature,
                                         Description = n.Description,
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         UserName = n.UserName,
                                         SupplierId = n.SupplierId,
                                         SupplierName = n.SupplierName,
                                         DocNo = n.DocNo,
                                         PurchaseOrderId = n.PurchaseOrderId,
                                         SO = n.SO,
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                       }).ToList();
      var footer = new List<dynamic>();
      footer.Add(new { Status = "本页汇总", Qty = pagerows.Sum(x => x.Qty), TotalPrice = pagerows.Sum(x => x.TotalPrice) });
      var pagelist = new { total = totalCount, rows = pagerows, footer = footer };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }


    //Get :Biddings/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetMeData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                           .Query(new BiddingQuery().WithMefilter(Auth.GetCompanyId(), filters)).Include(b => b.PurchaseOrder)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         PurchaseOrderPO = n.PurchaseOrder?.PO,
                                         Id = n.Id,
                                         BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         PO = n.PO,
                                         n.Dept,
                                         n.DeliveryCycle,
                                         n.Section,
                                         n.GrantNo,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo,
                                         ProductName = n.ProductName,
                                         Spec = n.Spec,
                                         BrandName = n.BrandName,
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         BiddingPrice = n.BiddingPrice,
                                         TotalPrice = n.TotalPrice,
                                         Feature = n.Feature,
                                         Description = n.Description,
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         UserName = n.UserName,
                                         SupplierId = n.SupplierId,
                                         SupplierName = n.SupplierName,
                                         DocNo = n.DocNo,
                                         PurchaseOrderId = n.PurchaseOrderId,
                                         SO=n.SO,
                                         ShippingDate=n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                       }).ToList();
      var footer = new List<dynamic>();
      footer.Add( new { Status = "本页汇总", Qty= pagerows.Sum(x=>x.Qty), TotalPrice= pagerows.Sum(x=>x.TotalPrice) });
      var pagelist = new { total = totalCount, rows = pagerows, footer= footer };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                           .Query(new BiddingQuery().Withfilter(filters)).Include(b => b.PurchaseOrder)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         PurchaseOrderPO = n.PurchaseOrder?.PO,
                                         Id = n.Id,
                                         n.DeliveryCycle,
                                         BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Status = n.Status,
                                         PO = n.PO,
                                         n.Dept,
                                         n.Section,
                                         n.GrantNo,
                                         DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         LineNum = n.LineNum,
                                         ProductNo = n.ProductNo,
                                         ProductName = n.ProductName,
                                         Spec = n.Spec,
                                         BrandName = n.BrandName,
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         BiddingPrice = n.BiddingPrice,
                                         TotalPrice = n.TotalPrice,
                                         Feature = n.Feature,
                                         Description = n.Description,
                                         Buyer = n.Buyer,
                                         BuyerPhone = n.BuyerPhone,
                                         UserName = n.UserName,
                                         SupplierId = n.SupplierId,
                                         SupplierName = n.SupplierName,
                                         DocNo = n.DocNo,
                                         PurchaseOrderId = n.PurchaseOrderId,
                                         SO = n.SO,
                                         ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByPurchaseOrderId(int purchaseorderid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.biddingService
                       .Query(new BiddingQuery().ByPurchaseOrderIdWithfilter(purchaseorderid, filters)).Include(b => b.PurchaseOrder)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     PurchaseOrderPO = n.PurchaseOrder?.PO,
                                     Id = n.Id,
                                     BiddingDate = n.BiddingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Status = n.Status,
                                     PO = n.PO,
                                     n.Dept,
                                     n.Section,
                                     n.DeliveryCycle,
                                     n.GrantNo,
                                     DemandedDate = n.DemandedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     LineNum = n.LineNum,
                                     ProductNo = n.ProductNo,
                                     ProductName = n.ProductName,
                                     Spec = n.Spec,
                                     BrandName = n.BrandName,
                                     Unit = n.Unit,
                                     Qty = n.Qty,
                                     BiddingPrice = n.BiddingPrice,
                                     TotalPrice = n.TotalPrice,
                                     Feature = n.Feature,
                                     Description = n.Description,
                                     Buyer = n.Buyer,
                                     BuyerPhone = n.BuyerPhone,
                                     UserName = n.UserName,
                                     SupplierId = n.SupplierId,
                                     SupplierName = n.SupplierName,
                                     DocNo = n.DocNo,
                                     PurchaseOrderId = n.PurchaseOrderId,
                                     SO = n.SO,
                                     ShippingDate = n.ShippingDate?.ToString("yyyy-MM-dd HH:mm:ss")
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(Bidding[] biddings)
    {
      if (biddings == null)
      {
        throw new ArgumentNullException(nameof(biddings));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in biddings)
          {
            this.biddingService.ApplyChanges(item);
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
    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetPurchaseOrders(string q = "")
    {
      var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      var rows = await purchaseorderRepository
                            .Queryable()
                            .Where(n => n.PO.Contains(q))
                            .OrderBy(n => n.PO)
                            .Select(n => new { Id = n.Id, PO = n.PO })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }
    //GET: Biddings/Details/:id
    public ActionResult Details(int id)
    {

      var bidding = this.biddingService.Find(id);
      if (bidding == null)
      {
        return HttpNotFound();
      }
      return View(bidding);
    }
    //GET: Biddings/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var bidding = await this.biddingService.FindAsync(id);
      return Json(bidding, JsonRequestBehavior.AllowGet);
    }
    //GET: Biddings/Create
    public ActionResult Create()
    {
      var bidding = new Bidding();
      //set default value
      var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n => n.PO), "Id", "PO");
      return View(bidding);
    }
    //POST: Biddings/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Bidding bidding)
    {
      if (bidding == null)
      {
        throw new ArgumentNullException(nameof(bidding));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.biddingService.Insert(bidding);
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
        //DisplaySuccessMessage("Has update a bidding record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      //ViewBag.PurchaseOrderId = new SelectList(await purchaseorderRepository.Queryable().OrderBy(n=>n.PO).ToListAsync(), "Id", "PO", bidding.PurchaseOrderId);
      //return View(bidding);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var bidding = await Task.Run(() =>
      {
        return new Bidding();
      });
      return Json(bidding, JsonRequestBehavior.AllowGet);
    }


    //GET: Biddings/Edit/:id
    public ActionResult Edit(int id)
    {
      var bidding = this.biddingService.Find(id);
      if (bidding == null)
      {
        return HttpNotFound();
      }
      var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n => n.PO), "Id", "PO", bidding.PurchaseOrderId);
      return View(bidding);
    }
    //POST: Biddings/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Bidding bidding)
    {
      if (bidding == null)
      {
        throw new ArgumentNullException(nameof(bidding));
      }
      if (ModelState.IsValid)
      {
        bidding.TrackingState = TrackingState.Modified;
        try
        {
          this.biddingService.Update(bidding);

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

        //DisplaySuccessMessage("Has update a Bidding record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      //return View(bidding);
    }
    //删除当前记录
    //GET: Biddings/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.biddingService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        this.biddingService.Delete(id);
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
      var fileName = "biddings_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.biddingService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
