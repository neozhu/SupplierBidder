using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class BiddingViewModel
  {
    public int[] PurcshaseOrderId { get; set; }
    public int[] SupplierId { get; set; }
    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    public string DocNo { get; set; }
    [Display(Name = "发标日期", Description = "发标日期")]
    [DefaultValue(null)]
    public DateTime? BiddingDate { get; set; }
    [Display(Name = "询价截止日期", Description = "询价截止日期")]
    [DefaultValue(null)]
    public DateTime? DueDate { get; set; }
  }
}