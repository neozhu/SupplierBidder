using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="BiddingMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>3/8/2020 7:58:10 AM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(BiddingMetadata))]
    public partial class Bidding
    {
    }

    public partial class BiddingMetadata
    {
        [Display(Name = "PurchaseOrder",Description ="采购单记录",Prompt = "采购单记录",ResourceType = typeof(resource.Bidding))]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Required(ErrorMessage = "Please enter : 流水号")]
        [Display(Name = "Id",Description ="流水号",Prompt = "流水号",ResourceType = typeof(resource.Bidding))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 投标时间")]
        [Display(Name = "BiddingDate",Description ="投标时间",Prompt = "投标时间",ResourceType = typeof(resource.Bidding))]
        public DateTime BiddingDate { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.Bidding))]
        [MaxLength(10)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单号")]
        [Display(Name = "PO",Description ="采购单号",Prompt = "采购单号",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string PO { get; set; }

        [Display(Name = "DemandedDate",Description ="要求到货日期",Prompt = "要求到货日期",ResourceType = typeof(resource.Bidding))]
        public DateTime DemandedDate { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "LineNum",Description ="序号",Prompt = "序号",ResourceType = typeof(resource.Bidding))]
        [MaxLength(3)]
        public string LineNum { get; set; }

        [Display(Name = "ProductNo",Description ="货号",Prompt = "货号",ResourceType = typeof(resource.Bidding))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 品名")]
        [Display(Name = "ProductName",Description ="品名",Prompt = "品名",ResourceType = typeof(resource.Bidding))]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Spec",Description ="规格",Prompt = "规格",ResourceType = typeof(resource.Bidding))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "BrandName",Description ="品牌",Prompt = "品牌",ResourceType = typeof(resource.Bidding))]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Bidding))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "Qty",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.Bidding))]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Please enter : 出价")]
        [Display(Name = "BiddingPrice",Description ="出价",Prompt = "出价",ResourceType = typeof(resource.Bidding))]
        public decimal BiddingPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 总价")]
        [Display(Name = "TotalPrice",Description ="总价",Prompt = "总价",ResourceType = typeof(resource.Bidding))]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Feature",Description ="参数",Prompt = "参数",ResourceType = typeof(resource.Bidding))]
        [MaxLength(100)]
        public string Feature { get; set; }

        [Display(Name = "Description",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Bidding))]
        [MaxLength(50)]
        public string Description { get; set; }

        [Display(Name = "Buyer",Description ="业务联系人",Prompt = "业务联系人",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string Buyer { get; set; }

        [Display(Name = "BuyerPhone",Description ="联系人电话",Prompt = "联系人电话",ResourceType = typeof(resource.Bidding))]
        [MaxLength(50)]
        public string BuyerPhone { get; set; }

        [Display(Name = "UserName",Description ="出价人",Prompt = "出价人",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter : 供应商ID")]
        [Display(Name = "SupplierId",Description ="供应商ID",Prompt = "供应商ID",ResourceType = typeof(resource.Bidding))]
        public int SupplierId { get; set; }

        [Display(Name = "SupplierName",Description ="供应商",Prompt = "供应商",ResourceType = typeof(resource.Bidding))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "DocNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string DocNo { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单")]
        [Display(Name = "PurchaseOrderId",Description ="采购单",Prompt = "采购单",ResourceType = typeof(resource.Bidding))]
        public int PurchaseOrderId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Bidding))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Bidding))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Bidding))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Bidding))]
        public int TenantId { get; set; }

    }

}
