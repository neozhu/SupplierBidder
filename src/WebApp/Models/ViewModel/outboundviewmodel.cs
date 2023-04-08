using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class outboundviewmodel
  {
    public int PurchaseOrderId { get; set; }
    public int InvId { get; set; }
    public decimal OutboundQty { get; set; }
    public string Remark { get; set; }
    public string RecordUser { get; set; }

    public DateTime? OutboundDate { get; set; }
  }
}