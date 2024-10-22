using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class CloseRequestDto
  {
    public string PurchaseOrderId { get; set; }
    public string InvoiceNo { get; set; }
    public decimal InvoiceAmount { get; set; }
  }
}