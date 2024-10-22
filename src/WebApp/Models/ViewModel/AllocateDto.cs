using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class AllocateDto
  {
    public int PurchaseOrderId { get; set; }
    public int InvId { get; set; }
    public decimal AllocateQty { get; set; }
    public string RefId { get; set; }
    public string ProductName { get; set; }
    public string Loc { get; set; }
  }
}