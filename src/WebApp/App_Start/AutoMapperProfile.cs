using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApp.Models;
using WebApp.Models.ViewModel;
namespace WebApp
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      //var map = CreateMap<Order, Order>();
      _ = this.CreateMap<PurchaseOrder, BidViewModel>();
      _ = this.CreateMap<PurchaseOrder, Bidding>();
      _ = this.CreateMap<SORequestViewModel, ShippingOrder>();
      _ = this.CreateMap<PurchaseOrder, Inventory>().ReverseMap();
      _ = this.CreateMap<Inventory, Outbound>();
      _ = this.CreateMap<Inventory, Allocate>();
    }
  }
   
}