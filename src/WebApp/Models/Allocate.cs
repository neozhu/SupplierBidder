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
  //领用记录
  public partial class Allocate : Entity
  {
    [Key]
    [Display(Name = "主键", Description = "主键")]
    public int Id { get; set; }
    [Display(Name ="采购单号",Description ="采购单号")]
    [MaxLength(20)]
    [Required]
    public string  PO{ get;set;}
    [Display(Name = "序号", Description = "序号")]
    [MaxLength(3)]
    [Required]
    public string LineNum { get; set; }
    [Display(Name = "申请日期", Description = "申请日期")]
    [DefaultValue("now")]
    public DateTime? PODate { get; set; }
    [Display(Name = "收货日期", Description = "收货日期")]
    [DefaultValue(null)]
    public DateTime? ReceivedDate { get; set; }
    [Display(Name = "分拨日期", Description = "分拨日期")]
    [DefaultValue("now")]
    public DateTime OuboundDate { get; set; }

    [Display(Name = "入库确认日期", Description = "入库确认日期")]
    public DateTime? ConfirmDate { get; set; }

    [Display(Name = "领用人", Description = "领用人")]
    [MaxLength(20)]
    public string RecordUser { get; set; }


    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string ProductNo { get; set; }
    [Display(Name = "品名", Description = "品名")]
    [MaxLength(100)]
    [Required]
    public string ProductName { get; set; }
    [Display(Name = "规格", Description = "规格")]
    [MaxLength(100)]
    public string Spec { get; set; }
    [Display(Name = "投标品牌", Description = "投标品牌")]
    [MaxLength(100)]
    public string BrandName { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "领用数量", Description = "领用数量")]
    public decimal Qty { get; set; }
    [Display(Name = "剩余数量", Description = "剩余数量")]
    public decimal StockQty { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(50)]
    public string SupplierName { get; set; }
    [Display(Name = "关联二级ID", Description = "关联二级ID")]
    [MaxLength(50)]
    public string RefId { get; set; }
    [Display(Name = "二级库品名", Description = "二级库品名")]
    [MaxLength(512)]
    public string Remark { get; set; }
    [Display(Name = "二级产品信息", Description = "二级产品信息")]
    [MaxLength(512)]
    public string ProductInfo { get; set; }
    [Display(Name = "二级仓库名", Description = "二级仓库名")]
    [MaxLength(128)]
    public string Loc { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    public string Status { get; set; }
    [Display(Name = "物品类型", Description = "物品类型")]
    [MaxLength(128)]
    public string GType { get; set; }

    [Display(Name = "参数", Description = "参数")]
    [MaxLength(500)]
    public string Feature { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(512)]
    public string Description { get; set; }
  
    [Display(Name = "采购单", Description = "采购单")]
    public int? PurchaseOrderId { get; set; }
    [Display(Name = "采购单", Description = "采购单")]
    [ForeignKey("PurchaseOrderId")]
    public PurchaseOrder PurchaseOrder { get; set; }

  }
}