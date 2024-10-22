using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //发标信息
  public partial class PurchaseOrder : Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name ="采购单号",Description ="采购单号")]
    [MaxLength(20)]
    [Required]
    public string  PO{ get;set;}
    [Display(Name = "申请日期", Description = "申请日期")]
    [DefaultValue("now")]
    public DateTime PODate { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
    [Required]
    [DefaultValue("待发标")]
    public string Status { get; set; }

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
    [Display(Name = "建议品牌", Description = "建议品牌")]
    [MaxLength(100)]
    public string BrandName { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "批准量", Description = "批准量")]
    public decimal Qty { get; set; }
    [Display(Name = "控制单价", Description = "控制单价")]
    public decimal ControlledPrice { get; set; }

    [Display(Name = "参数", Description = "参数")]
    [MaxLength(500)]
    public string Feature { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(512)]
    public string Description { get; set; }
    [Display(Name = "申请人", Description = "申请人")]
    [MaxLength(20)]
    public string Buyer { get; set; }
    [Display(Name = "申请人电话", Description = "申请人电话")]
    [MaxLength(50)]
    public string BuyerPhone { get; set; }
    [Display(Name = "发标日期", Description = "发标日期")]
    [DefaultValue(null)]
    public DateTime? BiddingDate { get; set; }
    [Display(Name = "询价截止日期", Description = "询价截止日期")]
    [DefaultValue(null)]
    public DateTime? DueDate { get; set; }

    [Display(Name = "发货日期", Description = "发货日期")]
    [DefaultValue(null)]
    public DateTime? ShippingDate { get; set; }
    [Display(Name = "出货单号", Description = "出货单号")]
    [DefaultValue(null)]
    [MaxLength(20)]
    public string SO { get; set; }
    [Display(Name = "发票号", Description = "发票号")]
    [DefaultValue(null)]
    [MaxLength(50)]
    public string InvoiceNo { get; set; }

    [Display(Name = "开标日期", Description = "开标日期")]
    [DefaultValue(null)]
    public DateTime? OpenDate { get; set; }

    [Display(Name = "收货日期", Description = "收货日期")]
    [DefaultValue(null)]
    public DateTime? ReceivedDate { get; set; }

    [Display(Name = "出价次数", Description = "出价次数")]
    public int BiddingTime { get; set; }
    [Display(Name = "竞标人数", Description = "竞标人数")]
    public int BiddingUsers { get; set; }
    [Display(Name = "最低价", Description = "最低价")]
    public decimal MinPrice { get; set; }
    [Display(Name = "最高价", Description = "最高价")]
    public decimal MaxPrice { get; set; }
    [Display(Name = "中标价格", Description = "中标价格")]
    public decimal BidedPrice { get; set; }
    [Display(Name = "中标供应商", Description = "中标供应商")]
    [MaxLength(50)]
    public string SupplierName { get; set; }

    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    public string DocNo { get; set; }
    [Display(Name = "结案日期", Description = "结案日期")]
    [DefaultValue(null)]
    public DateTime? ClosedDate { get; set; }
    [Display(Name = "采购单类别", Description = "采购单类别")]
    [MaxLength(128)]
    public string Category { get; set; }
    public PurchaseOrder()
    {
      this.Tenders = new HashSet<Tender>();
    }

    public virtual ICollection<Tender> Tenders { get; set; }

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

    #region 新增中标原因
    [Display(Name = "中标或废标原因", Description = "中标或废标原因")]
    [MaxLength(100)]
    public string Reason { get; set; }
    #endregion

    #region 新增收货操作相关字段
    [Display(Name = "收货数量", Description = "收货数量")]
    public decimal? ReceiptQty { get; set; }
    [Display(Name = "未收货数量", Description = "未收货数量")]
    public decimal? OpenQty { get; set; }
    [Display(Name = "发票金额", Description = "发票金额")]
    public decimal? InvoiceAmount { get; set; }
    #endregion
  }
}