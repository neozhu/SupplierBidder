using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class SORequestViewModel
  {
    public string SO { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
    public string Status { get; set; }
    [Display(Name = "发货日期", Description = "发货日期")]
    public DateTime ShippingDate { get; set; }
    [Display(Name = "送货地址", Description = "送货地址")]
    [MaxLength(200)]
    public string DeliveryAddress { get; set; }

    [Display(Name = "业务联系人", Description = "业务联系人")]
    [MaxLength(20)]
    public string Buyer { get; set; }
    [Display(Name = "联系人电话", Description = "联系人电话")]
    [MaxLength(50)]
    public string BuyerPhone { get; set; }

    [Display(Name = "总件数", Description = "总件数")]
    public decimal Packages { get; set; }
    [Display(Name = "总金额", Description = "总金额")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "发票号", Description = "发票号")]
    [MaxLength(50)]
    public string InvoiceNo { get; set; }
    [Display(Name = "开票金额", Description = "开票金额")]
    public decimal InvoiceAmount { get; set; }
    [Display(Name = "税点", Description = "税点")]
    public decimal TaxRate { get; set; }
    [Display(Name = "税金", Description = "税金")]
    public decimal Tax { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(200)]
    public string Remark { get; set; }
    [Display(Name = "收货日期", Description = "收货日期")]

    public DateTime? ReceivedDate { get; set; }
    [Display(Name = "结案日期", Description = "结案日期")]

    public DateTime? ClosedDate { get; set; }
    [Display(Name = "发货人", Description = "发货人")]
    [MaxLength(20)]
    public string UserName { get; set; }
    [Display(Name = "供应商ID", Description = "供应商ID")]
    public int SupplierId { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(50)]
    public string SupplierName { get; set; }

    public int[] BiddingId { get; set; }
  }
}