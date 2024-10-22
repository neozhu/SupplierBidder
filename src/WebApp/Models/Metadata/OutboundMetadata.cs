using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="OutboundMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/9/3 10:27:19 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(OutboundMetadata))]
    public partial class Outbound
    {
    }

    public partial class OutboundMetadata
    {
        [Required(ErrorMessage = "Please enter : 主键")]
        [Display(Name = "Id",Description ="主键",Prompt = "主键",ResourceType = typeof(resource.Outbound))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单号")]
        [Display(Name = "PO",Description ="采购单号",Prompt = "采购单号",ResourceType = typeof(resource.Outbound))]
        [MaxLength(20)]
        public string PO { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "LineNum",Description ="序号",Prompt = "序号",ResourceType = typeof(resource.Outbound))]
        [MaxLength(3)]
        public string LineNum { get; set; }

        [Display(Name = "PODate",Description ="申请日期",Prompt = "申请日期",ResourceType = typeof(resource.Outbound))]
        public DateTime PODate { get; set; }

        [Display(Name = "ReceivedDate",Description ="收货日期",Prompt = "收货日期",ResourceType = typeof(resource.Outbound))]
        public DateTime ReceivedDate { get; set; }

        [Required(ErrorMessage = "Please enter : 领用日期")]
        [Display(Name = "OuboundDate",Description ="领用日期",Prompt = "领用日期",ResourceType = typeof(resource.Outbound))]
        public DateTime OuboundDate { get; set; }

        [Display(Name = "RecordUser",Description ="领用人",Prompt = "领用人",ResourceType = typeof(resource.Outbound))]
        [MaxLength(20)]
        public string RecordUser { get; set; }

        [Display(Name = "ProductNo",Description ="货号",Prompt = "货号",ResourceType = typeof(resource.Outbound))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 品名")]
        [Display(Name = "ProductName",Description ="品名",Prompt = "品名",ResourceType = typeof(resource.Outbound))]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Spec",Description ="规格",Prompt = "规格",ResourceType = typeof(resource.Outbound))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "BrandName",Description ="建议品牌",Prompt = "建议品牌",ResourceType = typeof(resource.Outbound))]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Outbound))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please enter : 领用数量")]
        [Display(Name = "Qty",Description ="领用数量",Prompt = "领用数量",ResourceType = typeof(resource.Outbound))]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Please enter : 剩余数量")]
        [Display(Name = "StockQty",Description ="剩余数量",Prompt = "剩余数量",ResourceType = typeof(resource.Outbound))]
        public decimal StockQty { get; set; }

        [Required(ErrorMessage = "Please enter : 中标价格")]
        [Display(Name = "BidedPrice",Description ="中标价格",Prompt = "中标价格",ResourceType = typeof(resource.Outbound))]
        public decimal BidedPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 金额")]
        [Display(Name = "Amount",Description ="金额",Prompt = "金额",ResourceType = typeof(resource.Outbound))]
        public decimal Amount { get; set; }

        [Display(Name = "SupplierName",Description ="中标供应商",Prompt = "中标供应商",ResourceType = typeof(resource.Outbound))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "Feature",Description ="参数",Prompt = "参数",ResourceType = typeof(resource.Outbound))]
        [MaxLength(500)]
        public string Feature { get; set; }

        [Display(Name = "Description",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Outbound))]
        [MaxLength(512)]
        public string Description { get; set; }

        [Display(Name = "Remark",Description ="领用说明",Prompt = "领用说明",ResourceType = typeof(resource.Outbound))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Outbound))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Outbound))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Outbound))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Outbound))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Outbound))]
        public int TenantId { get; set; }

    }

}
