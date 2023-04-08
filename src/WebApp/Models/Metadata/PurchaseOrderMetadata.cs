using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="PurchaseOrderMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>3/7/2020 7:44:55 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(PurchaseOrderMetadata))]
    public partial class PurchaseOrder
    {
    }

    public partial class PurchaseOrderMetadata
    {
        [Display(Name = "Tenders",Description ="Tenders",Prompt = "Tenders",ResourceType = typeof(resource.PurchaseOrder))]
        public Tender Tenders { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.PurchaseOrder))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单号")]
        [Display(Name = "PO",Description ="采购单号",Prompt = "采购单号",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(20)]
        public string PO { get; set; }

        [Required(ErrorMessage = "Please enter : 下单日期")]
        [Display(Name = "PODate",Description ="下单日期",Prompt = "下单日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime PODate { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(10)]
        public string Status { get; set; }

        [Display(Name = "DemandedDate",Description ="要求到货日期",Prompt = "要求到货日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime DemandedDate { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "LineNum",Description ="序号",Prompt = "序号",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(3)]
        public string LineNum { get; set; }

        [Display(Name = "ProductNo",Description ="货号",Prompt = "货号",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 品名")]
        [Display(Name = "ProductName",Description ="品名",Prompt = "品名",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Spec",Description ="规格",Prompt = "规格",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "BrandName",Description ="品牌",Prompt = "品牌",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "Qty",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.PurchaseOrder))]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Please enter : 控制价")]
        [Display(Name = "ControlledPrice",Description ="控制价",Prompt = "控制价",ResourceType = typeof(resource.PurchaseOrder))]
        public decimal ControlledPrice { get; set; }

        [Display(Name = "Feature",Description ="参数",Prompt = "参数",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(100)]
        public string Feature { get; set; }

        [Display(Name = "Description",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(50)]
        public string Description { get; set; }

        [Display(Name = "Buyer",Description ="业务联系人",Prompt = "业务联系人",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(20)]
        public string Buyer { get; set; }

        [Display(Name = "BuyerPhone",Description ="联系人电话",Prompt = "联系人电话",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(50)]
        public string BuyerPhone { get; set; }

        [Display(Name = "BiddingDate",Description ="发标日期",Prompt = "发标日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime BiddingDate { get; set; }

        [Display(Name = "DueDate",Description ="截至日期",Prompt = "截至日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime DueDate { get; set; }

        [Display(Name = "ShippingDate",Description ="发货日期",Prompt = "发货日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime ShippingDate { get; set; }

        [Display(Name = "SO",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(20)]
        public string SO { get; set; }

        [Display(Name = "InvoiceNo",Description ="发票号",Prompt = "发票号",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(50)]
        public string InvoiceNo { get; set; }

        [Display(Name = "OpenDate",Description ="发货日期",Prompt = "发货日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime OpenDate { get; set; }

        [Display(Name = "ReceivedDate",Description ="收货日期",Prompt = "收货日期",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime ReceivedDate { get; set; }

        [Required(ErrorMessage = "Please enter : 出价次数")]
        [Display(Name = "BiddingTime",Description ="出价次数",Prompt = "出价次数",ResourceType = typeof(resource.PurchaseOrder))]
        public int BiddingTime { get; set; }

        [Required(ErrorMessage = "Please enter : 竞标人数")]
        [Display(Name = "BiddingUsers",Description ="竞标人数",Prompt = "竞标人数",ResourceType = typeof(resource.PurchaseOrder))]
        public int BiddingUsers { get; set; }

        [Required(ErrorMessage = "Please enter : 最低价")]
        [Display(Name = "MinPrice",Description ="最低价",Prompt = "最低价",ResourceType = typeof(resource.PurchaseOrder))]
        public decimal MinPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 最高价")]
        [Display(Name = "MaxPrice",Description ="最高价",Prompt = "最高价",ResourceType = typeof(resource.PurchaseOrder))]
        public decimal MaxPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 中标价格")]
        [Display(Name = "BidedPrice",Description ="中标价格",Prompt = "中标价格",ResourceType = typeof(resource.PurchaseOrder))]
        public decimal BidedPrice { get; set; }

        [Display(Name = "SupplierName",Description ="中标供应商",Prompt = "中标供应商",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.PurchaseOrder))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.PurchaseOrder))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.PurchaseOrder))]
        public int TenantId { get; set; }

    }

}
