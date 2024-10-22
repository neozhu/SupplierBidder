using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class ReceiptDto
  {
    public int PurchaseOrderId { get; set; }
    public decimal ReceiptQty { get; set; }
    public string SupplierName { get; set; }
    public string SO { get; set; }
  }
}