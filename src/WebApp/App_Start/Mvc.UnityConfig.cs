using System;
using System.Data.Entity;
using AutoMapper;
using LazyCache;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using WebApp.Models;
using WebApp.Services;

namespace WebApp
{
  /// <summary>
  /// Specifies the Unity configuration for the main container.
  /// </summary>
  public class MvcUnityConfig
  {
    #region Unity Container
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
      var container = new UnityContainer();
      RegisterTypes(container);
      return container;
    });
    


    /// <summary>
    /// Configured Unity Container.
    /// </summary>
    public static IUnityContainer Container => container.Value;
    /// <summary>
    /// Gets the configured Unity container.
    /// </summary>
    public static IUnityContainer GetConfiguredContainer() => container.Value;
    #endregion

    /// <summary>Registers the type mappings with the Unity container.</summary>
    /// <param name="container">The unity container to configure.</param>
    /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
    /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
    public static void RegisterTypes(IUnityContainer container)
    {
      // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
      // container.LoadConfiguration();
      // TODO: Register your types here

      //添加cache功能
      container.RegisterType<IAppCache, CachingService>(new HierarchicalLifetimeManager(), new InjectionConstructor(CachingService.DefaultCacheProvider));
      //注册Nlog功能
      container.AddNewExtension<Unity.NLog.NLogExtension>();
      //注册Database功能
      container.RegisterInstance(SqlHelper2.DatabaseFactory.CreateDatabase(), InstanceLifetime.Singleton);
      container.RegisterInstance(SqlSugarFactory.CreateSqlSugarClient(), InstanceLifetime.Singleton);
      //注册automapper
      var config = new MapperConfiguration(cfg =>
      {
        //Create all maps here
        cfg.AddProfile(new AutoMapperProfile());
      });
      container.RegisterInstance(config.CreateMapper());
      //注册EF
      container.RegisterType<DbContext, StoreContext>(new PerRequestLifetimeManager());
      container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());

      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();
     
      container.RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>();
      container.RegisterType<IEmployeeService, EmployeeService>();
      container.RegisterType<IRepositoryAsync<Log>, Repository<Log>>();
       container.RegisterType<ILogService, LogService>();
      container.RegisterType<IRepositoryAsync<Company>, Repository<Company>>();
      container.RegisterType<ICompanyService, CompanyService>();

      container.RegisterType<IRepositoryAsync<Department>, Repository<Department>>();
      container.RegisterType<IDepartmentService, DepartmentService>();

    


      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();



      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
      container.RegisterType<IMenuItemService, MenuItemService>();
      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();

      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();
      container.RegisterType<IRoleMenuService, RoleMenuService>();
      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();



      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();

      container.RegisterType<IRepositoryAsync<Notification>, Repository<Notification>>();
      container.RegisterType<INotificationService, NotificationService>();
    


      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();
      container.RegisterType<ICodeItemService, CodeItemService>();


      container.RegisterType<IRepositoryAsync<PurchaseOrder>, Repository<PurchaseOrder>>();
      container.RegisterType<IPurchaseOrderService, PurchaseOrderService>();
      container.RegisterType<IRepositoryAsync<Tender>, Repository<Tender>>();
      container.RegisterType<ITenderService, TenderService>();
      container.RegisterType<IRepositoryAsync<Bidding>, Repository<Bidding>>();
      container.RegisterType<IBiddingService, BiddingService>();
      container.RegisterType<IRepositoryAsync<ShippingOrder>, Repository<ShippingOrder>>();
      container.RegisterType<IShippingOrderService, ShippingOrderService>();
      container.RegisterType<IRepositoryAsync<Contact>, Repository<Contact>>();
      container.RegisterType<IContactService, ContactService>();
      container.RegisterType<IRepositoryAsync<Category>, Repository<Category>>();
      container.RegisterType<ICategoryService, CategoryService>();
      container.RegisterType<IRepositoryAsync<CategoryAllocation>, Repository<CategoryAllocation>>();
      container.RegisterType<ICategoryAllocationService, CategoryAllocationService>();

      container.RegisterType<IRepositoryAsync<Inventory>, Repository<Inventory>>();
      container.RegisterType<IInventoryService, InventoryService>();
      container.RegisterType<IRepositoryAsync<Outbound>, Repository<Outbound>>();
      container.RegisterType<IOutboundService, OutboundService>();

      container.RegisterType<IRepositoryAsync<Allocate>, Repository<Allocate>>();
      container.RegisterType<IAllocateService, AllocateService>();
    }


  }
}
