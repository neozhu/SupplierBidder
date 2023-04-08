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
  //出价记录
  public partial class Bidding : Entity
  {
    [Key]
    [Display(Name = "流水号", Description = "流水号")]
    public int Id { get; set; }
    [Display(Name = "投标时间", Description = "投标时间")]
    [DefaultValue("now")]
    public DateTime BiddingDate { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("等待")]
    public string Status { get; set; }

    [Display(Name ="采购单号",Description ="采购单号")]
    [MaxLength(20)]
    [Required]
    public string  PO{ get;set;}
    [Display(Name = "要求到货日期", Description = "要求到货日期")]
    [DefaultValue("now")]
    public DateTime? DemandedDate { get; set; }

    [Display(Name = "序号", Description = "序号")]
    [MaxLength(3)]
    [Required]
    public string LineNum { get; set; }
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
    [Display(Name = "数量", Description = "数量")]
    public decimal Qty { get; set; }
    [Display(Name = "投标价", Description = "投标价")]
    public decimal BiddingPrice { get; set; }
    [Display(Name = "总价", Description = "总价")]
    public decimal TotalPrice { get; set; }

    [Display(Name = "参数", Description = "参数")]
    [MaxLength(100)]
    public string Feature { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string Description { get; set; }
    [Display(Name = "申请人", Description = "申请人")]
    [MaxLength(20)]
    public string Buyer { get; set; }
    [Display(Name = "联系人电话", Description = "联系人电话")]
    [MaxLength(50)]
    public string BuyerPhone { get; set; }
    [Display(Name = "出价人", Description = "出价人")]
    [MaxLength(20)]
    public string UserName { get; set; }
    [Display(Name = "供应商ID", Description = "供应商ID")]
    public int SupplierId { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(50)]
    public string SupplierName { get; set; }
    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    public string DocNo { get; set; }
 
    [Display(Name = "采购单", Description = "采购单")]
    public int PurchaseOrderId { get; set; }
   
    
    [Display(Name = "采购单记录", Description = "采购单记录")]
    [ForeignKey("PurchaseOrderId")]
    public PurchaseOrder PurchaseOrder { get; set; }


    [Display(Name = "出货单号", Description = "出货单号")]
    [DefaultValue(null)]
    [MaxLength(20)]
    public string SO { get; set; }
    [Display(Name = "发货日期", Description = "发货日期")]
    [DefaultValue(null)]
    public DateTime? ShippingDate { get; set; }
    [Display(Name = "收货日期", Description = "收货日期")]
    [DefaultValue(null)]
    public DateTime? ReceivedDate { get; set; }
    [Display(Name = "结案日期", Description = "结案日期")]
    [DefaultValue(null)]
    public DateTime? ClosedDate { get; set; }


    #region 2020-03-23需求新增字段
    [Display(Name = "申请部门", Description = "申请部门")]
    [MaxLength(30)]
    public string Dept { get; set; }
    [Display(Name = "申请科室", Description = "申请科室")]
    [MaxLength(30)]
    public string Section { get; set; }
    [Display(Name = "科研号", Description = "科研号或名称")]
    [MaxLength(30)]
    public string GrantNo { get; set; }
    #endregion
    #region 添加交货周期
    [Display(Name = "交货周期(天)", Description = "交货周期(天)")]
    public int? DeliveryCycle { get; set; }
    #endregion
  }
}