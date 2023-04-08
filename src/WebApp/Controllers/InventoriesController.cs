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

namespace WebApp.Controllers
{
  /// <summary>
  /// File: InventoriesController.cs
  /// Purpose:采购中心/库存管理
  /// Created Date: 2020/9/3 10:07:23
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Inventory>, Repository<Inventory>>();
  ///    container.RegisterType<IInventoryService, InventoryService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Inventories")]
  public class InventoriesController : Controller
  {
    private readonly IInventoryService inventoryService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    private readonly SqlSugar.ISqlSugarClient db;
    public InventoriesController(
          SqlSugar.ISqlSugarClient db,
          IInventoryService inventoryService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.inventoryService = inventoryService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
      this.db = db;
    }
    //GET: Inventories/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "库存管理", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult Index() => this.View();
    

    //下拨到二级库
    public async Task<JsonResult> Allocated(AllocateDto[] request)
    {
      try
      {
        await this.inventoryService.Allocated(request, Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);

      }
    }

    //Get :Inventories/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    public async Task<JsonResult> GetHistories(int id)
    {
      var result = await this.inventoryService.GetHistories(id);
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public async Task<JsonResult> Outbound(outboundviewmodel[] data)
    {
      try
      {
        await this.inventoryService.Outbound(data, Auth.GetFullName());
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);

      }
    }
    //获取每月进出汇总数量

    [HttpGet]
    public async Task<JsonResult> GetIOChartData() {
      var sql = @"select * from (
select format(OuboundDate,N'yyyy年MM月') [date],sum(Qty) qty,N'领用数量' as direct from [dbo].[Outbounds]
group  BY format(OuboundDate,N'yyyy年MM月')
union all
select format(ReceivedDate,N'yyyy年MM月') [date],sum(Qty) qty,N'收货数量' as direct from [dbo].[PurchaseOrders] where ReceivedDate is not null
group  BY format(ReceivedDate,N'yyyy年MM月')
union all
select format(ReceivedDate,N'yyyy年MM月') [date],sum(Qty) qty,N'下拨数量' as direct from [dbo].[Allocates] where ReceivedDate is not null
group  BY format(ReceivedDate,N'yyyy年MM月')
) t
order by t.direct ,[date]
";
      var result =await this.db.Ado.SqlQueryAsync<invmonthdata>(sql);
      return Json(result, JsonRequestBehavior.AllowGet);
      }

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(bool nz = true, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.inventoryService
                           .Query(new InventoryQuery().Withfilter(nz, filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Id = n.Id,
                                         PO = n.PO,
                                         LineNum = n.LineNum,
                                         PODate = n.PODate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Receiver = n.Receiver,
                                         Status = n.Status,
                                         InvStatus = n.InvStatus,
                                         PurchaseOrderId = n.PurchaseOrderId,
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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(Inventory[] inventories)
    {
      if (inventories == null)
      {
        throw new ArgumentNullException(nameof(inventories));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in inventories)
          {
            this.inventoryService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
      }

    }
    //GET: Inventories/Details/:id
    public ActionResult Details(int id)
    {

      var inventory = this.inventoryService.Find(id);
      if (inventory == null)
      {
        return HttpNotFound();
      }
      return View(inventory);
    }
    //GET: Inventories/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var inventory = await this.inventoryService.FindAsync(id);
      return Json(inventory, JsonRequestBehavior.AllowGet);
    }
    //GET: Inventories/Create
    public ActionResult Create()
    {
      var inventory = new Inventory();
      //set default value
      return View(inventory);
    }
    //POST: Inventories/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Inventory inventory)
    {
      if (inventory == null)
      {
        throw new ArgumentNullException(nameof(inventory));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.inventoryService.Insert(inventory);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a inventory record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(inventory);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var inventory = await Task.Run(() =>
      {
        return new Inventory();
      });
      return Json(inventory, JsonRequestBehavior.AllowGet);
    }


    //GET: Inventories/Edit/:id
    public ActionResult Edit(int id)
    {
      var inventory = this.inventoryService.Find(id);
      if (inventory == null)
      {
        return HttpNotFound();
      }
      return View(inventory);
    }
    //POST: Inventories/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Inventory inventory)
    {
      if (inventory == null)
      {
        throw new ArgumentNullException(nameof(inventory));
      }
      if (ModelState.IsValid)
      {
        inventory.TrackingState = TrackingState.Modified;
        try
        {
          this.inventoryService.Update(inventory);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Inventory record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(inventory);
    }
    //删除当前记录
    //GET: Inventories/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.inventoryService.Queryable().Where(x => x.Id == id).DeleteAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
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
        await this.inventoryService.Delete(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //导出Excel
    [HttpPost]
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "inventories_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.inventoryService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
