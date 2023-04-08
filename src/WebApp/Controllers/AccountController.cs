#region Using

using System;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using LazyCache;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApp.App_Helpers;
using WebApp.Models;
using WebApp.Services;

#endregion

namespace WebApp.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private ApplicationRoleManager _roleManager;
    public ApplicationRoleManager RoleManager
    {
      get
      {
        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
      }
      private set
      {
        _roleManager = value;
      }
    }

    // TODO: This should be moved to the constructor of the controller in combination with a DependencyResolver setup
    // NOTE: You can use NuGet to find a strategy for the various IoC packages out there (i.e. StructureMap.MVC5)
    //private readonly UserManager _manager = UserManager.Create();
    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
        {
            get => this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => this._userManager = value;
        }
    private ApplicationSignInManager _signInManager;

    public ApplicationSignInManager SignInManager
        {
            get => this._signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => this._signInManager = value;
        }
    private readonly ICompanyService _companyService;
    private readonly IAppCache cache;
    private readonly NLog.ILogger logger;
    public AccountController(
       IAppCache cache,
      NLog.ILogger logger,
      ICompanyService companyService) {
      this._companyService = companyService;
      this.cache = cache;
      this.logger = logger;
      }

    // GET: /account/forgotpassword
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      return this.View();
    }
    [AllowAnonymous]
    public ActionResult VerifyCode()
    {
      var verifyCode = string.Empty;
      var bitmap = VerifyCodeHelper.CreateVerifyCode(out verifyCode);

      #region 缓存Key 
      var cache = new Cache();
      // 先用当前类的全名称拼接上字符串 “verifyCode” 作为缓存的key
      var verifyCodeKey = $"{this.GetType().FullName}_verifyCode";
      cache.Remove(verifyCodeKey);
      cache.Insert(verifyCodeKey, verifyCode);
      #endregion

      var memory = new MemoryStream();
      bitmap.Save(memory, ImageFormat.Gif);
      return File(memory.ToArray(), "image/gif");
    }

    // GET: /account/login
    [AllowAnonymous]

    public ActionResult Login(string returnUrl)
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      // Store the originating URL so we can attach it to a form field

      return this.View(new AccountLoginModel { ReturnUrl = returnUrl });
    }

    // POST: /account/login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(AccountLoginModel viewModel)
    {
      var cache = new Cache();
      var verifyCodeKey = $"{this.GetType().FullName}_verifyCode";
      var cacheobj = cache.Get(verifyCodeKey);
      if (cacheobj == null)
      {
        this.ModelState.AddModelError("", "验证码不正确.");
        return this.View(viewModel);
      }//不区分大小写比较验证码是否正确
      else
      {
        if(!viewModel.VerifyCode.Equals(cacheobj.ToString(), StringComparison.InvariantCultureIgnoreCase))
        {
          this.ModelState.AddModelError("", "验证码不正确.");
          return this.View(viewModel);
        }
      }
      cache.Remove(verifyCodeKey);

      // Ensure we have a valid viewModel to work with
      if (!this.ModelState.IsValid)
      {
        return this.View(viewModel);
      }

      // Verify if a user exists with the provided identity information

      // If a user was found
      var user = await this.UserManager.FindByNameAsync(viewModel.UserName);
      if (user != null)
      {
        var disableroles = new string[] { "仓库" };
        foreach(var role in disableroles)
        {
          var result =await this.UserManager.IsInRoleAsync(user.Id, role);
          if (result)
          {
            this.ModelState.AddModelError("", $"{role} 不允许登录.");
            return this.View(viewModel);
          }
        }

        switch (await this.SignInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, shouldLockout: false))
        {
          case SignInStatus.Success:
            // Then create an identity for it and sign it in
            await this.SignInAsync(user, viewModel.RememberMe);
            // If the user came from a specific page, redirect back to it
            this.logger.Info($"{viewModel.UserName}:登录成功");
            return this.RedirectToLocal(viewModel.ReturnUrl);
          case SignInStatus.LockedOut:
            return this.View("Lockout");
          case SignInStatus.RequiresVerification:
          case SignInStatus.Failure:
          default:
            this.ModelState.AddModelError("", "用户名或密码不对.");
            this.logger.Info($"{viewModel.UserName}:用户名或密码不对");
            return this.View(viewModel);
        }
      }

      // No existing user was found that matched the given criteria
      this.ModelState.AddModelError("", "用户名或密码不对.");

      // If we got this far, something failed, redisplay form
      return this.View(viewModel);
    }

    // GET: /account/error
    [AllowAnonymous]
    public ActionResult Error()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();

      return this.View();
    }

    // GET: /account/error
    [AllowAnonymous]
    public ActionResult Error404() => this.View();

    // GET: /account/register
    [AllowAnonymous]

    public ActionResult Register()
    {
      // We do not want to use any existing identity information
      this.EnsureLoggedOut();
      var data = this._companyService.Queryable().Select(x => new ListItem() { Value = x.Id.ToString(), Text = x.Name });

      this.ViewBag.companylist = data;
     var model = new AccountRegistrationModel
      {
        CompanyName = data.FirstOrDefault()?.Text
      };

      return this.View(model);
    }

    // POST: /account/register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
    {
      var data = this._companyService.Queryable().Select(x => new ListItem() { Value = x.Id.ToString(), Text = x.Name });
      this.ViewBag.companylist = data;

      // Ensure we have a valid viewModel to work with
      if (!this.ModelState.IsValid)
      {
        return this.View(viewModel);
      }

      // Try to create a user with the given identity
      try
      {
        // Prepare the identity with the provided information
        var user = new ApplicationUser
        {
          UserName = viewModel.Username,
          FullName = viewModel.FullName,
          CompanyName = viewModel.CompanyName,
          TenantId = viewModel.TenantId,
          Gender = viewModel.Gender,
          Email = viewModel.Email,
          PhoneNumber=viewModel.PhoneNumber,
          AccountType = "企业",
          AvatarsX50 = viewModel.Gender == 2 ? "femal1" : "male1",
          AvatarsX120 = viewModel.Gender == 2 ? "femal1_big" : "male1_big",

        };
        var result = await this.UserManager.CreateAsync(user, viewModel.Password);

        // If the user could not be created
        if (!result.Succeeded)
        {
          // Add all errors to the page so they can be used to display what went wrong
          this.AddErrors(result);

          return this.View(viewModel);
        }
        this.logger.Info($"注册成功【{user.UserName}】");
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/tenantid", user.TenantId.ToString()));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("CompanyName", user.CompanyName));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("FullName", user.FullName));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("AvatarsX50", user.AvatarsX50));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("AvatarsX120", user.AvatarsX120));
        await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("PhoneNumber", user.PhoneNumber));
        // If the user was able to be created we can sign it in immediately
        // Note: Consider using the email verification proces
        await this.SignInAsync(user, true);

        return this.RedirectToLocal();
      }
      catch (DbEntityValidationException ex)
      {
        // Add all errors to the page so they can be used to display what went wrong
        this.AddErrors(ex);

        return this.View(viewModel);
      }
    }
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="username"></param>
    /// <param name="currpwd"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public async Task<JsonResult> ChangePassword(string username, string currpwd, string pwd)
    {
      var user = await this.UserManager.FindByNameAsync(username);
      var result = await this.UserManager.ChangePasswordAsync(user.Id, currpwd, pwd);
      if (result.Succeeded)
      {
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      else
      {
        return Json(new { success = false, err = string.Join(",", result.Errors) }, JsonRequestBehavior.AllowGet);
      }
    }
    // POST: /account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Logout()
    {
      // First we clean the authentication ticket like always
      FormsAuthentication.SignOut();
      this.SignInManager.SignOut();
      // Second we clear the principal to ensure the user does not retain any authentication
      this.HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

      // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
      // this clears the Request.IsAuthenticated flag since this triggers a new request
      return this.RedirectToLocal();
    }
    public  ActionResult Profile() => this.View();
    private ActionResult RedirectToLocal(string returnUrl = "")
    {
      // If the return url starts with a slash "/" we assume it belongs to our site
      // so we will redirect to this "action"
      if (!returnUrl.IsNullOrWhiteSpace() && this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }

      // If we cannot verify if the url is local to our host we redirect to a default location
      return this.RedirectToAction("index", "home");
    }

    private void AddErrors(DbEntityValidationException exc)
    {
      foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
      {
        this.ModelState.AddModelError("", error);
      }
    }

    private void AddErrors(IdentityResult result)
    {
      // Add all errors that were returned to the page error collection
      foreach (var error in result.Errors)
      {
        this.ModelState.AddModelError("", error);
      }
    }

    private void EnsureLoggedOut()
    {
      // If the request is (still) marked as authenticated we send the user to the logout action
      if (this.Request.IsAuthenticated)
        this.Logout();
    }
    private async Task SignInAsync(ApplicationUser user, bool isPersistent) =>
        // Clear any lingering authencation data
        //FormsAuthentication.SignOut();
        //this.SignInManager.SignOut();

        // Create a claims based identity for the current user

        // Write the authentication cookie
        FormsAuthentication.SetAuthCookie(( await this.SignInManager.CreateUserIdentityAsync(user) ).Name, isPersistent);
    [HttpGet]
    public ActionResult SetCulture(string lang)
    {
      switch (lang.Trim())
      {
        case "en":
          CultureInfo.CurrentCulture = new CultureInfo("en-US");
          CultureInfo.CurrentUICulture = new CultureInfo("en-US");
          break;
        case "cn":
          CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
          CultureInfo.CurrentUICulture = new CultureInfo("zh-CN");
          break;
        case "tw":
          CultureInfo.CurrentCulture = new CultureInfo("zh-TW");
          CultureInfo.CurrentUICulture = new CultureInfo("zh-TW");
          break;
      }

      var cookie = new HttpCookie("culture", lang)
      {
        Expires = DateTime.Now.AddYears(1)
      };
      this.Response.Cookies.Add(cookie);
      return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }

    // GET: /account/lock
    [AllowAnonymous]
    public ActionResult Lock() => this.View();
  }
}