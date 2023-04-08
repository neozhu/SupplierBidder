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
  /// File: CategoryAllocationsController.cs
  /// Purpose:厂商信息/分配厂商
  /// Created Date: 2020/7/20 13:32:27
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<CategoryAllocation>, Repository<CategoryAllocation>>();
  ///    container.RegisterType<ICategoryAllocationService, CategoryAllocationService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("CategoryAllocations")]
  public class CategoryAllocationsController : Controller
  {
    private readonly ICategoryAllocationService categoryAllocationService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public CategoryAllocationsController(
          ICategoryAllocationService categoryAllocationService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.categoryAllocationService = categoryAllocationService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: CategoryAllocations/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "分配厂商", Order = 1)]
    public ActionResult Index() => this.View();

    //Get :CategoryAllocations/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.categoryAllocationService
                           .Query(new CategoryAllocationQuery().Withfilter(filters)).Include(c => c.Category).Include(c => c.Company)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         CategoryName = n.Category?.Name,
                                         CompanyName = n.Company?.Name,
                                         Id = n.Id,
                                         CategoryId = n.CategoryId,
                                         CompanyId = n.CompanyId,
                                         Remark = n.Remark,
                                         IsDisabled = n.IsDisabled
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByCategoryId(int categoryid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.categoryAllocationService
                       .Query(new CategoryAllocationQuery().ByCategoryIdWithfilter(categoryid, filters)).Include(c => c.Category).Include(c => c.Company)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CategoryName = n.Category?.Name,
                                     CompanyName = n.Company?.Name,
                                     Id = n.Id,
                                     CategoryId = n.CategoryId,
                                     CompanyId = n.CompanyId,
                                     Remark = n.Remark,
                                     IsDisabled = n.IsDisabled
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //分配供应商
    [HttpPost]
    public async Task<JsonResult> Allocation(AllocationRequest request) {
      try
      {
        await this.categoryAllocationService.Allocation(request);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      
      }
      catch (Exception e) {
        return Json(new { success = false,err=e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
      }
    //获取可竞标的供应商
    [HttpPost]
    public async Task<JsonResult> GetSupplierList(string[] categoryname) {
      var list = ( await this.categoryAllocationService.Queryable()
          .Where(x => categoryname.Contains(x.CategoryName))
          .Select(x => new
          {
            x.CompanyId,
            x.CompanyName
          })
          .ToListAsync() )
          .Distinct();

          return Json(list, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByCompanyId(int companyid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.categoryAllocationService
                       .Query(new CategoryAllocationQuery().ByCompanyIdWithfilter(companyid, filters)).Include(c => c.Category).Include(c => c.Company)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     CategoryName = n.Category?.Name,
                                     CompanyName = n.Company?.Name,
                                     Id = n.Id,
                                     CategoryId = n.CategoryId,
                                     CompanyId = n.CompanyId,
                                     Remark = n.Remark,
                                     IsDisabled = n.IsDisabled
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(CategoryAllocation[] categoryallocations)
    {
      if (categoryallocations == null)
      {
        throw new ArgumentNullException(nameof(categoryallocations));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in categoryallocations)
          {
            this.categoryAllocationService.ApplyChanges(item);
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
    public async Task<JsonResult> GetCategories(string q = "")
    {
      var categoryRepository = this.unitOfWork.RepositoryAsync<Category>();
      var rows = await categoryRepository
                            .Queryable()
                            .Where(n => n.Name.Contains(q))
                            .OrderBy(n => n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }
    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetCompanies(string q = "")
    {
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      var rows = await companyRepository
                            .Queryable()
                            .Where(n => n.Name.Contains(q))
                            .OrderBy(n => n.Name)
                            .Select(n => new { Id = n.Id, Name = n.Name })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }
    //GET: CategoryAllocations/Details/:id
    public ActionResult Details(int id)
    {

      var categoryAllocation = this.categoryAllocationService.Find(id);
      if (categoryAllocation == null)
      {
        return HttpNotFound();
      }
      return View(categoryAllocation);
    }
    //GET: CategoryAllocations/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var categoryAllocation = await this.categoryAllocationService.FindAsync(id);
      return Json(categoryAllocation, JsonRequestBehavior.AllowGet);
    }
    //GET: CategoryAllocations/Create
    public ActionResult Create()
    {
      var categoryAllocation = new CategoryAllocation();
      //set default value
      var categoryRepository = this.unitOfWork.RepositoryAsync<Category>();
      ViewBag.CategoryId = new SelectList(categoryRepository.Queryable().OrderBy(n => n.Name), "Id", "Name");
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name");
      return View(categoryAllocation);
    }
    //POST: CategoryAllocations/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CategoryAllocation categoryAllocation)
    {
      if (categoryAllocation == null)
      {
        throw new ArgumentNullException(nameof(categoryAllocation));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.categoryAllocationService.Insert(categoryAllocation);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a categoryAllocation record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var categoryRepository = this.unitOfWork.RepositoryAsync<Category>();
      //ViewBag.CategoryId = new SelectList(await categoryRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", categoryAllocation.CategoryId);
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //ViewBag.CompanyId = new SelectList(await companyRepository.Queryable().OrderBy(n=>n.Name).ToListAsync(), "Id", "Name", categoryAllocation.CompanyId);
      //return View(categoryAllocation);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var categoryAllocation = await Task.Run(() =>
      {
        return new CategoryAllocation();
      });
      return Json(categoryAllocation, JsonRequestBehavior.AllowGet);
    }


    //GET: CategoryAllocations/Edit/:id
    public ActionResult Edit(int id)
    {
      var categoryAllocation = this.categoryAllocationService.Find(id);
      if (categoryAllocation == null)
      {
        return HttpNotFound();
      }
      var categoryRepository = this.unitOfWork.RepositoryAsync<Category>();
      ViewBag.CategoryId = new SelectList(categoryRepository.Queryable().OrderBy(n => n.Name), "Id", "Name", categoryAllocation.CategoryId);
      var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      ViewBag.CompanyId = new SelectList(companyRepository.Queryable().OrderBy(n => n.Name), "Id", "Name", categoryAllocation.CompanyId);
      return View(categoryAllocation);
    }
    //POST: CategoryAllocations/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CategoryAllocation categoryAllocation)
    {
      if (categoryAllocation == null)
      {
        throw new ArgumentNullException(nameof(categoryAllocation));
      }
      if (ModelState.IsValid)
      {
        categoryAllocation.TrackingState = TrackingState.Modified;
        try
        {
          this.categoryAllocationService.Update(categoryAllocation);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a CategoryAllocation record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var categoryRepository = this.unitOfWork.RepositoryAsync<Category>();
      //var companyRepository = this.unitOfWork.RepositoryAsync<Company>();
      //return View(categoryAllocation);
    }
    //删除当前记录
    //GET: CategoryAllocations/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.categoryAllocationService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.categoryAllocationService.Delete(id);
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
      var fileName = "categoryallocations_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.categoryAllocationService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
