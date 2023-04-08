using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //出货单
  public partial class ShippingOrder:Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name ="出货单号",Description = "出货单号")]
    [MaxLength(20)]
    [Required]
    [Index(IsUnique =true)]
    public string SO { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("待发货")]
    public string Status { get; set; }
    [Display(Name = "发货日期", Description = "发货日期")]
    [DefaultValue("now")]
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
    [DefaultValue(null)]
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
    [DefaultValue(null)]
    public DateTime? ReceivedDate { get; set; }
    [Display(Name = "结案日期", Description = "结案日期")]
    [DefaultValue(null)]
    public DateTime? ClosedDate { get; set; }
    [Display(Name = "发货人", Description = "发货人")]
    [MaxLength(20)]
    public string UserName { get; set; }
    [Display(Name = "供应商ID", Description = "供应商ID")]
    public int SupplierId { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(50)]
    public string SupplierName { get; set; }
   
  }
}