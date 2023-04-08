using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LazyCache;
using WebApp.App_Helpers.third_party.api;
using WebApp.Models;

namespace WebApp.Controllers
{
  [Authorize]
  [RoutePrefix("Home")]
  public class HomeController : Controller
  {
    private readonly IAppCache cache;
    private readonly IMapper mapper;
    private readonly NLog.ILogger logger;
    private readonly SqlHelper2.IDatabaseAsync db;

    public HomeController(
      NLog.ILogger logger,
      SqlHelper2.IDatabaseAsync db,
      IAppCache cache, IMapper mapper) {

      this.cache = cache;
      this.mapper = mapper;
      this.logger = logger;

      this.db = db;
    }

    public ActionResult Index()
    {
      //throw new Exception();
      //throw new Exception("new throw!");
      //var a =this.db.ExecuteScalar<int>("select count(1) from tb1");
      //this.cache.GetOrAdd("key", () => {
      //  return DateTime.Now;
      //  });
      //var code1 = PublicPara.Data.CodeListSet.CLS["unit"].Code2Value("001");
      //var code2 = PublicPara.Data.CodeListSet.CLS["Level"].Code2Value("Info");

      return this.View();
    }

    public ActionResult About()
    {
      this.ViewBag.Message = "Your application description page.";

      return this.View();
    }

    public ActionResult GetTime() =>
        //ViewBag.Message = "Your application description page.";

        this.View();
    public ActionResult BlankPage() => this.View();
    public ActionResult AgileBoard() => this.View();


    public ActionResult Contact()
    {
      this.ViewBag.Message = "Your contact page.";

      return this.View();
    }
    public ActionResult Chat() => this.View();




  }
}