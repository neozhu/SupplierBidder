using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class BidRequestViewModel
  {
    public int PurchaseOrderId { get; set; }
    public string BrandName { get; set; }
    public string Spec { get; set; }
    public string Feature { get; set; }
    public decimal Qty { get; set; }
    public int? DeliveryCycle { get; set; }
  public decimal BiddingPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; }

    public int SupplierId { get; set; }
    public string UserName { get; set; }

  }
}