using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Repository.Pattern.UnitOfWork;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  public class FileUploadController : Controller
  {

    private readonly ICodeItemService _codeService;
    private readonly IEmployeeService _empService;
    private readonly ICompanyService companyService;
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IPurchaseOrderService purchaseOrderService;
    private readonly NLog.ILogger logger;
    private readonly IContactService contactService;
    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get => this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      private set => this._userManager = value;
    }
    public FileUploadController(
       NLog.ILogger logger,
       IContactService contactService,
       ICompanyService companyService,
       IPurchaseOrderService purchaseOrderService,
            //IWorkService workService,
            IEmployeeService _empService, ICodeItemService _codeService, IUnitOfWorkAsync unitOfWork)
    {
      //_iBOMComponentService = iBOMComponentService;
      //_sKUService  = sKUService;
      //this.workService = workService;
      this.contactService = contactService;
      this.companyService = companyService;
      this._empService = _empService;
      this._unitOfWork = unitOfWork;
      this._codeService = _codeService;
      this.logger = logger;
      this.purchaseOrderService = purchaseOrderService;
    }
    //Excel上传导入接口
    [HttpPost]
    public async Task<JsonResult> Upload()
    {
      
      var watch = new Stopwatch();
      var uploadfilename = string.Empty;
      var newfileName = string.Empty;

      try
      {

        watch.Start();
        // 如果没有上传文件
        if (this.Request.Files.Count == 0)
        {
          return this.Json(new { success = false, message = "没有上传文件" }, JsonRequestBehavior.AllowGet);
        }
        var filedata = this.Request.Files[0];
        var model = this.Request.Form["model"];
        uploadfilename = System.IO.Path.GetFileName(filedata.FileName);
        var ext = System.IO.Path.GetExtension(filedata.FileName);
        var folder = this.Server.MapPath("~/UploadFiles");
        newfileName = $"{filedata.FileName.Replace(ext, "")}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{ext}";//重组成新的文件名

        if (!Directory.Exists(folder))
        {
          Directory.CreateDirectory(folder);
        }
        var virtualPath = Path.Combine(folder, newfileName);
        // 文件系统不能使用虚拟路径
        //string path = this.Server.MapPath(virtualPath);
        

        //Lastfilename = this.Request.Form["Lastfilename"];
        filedata.InputStream.Seek(0, SeekOrigin.Begin);
        //Lastfilename = this.Request.Form["Lastfilename"];
        var datatable = await NPOIHelper.GetDataTableFromExcelAsync(filedata.InputStream,ext);
        
        //if (fileType == "Work")
        //{
        //  this._unitOfWork.SetAutoDetectChangesEnabled(false);
        //  this.workService.ImportDataTable(datatable);
        //  this._unitOfWork.BulkSaveChanges();
        //  this._unitOfWork.SetAutoDetectChangesEnabled(true);
        //  //_unitOfWork.SaveChanges();
        //}
        if (model == "Employee")
        {
          this._unitOfWork.SetAutoDetectChangesEnabled(false);
          await this._empService.ImportDataTableAsync(datatable);
          await this._unitOfWork.SaveChangesAsync();
          this._unitOfWork.SetAutoDetectChangesEnabled(true);
          //_unitOfWork.SaveChanges();
        }
        if (model == "CodeItem")
        {
          this._unitOfWork.SetAutoDetectChangesEnabled(false);
          await this._codeService.ImportDataTableAsync(datatable);
          await this._unitOfWork.SaveChangesAsync();

          //_unitOfWork.SaveChanges();
          this._unitOfWork.SetAutoDetectChangesEnabled(true);
        }
        if (model == "PurchaseOrder")
        {

          await this.purchaseOrderService.ImportDataTableAsync(datatable);
          await this._unitOfWork.SaveChangesAsync();
        }
        if (model == "PurchaseOrder1")
        {

          await this.purchaseOrderService.ImportDataTableAsync1(datatable);
          await this._unitOfWork.SaveChangesAsync();
        }
        if (model == "Company")
        {

          await this.companyService.ImportDataTableAsync(datatable);
          await this._unitOfWork.SaveChangesAsync();
          //新建账号
          var suppliers = await this.companyService.Queryable().Where(x => x.Type == "供应商").ToListAsync();
          foreach (var supplier in suppliers)
          {
            var user = await this.UserManager.FindByNameAsync(supplier.Contect);
            if (user == null)
            {
              var newuser = new ApplicationUser
              {
                UserName = supplier.Contect,
                Email = supplier.Contect + "@139.com",
                FullName = supplier.Contect,
                Gender = 1,
                TenantId = supplier.Id,
                CompanyName = supplier.Name,
                PhoneNumber = supplier.PhoneNumber,
                AccountType = supplier.Type,
                AvatarsX50 = "male",
                AvatarsX120 = "male" + "_big"
              };
              var result =await this.UserManager.CreateAsync(newuser, supplier.PhoneNumber);
              if (result.Succeeded)
              {
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/tenantid", newuser.TenantId.ToString()));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("FullName", string.IsNullOrEmpty(newuser.FullName) ? "" : newuser.FullName));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("CompanyName", string.IsNullOrEmpty(newuser.CompanyName) ? "" : newuser.CompanyName));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, newuser.Email));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("AvatarsX50", newuser.AvatarsX50));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("AvatarsX120", newuser.AvatarsX120));
                this.UserManager.AddClaim(newuser.Id, new System.Security.Claims.Claim("PhoneNumber", string.IsNullOrEmpty(newuser.PhoneNumber) ? "" : newuser.PhoneNumber));
                if (newuser.AccountType == "供应商")
                {
                  this.UserManager.AddToRole(newuser.Id, "供应商");
                }
              }
            }
          }
          }
        if (model == "Contact")
        {

          await this.contactService.ImportDataTableAsync(datatable);
          await this._unitOfWork.SaveChangesAsync();

           
        }
        //if (fileType == "Product")
        //{
        //    _iBOMComponentService.ImportDataTable(datatable);
        //    _unitOfWork.SaveChanges();
        //}



        watch.Stop();
        //获取当前实例测量得出的总运行时间（以毫秒为单位）
        var elapsedTime = watch.ElapsedMilliseconds.ToString();
        filedata.InputStream.Position = 0;
        filedata.SaveAs(virtualPath);
        this.logger.Info($"{model} 文件名:{uploadfilename} 导入成功!");
        return this.Json(new { success = true, filename = newfileName, elapsedTime = elapsedTime }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.SqlClient.SqlException e)
      {
        this.logger.Error(e, $"文件名:{uploadfilename},{e.GetBaseException().Message}");
        return this.Json(new { success = false, filename = uploadfilename, message = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Infrastructure.DbUpdateException e)
      {
        this.logger.Error(e, $"文件名:{uploadfilename},{e.GetBaseException().Message}");
        return this.Json(new { success = false, filename = uploadfilename, message = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
      catch (System.Data.Entity.Validation.DbEntityValidationException e)
      {
        var errormessage = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage).Distinct());
        this.logger.Error(e, $"文件名:{uploadfilename},{errormessage}");
        return this.Json(new { success = false, filename = uploadfilename, message = errormessage }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        this.logger.Error(e, $"文件名:{uploadfilename},{e.GetBaseException().Message}");
        return this.Json(new { success = false, filename = uploadfilename, message = e.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
      }
    }


    [FileDownload]
    public async  Task<FileContentResult> Download(string file = "")
    {
      if (string.IsNullOrEmpty(file))
      {
        throw new ArgumentNullException($"input file name is empty!");
      }
      byte[] fileContent = null;
      var fileName = "";
      var mimeType = "";
      this.HttpContext.Response.AppendCookie(new HttpCookie("fileDownload", "true") { Path = "/" });

      var downloadFile = new FileInfo(this.Server.MapPath(file));
      if (downloadFile.Exists)
      {
        fileName = downloadFile.Name;
        mimeType = this.GetMimeType(downloadFile.Extension);
        fileContent = new byte[Convert.ToInt32(downloadFile.Length)];
        using (var fs = downloadFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          await fs.ReadAsync(fileContent, 0, Convert.ToInt32(downloadFile.Length));
        }
        return this.File(fileContent, mimeType, fileName);
      }
      else
      {
        throw new FileNotFoundException($"not found file {file}!");
      }
    }
    [HttpDelete]
    public async Task<JsonResult> Revert() {
      var req = Request.InputStream;
      var filename =await new StreamReader(req).ReadToEndAsync();
      if (!string.IsNullOrEmpty(filename))
      {
        var folder = this.Server.MapPath("~/UploadFiles");
        var path = Path.Combine(folder, filename);
        if (System.IO.File.Exists(path))
        {
          System.IO.File.Delete(path);
        }
      }
      return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public JsonResult Remove(string filename = "")
    {
      if (!string.IsNullOrEmpty(filename))
      {
        var folder = this.Server.MapPath("~/UploadFiles");
        var path = Path.Combine(folder, filename);
        if (System.IO.File.Exists(path))
        {
          System.IO.File.Delete(path);
        }
      }
      return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }

    private string GetMimeType(string fileExtensionStr)
    {
      var ContentTypeStr = "application/octet-stream";
      var fileExtension = fileExtensionStr.ToLower();
      switch (fileExtension)
      {
        case ".mp3":
          ContentTypeStr = "audio/mpeg3";
          break;
        case ".mpeg":
          ContentTypeStr = "video/mpeg";
          break;
        case ".jpg":
          ContentTypeStr = "image/jpeg";
          break;
        case ".bmp":
          ContentTypeStr = "image/bmp";
          break;
        case ".gif":
          ContentTypeStr = "image/gif";
          break;
        case ".doc":
          ContentTypeStr = "application/msword";
          break;
        case ".css":
          ContentTypeStr = "text/css";
          break;
        case ".html":
          ContentTypeStr = "text/html";
          break;
        case ".htm":
          ContentTypeStr = "text/html";
          break;
        case ".swf":
          ContentTypeStr = "application/x-shockwave-flash";
          break;
        case ".exe":
          ContentTypeStr = "application/octet-stream";
          break;
        case ".inf":
          ContentTypeStr = "application/x-texinfo";
          break;
        case ".xls":
        case ".xlsx":
          ContentTypeStr = "application/vnd.ms-excel";
          break;
        default:
          ContentTypeStr = "application/octet-stream";
          break;
      }
      return ContentTypeStr;
    }
  }
}
