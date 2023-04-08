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
using SqlSugar;
using System.Configuration;
using WebApp.Models.ViewModel;
using LazyCache;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace WebApp.Controllers
{
  /// <summary>
  /// File: AllocatesController.cs
  /// Purpose:库存管理/分拨二级记录
  /// Created Date: 5/9/2021 7:43:25 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Allocate>, Repository<Allocate>>();
  ///    container.RegisterType<IAllocateService, AllocateService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Allocates")]
  public class AllocatesController : Controller
  {
    private readonly IAllocateService allocateService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly IAppCache cache;
    private readonly NLog.ILogger logger;
    private readonly SqlSugar.ISqlSugarClient mysql;
    public AllocatesController(
          IAllocateService allocateService,
          IUnitOfWorkAsync unitOfWork,
          IAppCache cache,
          NLog.ILogger logger
          )
    {
      this.allocateService = allocateService;
      this.unitOfWork = unitOfWork;
      this.cache = cache;
      this.logger = logger;
      mysql= new SqlSugarClient(new ConnectionConfig()
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString, //必填（连接字符串）
        DbType = SqlSugar.DbType.MySql, //必填（那个数据库）
        IsAutoCloseConnection = false, //默认false（是否自动关闭连接）
        InitKeyType = InitKeyType.Attribute
      }); //默认SystemTable
 
    }
    //GET: Allocates/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "分拨二级记录", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult Index() => this.View();

    [Route("MyLoc", Name = "二级库存查询", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult MyLoc() => this.View();
    //入库确认

    public async Task<JsonResult> Confirm(int[] id)
    {
      try
      {
        await this.allocateService.Confirm(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);

      }
    }

    [OutputCache(Duration = 20, VaryByParam = "term")]
    public async Task<JsonResult> GetSelectData(string term="") {
     
      var sql = $@"SELECT t1.Id as Id 
,rri_zh_name as ProductName
,rri_cas_no as CasNO
,rri_volume as Volume
,t4.RD_CONTENT as Unit
,t5.RD_CONTENT as Purity
,t2.RSI_NAME as SupplierName
,t3.RSI_NAME CustomerName
FROM rms.rms_reagent_info t1,
rms.rms_supplier_info t2,
rms.rms_supplier_info t3,
rms.rms_dict t4,
rms.rms_dict t5
 where t1.if_delete = 0
 and t1.rsi_id_supply = t2.id
 and t1.rsi_id_make = t3.id
 and t1.RD_ID_UNIT = t4.id
 and t1.rd_id_purity = t5.id";

      var result = await this.cache.GetOrAddAsync("rms_reagent_info", async () =>
      {
        return await mysql.Ado.SqlQueryAsync<ReagentInfo>(sql);
      });
      result = result.Where(x => x.ProductName.Contains(term)
       || x.Purity.Contains(term)
       || x.SupplierName.Contains(term)
       || x.CustomerName.Contains(term)
      ).ToList();


      //var result =  this.cache.GetOrAdd("rms_reagent_info",  () => {
      //  var data = new List<ReagentInfo>();
      //  for (var i = 0; i < 10; i++)
      //  {
      //    data.Add(new ReagentInfo()
      //    {
      //      CasNO = "abc-1-" + i.ToString(),
      //      Id = i.ToString(),
      //      CustomerName = "CustomerName-" + i.ToString(),
      //      ProductName = "ProductName-" + i.ToString(),
      //      SupplierName = "ProductName",
      //      Unit = "ml",
      //      Volume = "1909",
      //      Purity = "Purity"
      //    });
      //  }
      //  return data;
      //});
      result = result.Where(x => x.ProductName.Contains(term)
       || x.Purity.Contains(term)
       || x.SupplierName.Contains(term)
       || x.CustomerName.Contains(term)
      ).ToList();

     

      return Json(result, JsonRequestBehavior.AllowGet);
   }

    //Get :Allocates/GetData
    //For Index View datagrid datasource url

    //二级库查询
    public async Task<JsonResult> GetLocData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = PredicateBuilder.From<ReagentLoc>(filterRules);
      var sql = @"select Id
,ProductCode
,ProductName
,CasNO
,Volume
,Unit
,Purity
,SupplierName
,CustomerName
,Loc
,sum(Qty) as Qty
 from (
SELECT t1.Id as Id 
,RRI_NO as ProductCode
,rri_zh_name as ProductName
,rri_cas_no as CasNO
,rri_volume as Volume
,t4.RD_CONTENT as Unit
,t5.RD_CONTENT as Purity
,t2.RSI_NAME as SupplierName
,t3.RSI_NAME as CustomerName
,t6.stock_num as Qty
,t7.RL_NAME as Loc
FROM rms.rms_reagent_info t1,
rms.rms_supplier_info t2,
rms.rms_supplier_info t3,
rms.rms_dict t4,
rms.rms_dict t5,
rms.rms_rfid_product_mapping t6,
rms.rms_lab t7
 where t1.if_delete = 0
 and t1.rsi_id_supply = t2.id
 and t1.rsi_id_make = t3.id
 and t1.RD_ID_UNIT = t4.id
 and t1.rd_id_purity = t5.id
 and t1.id=t6.RRI_RSI_ID
 and t6.rl_no=t7.rl_no
 union all
 SELECT t1.Id as Id 
,RRI_NO as ProductCode
,rri_zh_name as ProductName
,rri_cas_no as CasNO
,rri_volume as Volume
,t4.RD_CONTENT as Unit
,t5.RD_CONTENT as Purity
,t2.RSI_NAME as SupplierName
,t3.RSI_NAME as CustomerName
,count(1) as Qty
,t7.RL_NAME as Loc
FROM rms.rms_reagent_info t1,
rms.rms_supplier_info t2,
rms.rms_supplier_info t3,
rms.rms_dict t4,
rms.rms_dict t5,
rms.rms_rfid_reagent_mapping t6,
rms.rms_lab t7
 where t1.if_delete = 0
 and t6.rrrm_state=1
 and t1.rsi_id_supply = t2.id
 and t1.rsi_id_make = t3.id
 and t1.RD_ID_UNIT = t4.id
 and t1.rd_id_purity = t5.id
 and t1.id=t6.RRI_ID
 and t6.rl_no=t7.rl_no
 group by t1.Id
 ,rri_zh_name
 ,rri_cas_no
 ,rri_volume
 ,t4.RD_CONTENT
 ,t5.RD_CONTENT
 ,t2.RSI_NAME
 ,t3.RSI_NAME
 ,t7.RL_NAME
 ) t
 group by 
 Id
 ,ProductCode
,ProductName
,CasNO
,Volume
,Unit
,Purity
,SupplierName
,CustomerName
,Loc";
      var count =await this.mysql.SqlQueryable<ReagentLoc>(sql).CountAsync();
      var result = this.mysql.SqlQueryable<ReagentLoc>(sql).Where(filters)
        .OrderBy($"{sort} {order}")
        .ToPageList(page, rows)
        .ToList();

      var pagelist = new { total = count, rows = result };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //导出二级库

    public async Task<ActionResult> ExportLoc(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = PredicateBuilder.From<ReagentLoc>(filterRules);
      var sql = @"select Id
,ProductCode
,ProductName
,CasNO
,Volume
,Unit
,Purity
,SupplierName
,CustomerName
,Loc
,sum(Qty) as Qty
 from (
SELECT t1.Id as Id 
,RRI_NO as ProductCode
,rri_zh_name as ProductName
,rri_cas_no as CasNO
,rri_volume as Volume
,t4.RD_CONTENT as Unit
,t5.RD_CONTENT as Purity
,t2.RSI_NAME as SupplierName
,t3.RSI_NAME as CustomerName
,t6.stock_num as Qty
,t7.RL_NAME as Loc
FROM rms.rms_reagent_info t1,
rms.rms_supplier_info t2,
rms.rms_supplier_info t3,
rms.rms_dict t4,
rms.rms_dict t5,
rms.rms_rfid_product_mapping t6,
rms.rms_lab t7
 where t1.if_delete = 0
 and t1.rsi_id_supply = t2.id
 and t1.rsi_id_make = t3.id
 and t1.RD_ID_UNIT = t4.id
 and t1.rd_id_purity = t5.id
 and t1.id=t6.RRI_RSI_ID
 and t6.rl_no=t7.rl_no
 union all
 SELECT t1.Id as Id 
,RRI_NO as ProductCode
,rri_zh_name as ProductName
,rri_cas_no as CasNO
,rri_volume as Volume
,t4.RD_CONTENT as Unit
,t5.RD_CONTENT as Purity
,t2.RSI_NAME as SupplierName
,t3.RSI_NAME as CustomerName
,count(1) as Qty
,t7.RL_NAME as Loc
FROM rms.rms_reagent_info t1,
rms.rms_supplier_info t2,
rms.rms_supplier_info t3,
rms.rms_dict t4,
rms.rms_dict t5,
rms.rms_rfid_reagent_mapping t6,
rms.rms_lab t7
 where t1.if_delete = 0
 and t6.rrrm_state=1
 and t1.rsi_id_supply = t2.id
 and t1.rsi_id_make = t3.id
 and t1.RD_ID_UNIT = t4.id
 and t1.rd_id_purity = t5.id
 and t1.id=t6.RRI_ID
 and t6.rl_no=t7.rl_no
 group by t1.Id
 ,rri_zh_name
 ,rri_cas_no
 ,rri_volume
 ,t4.RD_CONTENT
 ,t5.RD_CONTENT
 ,t2.RSI_NAME
 ,t3.RSI_NAME
 ,t7.RL_NAME
 ) t
 group by 
 Id
 ,ProductCode
,ProductName
,CasNO
,Volume
,Unit
,Purity
,SupplierName
,CustomerName
,Loc";
      var result =await this.mysql.SqlQueryable<ReagentLoc>(sql).Where(filters).ToListAsync();
      byte[] buffer = null;
      using (var stream = new MemoryStream())
      {
        using (var writer = new StreamWriter(stream,System.Text.Encoding.UTF8))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
          csv.WriteRecords(result);
          writer.Flush();
          buffer = stream.ToArray();
        }
      }
      var filestream = new MemoryStream(buffer);
      return File(filestream, "application/vnd.ms-excel","mysql-stock_" +DateTime.Now.ToString("yyyyMMdd") + ".csv");
    }

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.allocateService
                           .Query(new AllocateQuery().Withfilter(filters)).Include(a => a.PurchaseOrder)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         PurchaseOrderPO = n.PurchaseOrder?.PO,
                                         Id = n.Id,
                                         PO = n.PO,
                                         Loc=n.Loc,
                                         ProductInfo = n.ProductInfo,
                                         LineNum = n.LineNum,
                                         PODate = n.PODate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ConfirmDate = n.ConfirmDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         OuboundDate = n.OuboundDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         RecordUser = n.RecordUser,
                                         ProductNo = n.ProductNo,
                                         ProductName = n.ProductName,
                                         Spec = n.Spec,
                                         BrandName = n.BrandName,
                                         Unit = n.Unit,
                                         Qty = n.Qty,
                                         SupplierName = n.SupplierName,
                                         RefId = n.RefId,
                                         Remark = n.Remark,
                                         Status = n.Status,
                                         Feature = n.Feature,
                                         Description = n.Description,
                                         PurchaseOrderId = n.PurchaseOrderId
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByPurchaseOrderId(int purchaseorderid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.allocateService
                       .Query(new AllocateQuery().ByPurchaseOrderIdWithfilter(purchaseorderid, filters)).Include(a => a.PurchaseOrder)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     PurchaseOrderPO = n.PurchaseOrder?.PO,
                                     Id = n.Id,
                                     PO = n.PO,
                                     Loc = n.Loc,
                                     ProductInfo = n.ProductInfo,
                                     LineNum = n.LineNum,
                                     PODate = n.PODate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     ReceivedDate = n.ReceivedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     OuboundDate = n.OuboundDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     RecordUser = n.RecordUser,
                                     ProductNo = n.ProductNo,
                                     ProductName = n.ProductName,
                                     Spec = n.Spec,
                                     BrandName = n.BrandName,
                                     Unit = n.Unit,
                                     Qty = n.Qty,
                                     SupplierName = n.SupplierName,
                                     RefId = n.RefId,
                                     Remark = n.Remark,
                                     Status = n.Status,
                                     Feature = n.Feature,
                                     Description = n.Description,
                                     PurchaseOrderId = n.PurchaseOrderId
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(Allocate[] allocates)
    {
      if (allocates == null)
      {
        throw new ArgumentNullException(nameof(allocates));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in allocates)
          {
            this.allocateService.ApplyChanges(item);
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
    //GET: Allocates/Details/:id
    public ActionResult Details(int id)
    {

      var allocate = this.allocateService.Find(id);
      if (allocate == null)
      {
        return HttpNotFound();
      }
      return View(allocate);
    }
    //GET: Allocates/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var allocate = await this.allocateService.FindAsync(id);
      return Json(allocate, JsonRequestBehavior.AllowGet);
    }
    //GET: Allocates/Create
    public ActionResult Create()
    {
      var allocate = new Allocate();
      //set default value
      var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n => n.PO), "Id", "PO");
      return View(allocate);
    }
    //POST: Allocates/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Allocate allocate)
    {
      if (allocate == null)
      {
        throw new ArgumentNullException(nameof(allocate));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.allocateService.Insert(allocate);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a allocate record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      //ViewBag.PurchaseOrderId = new SelectList(await purchaseorderRepository.Queryable().OrderBy(n=>n.PO).ToListAsync(), "Id", "PO", allocate.PurchaseOrderId);
      //return View(allocate);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var allocate = await Task.Run(() =>
      {
        return new Allocate();
      });
      return Json(allocate, JsonRequestBehavior.AllowGet);
    }


    //GET: Allocates/Edit/:id
    public ActionResult Edit(int id)
    {
      var allocate = this.allocateService.Find(id);
      if (allocate == null)
      {
        return HttpNotFound();
      }
      var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n => n.PO), "Id", "PO", allocate.PurchaseOrderId);
      return View(allocate);
    }
    //POST: Allocates/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Allocate allocate)
    {
      if (allocate == null)
      {
        throw new ArgumentNullException(nameof(allocate));
      }
      if (ModelState.IsValid)
      {
        allocate.TrackingState = TrackingState.Modified;
        try
        {
          this.allocateService.Update(allocate);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Allocate record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
      //return View(allocate);
    }
    //删除当前记录
    //GET: Allocates/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.allocateService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.allocateService.Delete(id);
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
      var fileName = "allocates_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.allocateService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
