using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;
using Z.EntityFramework.Plus;
namespace WebApp.Controllers
{
  /// <summary>
  /// File: ShippingOrdersController.cs
  /// Purpose:投标操作/发货单管理
  /// Created Date: 3/10/2020 9:40:45 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<ShippingOrder>, Repository<ShippingOrder>>();
  ///    container.RegisterType<IShippingOrderService, ShippingOrderService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
    [RoutePrefix("ShippingOrders")]
	public class ShippingOrdersController : Controller
	{
		private readonly IShippingOrderService  shippingOrderService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public ShippingOrdersController (
          IShippingOrderService  shippingOrderService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.shippingOrderService  = shippingOrderService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: ShippingOrders/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "发货单", Order = 1)]
    [Authorize(Roles = "admin,供应商")]
    public ActionResult Index() => this.View();

    [Route("Receiving", Name = "收货作业", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult Receiving() => this.View();
    [Route("Closed", Name = "结案作业", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult Closed() => this.View();

    //Get :ShippingOrders/GetData
    //For Index View datagrid datasource url
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetReceivingData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.shippingOrderService
                           .Query(new ShippingOrderQuery().WithReceivingfilter( filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new {

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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetReceivedData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.shippingOrderService
                           .Query(new ShippingOrderQuery().WithReceivedfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new {

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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetMeData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.shippingOrderService
                           .Query(new ShippingOrderQuery().WithSupplierIdfilter(Auth.GetCompanyId(), filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new {

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
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.shippingOrderService
						               .Query(new ShippingOrderQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveData(ShippingOrder[] shippingorders)
		{
            if (shippingorders == null)
            {
                throw new ArgumentNullException(nameof(shippingorders));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in shippingorders)
               {
                 this.shippingOrderService.ApplyChanges(item);
               }
			   var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
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
						//GET: ShippingOrders/Details/:id
		public ActionResult Details(int id)
		{
			
			var shippingOrder = this.shippingOrderService.Find(id);
			if (shippingOrder == null)
			{
				return HttpNotFound();
			}
			return View(shippingOrder);
		}
        //GET: ShippingOrders/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  shippingOrder = await this.shippingOrderService.FindAsync(id);
            return Json(shippingOrder,JsonRequestBehavior.AllowGet);
        }
		//GET: ShippingOrders/Create
        		public ActionResult Create()
				{
			var shippingOrder = new ShippingOrder();
			//set default value
			return View(shippingOrder);
		}
		//POST: ShippingOrders/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ShippingOrder shippingOrder)
		{
			if (shippingOrder == null)
            {
                throw new ArgumentNullException(nameof(shippingOrder));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.shippingOrderService.Insert(shippingOrder);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
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
			    //DisplaySuccessMessage("Has update a shippingOrder record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(shippingOrder);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var shippingOrder = await Task.Run(() => {
                return new ShippingOrder();
                });
            return Json(shippingOrder, JsonRequestBehavior.AllowGet);
        }

         
		//GET: ShippingOrders/Edit/:id
		public ActionResult Edit(int id)
		{
			var shippingOrder = this.shippingOrderService.Find(id);
			if (shippingOrder == null)
			{
				return HttpNotFound();
			}
			return View(shippingOrder);
		}
		//POST: ShippingOrders/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ShippingOrder shippingOrder)
		{
            if (shippingOrder == null)
            {
                throw new ArgumentNullException(nameof(shippingOrder));
            }
			if (ModelState.IsValid)
			{
				shippingOrder.TrackingState = TrackingState.Modified;
				                try{
				this.shippingOrderService.Update(shippingOrder);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
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
				
				//DisplaySuccessMessage("Has update a ShippingOrder record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(shippingOrder);
		}
        //删除当前记录
		//GET: ShippingOrders/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.shippingOrderService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        public async Task<JsonResult> DeleteChecked(int[] id) {
           if (id == null)
           {
                throw new ArgumentNullException(nameof(id));
           }
           try{
               this.shippingOrderService.Delete(id);
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
		public async Task<ActionResult> ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "shippingorders_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.shippingOrderService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
