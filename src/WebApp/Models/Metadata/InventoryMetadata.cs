using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="InventoryMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/9/3 10:07:24 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(InventoryMetadata))]
    public partial class Inventory
    {
    }

    public partial class InventoryMetadata
    {
        [Required(ErrorMessage = "Please enter : 主键")]
        [Display(Name = "Id",Description ="主键",Prompt = "主键",ResourceType = typeof(resource.Inventory))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单号")]
        [Display(Name = "PO",Description ="采购单号",Prompt = "采购单号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string PO { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "LineNum",Description ="序号",Prompt = "序号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(3)]
        public string LineNum { get; set; }

        [Display(Name = "PODate",Description ="申请日期",Prompt = "申请日期",ResourceType = typeof(resource.Inventory))]
        public DateTime PODate { get; set; }

        [Display(Name = "ReceivedDate",Description ="入库日期",Prompt = "入库日期",ResourceType = typeof(resource.Inventory))]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Receiver",Description ="收货人",Prompt = "收货人",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单状态")]
        [Display(Name = "Status",Description ="采购单状态",Prompt = "采购单状态",ResourceType = typeof(resource.Inventory))]
        [MaxLength(10)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 库存状态")]
        [Display(Name = "InvStatus",Description ="库存状态",Prompt = "库存状态",ResourceType = typeof(resource.Inventory))]
        [MaxLength(10)]
        public string InvStatus { get; set; }

        [Display(Name = "OutboundDate",Description ="出库日期",Prompt = "出库日期",ResourceType = typeof(resource.Inventory))]
        public DateTime OutboundDate { get; set; }

        [Required(ErrorMessage = "Please enter : 品名")]
        [Display(Name = "ProductName",Description ="品名",Prompt = "品名",ResourceType = typeof(resource.Inventory))]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Spec",Description ="规格",Prompt = "规格",ResourceType = typeof(resource.Inventory))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "BrandName",Description ="建议品牌",Prompt = "建议品牌",ResourceType = typeof(resource.Inventory))]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Inventory))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please enter : 库存数量")]
        [Display(Name = "Qty",Description ="库存数量",Prompt = "库存数量",ResourceType = typeof(resource.Inventory))]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Please enter : 控制单价")]
        [Display(Name = "ControlledPrice",Description ="控制单价",Prompt = "控制单价",ResourceType = typeof(resource.Inventory))]
        public decimal ControlledPrice { get; set; }

        [Display(Name = "Feature",Description ="参数",Prompt = "参数",ResourceType = typeof(resource.Inventory))]
        [MaxLength(500)]
        public string Feature { get; set; }

        [Display(Name = "Description",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Inventory))]
        [MaxLength(512)]
        public string Description { get; set; }

        [Display(Name = "Buyer",Description ="申请人",Prompt = "申请人",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string Buyer { get; set; }

        [Display(Name = "BuyerPhone",Description ="申请人电话",Prompt = "申请人电话",ResourceType = typeof(resource.Inventory))]
        [MaxLength(50)]
        public string BuyerPhone { get; set; }

        [Display(Name = "BiddingDate",Description ="发标日期",Prompt = "发标日期",ResourceType = typeof(resource.Inventory))]
        public DateTime BiddingDate { get; set; }

        [Display(Name = "DueDate",Description ="询价截止日期",Prompt = "询价截止日期",ResourceType = typeof(resource.Inventory))]
        public DateTime DueDate { get; set; }

        [Display(Name = "DemandedDate",Description ="要求到货日期",Prompt = "要求到货日期",ResourceType = typeof(resource.Inventory))]
        public DateTime DemandedDate { get; set; }

        [Display(Name = "ProductNo",Description ="货号",Prompt = "货号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Display(Name = "ShippingDate",Description ="发货日期",Prompt = "发货日期",ResourceType = typeof(resource.Inventory))]
        public DateTime ShippingDate { get; set; }

        [Display(Name = "SO",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string SO { get; set; }

        [Display(Name = "InvoiceNo",Description ="发票号",Prompt = "发票号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(50)]
        public string InvoiceNo { get; set; }

        [Display(Name = "OpenDate",Description ="开标日期",Prompt = "开标日期",ResourceType = typeof(resource.Inventory))]
        public DateTime OpenDate { get; set; }

        [Required(ErrorMessage = "Please enter : 出价次数")]
        [Display(Name = "BiddingTime",Description ="出价次数",Prompt = "出价次数",ResourceType = typeof(resource.Inventory))]
        public int BiddingTime { get; set; }

        [Required(ErrorMessage = "Please enter : 竞标人数")]
        [Display(Name = "BiddingUsers",Description ="竞标人数",Prompt = "竞标人数",ResourceType = typeof(resource.Inventory))]
        public int BiddingUsers { get; set; }

        [Required(ErrorMessage = "Please enter : 最低价")]
        [Display(Name = "MinPrice",Description ="最低价",Prompt = "最低价",ResourceType = typeof(resource.Inventory))]
        public decimal MinPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 最高价")]
        [Display(Name = "MaxPrice",Description ="最高价",Prompt = "最高价",ResourceType = typeof(resource.Inventory))]
        public decimal MaxPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 中标价格")]
        [Display(Name = "BidedPrice",Description ="中标价格",Prompt = "中标价格",ResourceType = typeof(resource.Inventory))]
        public decimal BidedPrice { get; set; }

        [Display(Name = "SupplierName",Description ="中标供应商",Prompt = "中标供应商",ResourceType = typeof(resource.Inventory))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "DocNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string DocNo { get; set; }

        [Display(Name = "ClosedDate",Description ="结案日期",Prompt = "结案日期",ResourceType = typeof(resource.Inventory))]
        public DateTime ClosedDate { get; set; }

        [Display(Name = "Category",Description ="采购单类别",Prompt = "采购单类别",ResourceType = typeof(resource.Inventory))]
        [MaxLength(128)]
        public string Category { get; set; }

        [Display(Name = "Dept",Description ="申请部门",Prompt = "申请部门",ResourceType = typeof(resource.Inventory))]
        [MaxLength(30)]
        public string Dept { get; set; }

        [Display(Name = "Section",Description ="申请科室",Prompt = "申请科室",ResourceType = typeof(resource.Inventory))]
        [MaxLength(30)]
        public string Section { get; set; }

        [Display(Name = "GrantNo",Description ="科研号或名称",Prompt = "科研号或名称",ResourceType = typeof(resource.Inventory))]
        [MaxLength(30)]
        public string GrantNo { get; set; }

        [Display(Name = "Reason",Description ="中标或废标原因",Prompt = "中标或废标原因",ResourceType = typeof(resource.Inventory))]
        [MaxLength(100)]
        public string Reason { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Inventory))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Inventory))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Inventory))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Inventory))]
        public int TenantId { get; set; }

    }

}
