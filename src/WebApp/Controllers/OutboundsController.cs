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
/// File: OutboundsController.cs
/// Purpose:采购中心/领用记录
/// Created Date: 2020/9/3 10:27:18
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Outbound>, Repository<Outbound>>();
///    container.RegisterType<IOutboundService, OutboundService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Outbounds")]
	public class OutboundsController : Controller
	{
		private readonly IOutboundService  outboundService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public OutboundsController (
          IOutboundService  outboundService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.outboundService  = outboundService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Outbounds/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "领用记录", Order = 1)]
    [Authorize(Roles = "admin,厂商,仓库")]
    public ActionResult Index() => this.View();

		//Get :Outbounds/GetData
		//For Index View datagrid datasource url
        
		[HttpGet]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.outboundService
						               .Query(new OutboundQuery().Withfilter(filters))
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    Id = n.Id,
    PO = n.PO,
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
    StockQty = n.StockQty,
    BidedPrice = n.BidedPrice,
    Amount = n.Amount,
    SupplierName = n.SupplierName,
    Feature = n.Feature,
    Description = n.Description,
    Remark = n.Remark
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> SaveData(Outbound[] outbounds)
		{
            if (outbounds == null)
            {
                throw new ArgumentNullException(nameof(outbounds));
            }
            if (ModelState.IsValid)
			{
            try{
               foreach (var item in outbounds)
               {
                 this.outboundService.ApplyChanges(item);
               }
			   var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
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
						//GET: Outbounds/Details/:id
		public ActionResult Details(int id)
		{
			
			var outbound = this.outboundService.Find(id);
			if (outbound == null)
			{
				return HttpNotFound();
			}
			return View(outbound);
		}
        //GET: Outbounds/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  outbound = await this.outboundService.FindAsync(id);
            return Json(outbound,JsonRequestBehavior.AllowGet);
        }
		//GET: Outbounds/Create
        		public ActionResult Create()
				{
			var outbound = new Outbound();
			//set default value
			return View(outbound);
		}
		//POST: Outbounds/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Outbound outbound)
		{
			if (outbound == null)
            {
                throw new ArgumentNullException(nameof(outbound));
            } 
            if (ModelState.IsValid)
			{
                try{ 
				this.outboundService.Insert(outbound);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a outbound record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//return View(outbound);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var outbound = await Task.Run(() => {
                return new Outbound();
                });
            return Json(outbound, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Outbounds/Edit/:id
		public ActionResult Edit(int id)
		{
			var outbound = this.outboundService.Find(id);
			if (outbound == null)
			{
				return HttpNotFound();
			}
			return View(outbound);
		}
		//POST: Outbounds/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Outbound outbound)
		{
            if (outbound == null)
            {
                throw new ArgumentNullException(nameof(outbound));
            }
			if (ModelState.IsValid)
			{
				outbound.TrackingState = TrackingState.Modified;
				                try{
				this.outboundService.Update(outbound);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Outbound record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//return View(outbound);
		}
        //删除当前记录
		//GET: Outbounds/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.outboundService.Queryable().Where(x => x.Id == id).DeleteAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
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
               await this.outboundService.Delete(id);
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
		public async Task<ActionResult> ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "outbounds_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.outboundService.ExportExcelAsync(filterRules,sort, order );
			return File(stream, "application/vnd.ms-excel", fileName);
		}
		private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
        private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;
		 
	}
}
