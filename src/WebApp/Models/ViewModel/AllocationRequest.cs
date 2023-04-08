using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class AllocationRequest
  {
    public int CategoryId { get; set; }
    public int[] SupplierId { get; set; }
  }
}