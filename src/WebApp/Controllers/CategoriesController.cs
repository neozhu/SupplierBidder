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
  /// File: CategoriesController.cs
  /// Purpose:厂商管理/采购类别
  /// Created Date: 2020/7/20 13:13:40
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Category>, Repository<Category>>();
  ///    container.RegisterType<ICategoryService, CategoryService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Categories")]
  public class CategoriesController : Controller
  {
    private readonly ICategoryService categoryService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public CategoriesController(
          ICategoryService categoryService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.categoryService = categoryService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: Categories/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "采购类别", Order = 1)]
    public ActionResult Index() => this.View();

    [HttpGet]
    public async Task<JsonResult> GetComboData(string q="") {
      var list =await this.categoryService.Queryable()
        .Where(x => x.Name.Contains(q))
         .ToListAsync();
      return Json(list, JsonRequestBehavior.AllowGet);
     }
    //Get :Categories/GetData
    //For Index View datagrid datasource url

    [HttpGet]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.categoryService
                           .Query(new CategoryQuery().Withfilter(filters))
                           .Include(x=>x.Allocations)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Id = n.Id,
                                         Name = n.Name,
                                         Remark = n.Remark,
                                         AllowSuppliers = n.Allocations.Count
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> SaveData(Category[] categories)
    {
      if (categories == null)
      {
        throw new ArgumentNullException(nameof(categories));
      }
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in categories)
          {
            this.categoryService.ApplyChanges(item);
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
    //GET: Categories/Details/:id
    public ActionResult Details(int id)
    {

      var category = this.categoryService.Find(id);
      if (category == null)
      {
        return HttpNotFound();
      }
      return View(category);
    }
    //GET: Categories/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var category = await this.categoryService.FindAsync(id);
      return Json(category, JsonRequestBehavior.AllowGet);
    }
    //GET: Categories/Create
    public ActionResult Create()
    {
      var category = new Category();
      //set default value
      return View(category);
    }
    //POST: Categories/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Category category)
    {
      if (category == null)
      {
        throw new ArgumentNullException(nameof(category));
      }
      if (ModelState.IsValid)
      {
        try
        {
          this.categoryService.Insert(category);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a category record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(category);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var category = await Task.Run(() =>
      {
        return new Category();
      });
      return Json(category, JsonRequestBehavior.AllowGet);
    }


    //GET: Categories/Edit/:id
    public ActionResult Edit(int id)
    {
      var category = this.categoryService.Find(id);
      if (category == null)
      {
        return HttpNotFound();
      }
      return View(category);
    }
    //POST: Categories/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Category category)
    {
      if (category == null)
      {
        throw new ArgumentNullException(nameof(category));
      }
      if (ModelState.IsValid)
      {
        category.TrackingState = TrackingState.Modified;
        try
        {
          this.categoryService.Update(category);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Category record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(category);
    }
    //删除当前记录
    //GET: Categories/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.categoryService.Queryable().Where(x => x.Id == id).DeleteAsync();
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
        await this.categoryService.Delete(id);
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
      var fileName = "categories_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.categoryService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    private void DisplaySuccessMessage(string msgText) => TempData["SuccessMessage"] = msgText;
    private void DisplayErrorMessage(string msgText) => TempData["ErrorMessage"] = msgText;

  }
}
