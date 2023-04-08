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
/// File: TendersController.cs
/// Purpose:采购中心/询价单
/// Created Date: 3/7/2020 9:41:50 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Tender>, Repository<Tender>>();
///    container.RegisterType<ITenderService, TenderService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Tenders")]
	public class TendersController : Controller
	{
		private readonly ITenderService  tenderService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public TendersController (
          ITenderService  tenderService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.tenderService  = tenderService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Tenders/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "询价单", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Tenders/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.tenderService
						               .Query(new TenderQuery().Withfilter(filters)).Include(t => t.PurchaseOrder).Include(t => t.Supplier)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    PurchaseOrderPO = n.PurchaseOrder?.PO,
    SupplierName = n.Supplier?.Name,
    Id = n.Id,
    DocNo = n.DocNo,
    PurchaseOrderId = n.PurchaseOrderId,
    SupplierId = n.SupplierId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByPurchaseOrderId (int  purchaseorderid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.tenderService
						               .Query(new TenderQuery().ByPurchaseOrderIdWithfilter(purchaseorderid,filters)).Include(t => t.PurchaseOrder).Include(t => t.Supplier)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    PurchaseOrderPO = n.PurchaseOrder?.PO,
    SupplierName = n.Supplier?.Name,
    Id = n.Id,
    DocNo = n.DocNo,
    PurchaseOrderId = n.PurchaseOrderId,
    SupplierId = n.SupplierId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataBySupplierId (int  supplierid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.tenderService
						               .Query(new TenderQuery().BySupplierIdWithfilter(supplierid,filters)).Include(t => t.PurchaseOrder).Include(t => t.Supplier)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    PurchaseOrderPO = n.PurchaseOrder?.PO,
    SupplierName = n.Supplier?.Name,
    Id = n.Id,
    DocNo = n.DocNo,
    PurchaseOrderId = n.PurchaseOrderId,
    SupplierId = n.SupplierId
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveData(Tender[] tenders)
		{
            if (tenders == null)
            {
                throw new ArgumentNullException(nameof(tenders));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in tenders)
               {
                 this.tenderService.ApplyChanges(item);
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
				        //[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetPurchaseOrders(string q="")
		{
			var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
			var rows = await purchaseorderRepository
                            .Queryable()
                            .Where(n=>n.PO.Contains(q))
                            .OrderBy(n=>n.PO)
                            .Select(n => new { Id = n.Id, PO = n.PO })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
						        //[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetCompanies(string q="")
		{
			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			var rows = await companyRepository
                            .Queryable()
                            .Where(n=>n.Name.Contains(q))
                            .OrderBy(n=>n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
								//GET: Tenders/Details/:id
		public ActionResult Details(int id)
		{
			
			var tender = this.tenderService.Find(id);
			if (tender == null)
			{
				return HttpNotFound();
			}
			return View(tender);
		}
        //GET: Tenders/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  tender = await this.tenderService.FindAsync(id);
            return Json(tender,JsonRequestBehavior.AllowGet);
        }
		//GET: Tenders/Create
        		public ActionResult Create()
				{
			var tender = new Tender();
			//set default value
			var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
		   			ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n=>n.PO), "Id", "PO");
		   			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
		   			ViewBag.SupplierId = new SelectList(companyRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name");
		   			return View(tender);
		}
		//POST: Tenders/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Tender tender)
		{
			if (tender == null)
            {
                throw new ArgumentNullException(nameof(tender));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.tenderService.Insert(tender);
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
			    //DisplaySuccessMessage("Has update a tender record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
			//ViewBag.PurchaseOrderId = new SelectList(await purchaseorderRepository.Queryable().OrderBy(n=>n.PO).ToListAsync(), "Id", "PO", tender.PurchaseOrderId);
			//var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			//ViewBag.SupplierId = new SelectList(await companyRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", tender.SupplierId);
			//return View(tender);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var tender = await Task.Run(() => {
                return new Tender();
                });
            return Json(tender, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Tenders/Edit/:id
		public ActionResult Edit(int id)
		{
			var tender = this.tenderService.Find(id);
			if (tender == null)
			{
				return HttpNotFound();
			}
			var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
			ViewBag.PurchaseOrderId = new SelectList(purchaseorderRepository.Queryable().OrderBy(n=>n.PO), "Id", "PO", tender.PurchaseOrderId);
			var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
			ViewBag.SupplierId = new SelectList(companyRepository.Queryable().OrderBy(n=>n.Name), "Id", "Name", tender.SupplierId);
			return View(tender);
		}
		//POST: Tenders/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Tender tender)
		{
            if (tender == null)
            {
                throw new ArgumentNullException(nameof(tender));
            }
			if (ModelState.IsValid)
			{
				tender.TrackingState = TrackingState.Modified;
				                try{
				this.tenderService.Update(tender);
				                
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
				
				//DisplaySuccessMessage("Has update a Tender record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var purchaseorderRepository = this.unitOfWork.RepositoryAsync<PurchaseOrder>();
												//var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
												//return View(tender);
		}
        //删除当前记录
		//GET: Tenders/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.tenderService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
               this.tenderService.Delete(id);
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
			var fileName = "tenders_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.tenderService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
