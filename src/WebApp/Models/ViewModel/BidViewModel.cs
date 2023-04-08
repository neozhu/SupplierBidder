using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class BidViewModel
  {
    #region 采购单信息
    public int Id { get; set; }
    [Display(Name = "采购单号", Description = "采购单号")]
    [MaxLength(20)]

    public string PO { get; set; }
    [Display(Name = "下单日期", Description = "下单日期")]

    public DateTime PODate { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]
 
    public string Status { get; set; }

    [Display(Name = "要求到货日期", Description = "要求到货日期")]

    public DateTime? DemandedDate { get; set; }

    [Display(Name = "序号", Description = "序号")]
    [MaxLength(3)]

    public string LineNum { get; set; }
    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string ProductNo { get; set; }
    [Display(Name = "品名", Description = "品名")]
    [MaxLength(100)]

    public string ProductName { get; set; }
    [Display(Name = "规格", Description = "规格")]
    [MaxLength(100)]
    public string Spec { get; set; }
    [Display(Name = "品牌", Description = "品牌")]
    [MaxLength(100)]
    public string BrandName { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "数量", Description = "数量")]
    public decimal Qty { get; set; }
    [Display(Name = "控制价", Description = "控制价")]
    public decimal ControlledPrice { get; set; }

    [Display(Name = "参数", Description = "参数")]
    [MaxLength(100)]
    public string Feature { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string Description { get; set; }
    [Display(Name = "业务联系人", Description = "业务联系人")]
    [MaxLength(20)]
    public string Buyer { get; set; }
    [Display(Name = "联系人电话", Description = "联系人电话")]
    [MaxLength(50)]
    public string BuyerPhone { get; set; }
    [Display(Name = "发标日期", Description = "发标日期")]
 
    public DateTime? BiddingDate { get; set; }
    [Display(Name = "询价截止日期", Description = "询价截止日期")]

    public DateTime? DueDate { get; set; }

    [Display(Name = "发货日期", Description = "发货日期")]

    public DateTime? ShippingDate { get; set; }
    [Display(Name = "出货单号", Description = "出货单号")]

    [MaxLength(20)]
    public string SO { get; set; }
    [Display(Name = "发票号", Description = "发票号")]

    [MaxLength(50)]
    public string InvoiceNo { get; set; }

    [Display(Name = "开标日期", Description = "发货日期")]
   
    public DateTime? OpenDate { get; set; }

    [Display(Name = "收货日期", Description = "收货日期")]

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
    #endregion
    #region 出价信息
    [Display(Name = "投标时间", Description = "投标时间")]

    public DateTime? B_BiddingDate { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(10)]

  
    public string B_Status { get; set; }

    [Display(Name = "采购单号", Description = "采购单号")]
    [MaxLength(20)]

    public string B_PO { get; set; }
    [Display(Name = "要求到货日期", Description = "要求到货日期")]

    public DateTime? B_DemandedDate { get; set; }

    [Display(Name = "序号", Description = "序号")]
    [MaxLength(3)]

    public string B_LineNum { get; set; }
    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string B_ProductNo { get; set; }
    [Display(Name = "品名", Description = "品名")]
    [MaxLength(100)]

    public string B_ProductName { get; set; }
    [Display(Name = "规格", Description = "规格")]
    [MaxLength(100)]
    public string B_Spec { get; set; }
    [Display(Name = "品牌", Description = "品牌")]
    [MaxLength(100)]
    public string B_BrandName { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string B_Unit { get; set; }
    [Display(Name = "数量", Description = "数量")]
    public decimal B_Qty { get; set; }
    [Display(Name = "出价", Description = "出价")]
    public decimal B_BiddingPrice { get; set; }
    [Display(Name = "总价", Description = "总价")]
    public decimal B_TotalPrice { get; set; }

    [Display(Name = "参数", Description = "参数")]
    [MaxLength(100)]
    public string B_Feature { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string B_Description { get; set; }
    [Display(Name = "业务联系人", Description = "业务联系人")]
    [MaxLength(20)]
    public string B_Buyer { get; set; }
    [Display(Name = "联系人电话", Description = "联系人电话")]
    [MaxLength(50)]
    public string B_BuyerPhone { get; set; }
    [Display(Name = "出价人", Description = "出价人")]
    [MaxLength(20)]
    public string B_UserName { get; set; }
    [Display(Name = "供应商ID", Description = "供应商ID")]
    public int B_SupplierId { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(50)]
    public string B_SupplierName { get; set; }

    [Display(Name = "开价备注", Description = "开价备注")]
    public string B_BidRemark { get; set; }
    #endregion
  }
}