using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="AllocateMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>5/9/2021 7:38:46 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(AllocateMetadata))]
    public partial class Allocate
    {
    }

    public partial class AllocateMetadata
    {
        [Display(Name = "PurchaseOrder",Description ="采购单",Prompt = "采购单",ResourceType = typeof(resource.Allocate))]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Required(ErrorMessage = "Please enter : 主键")]
        [Display(Name = "Id",Description ="主键",Prompt = "主键",ResourceType = typeof(resource.Allocate))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单号")]
        [Display(Name = "PO",Description ="采购单号",Prompt = "采购单号",ResourceType = typeof(resource.Allocate))]
        [MaxLength(20)]
        public string PO { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "LineNum",Description ="序号",Prompt = "序号",ResourceType = typeof(resource.Allocate))]
        [MaxLength(3)]
        public string LineNum { get; set; }

        [Display(Name = "PODate",Description ="申请日期",Prompt = "申请日期",ResourceType = typeof(resource.Allocate))]
        public DateTime PODate { get; set; }

        [Display(Name = "ReceivedDate",Description ="收货日期",Prompt = "收货日期",ResourceType = typeof(resource.Allocate))]
        public DateTime ReceivedDate { get; set; }

        [Required(ErrorMessage = "Please enter : 领用日期")]
        [Display(Name = "OuboundDate",Description ="领用日期",Prompt = "领用日期",ResourceType = typeof(resource.Allocate))]
        public DateTime OuboundDate { get; set; }

        [Display(Name = "RecordUser",Description ="领用人",Prompt = "领用人",ResourceType = typeof(resource.Allocate))]
        [MaxLength(20)]
        public string RecordUser { get; set; }

        [Display(Name = "ProductNo",Description ="货号",Prompt = "货号",ResourceType = typeof(resource.Allocate))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 品名")]
        [Display(Name = "ProductName",Description ="品名",Prompt = "品名",ResourceType = typeof(resource.Allocate))]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Spec",Description ="规格",Prompt = "规格",ResourceType = typeof(resource.Allocate))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "BrandName",Description ="投标品牌",Prompt = "投标品牌",ResourceType = typeof(resource.Allocate))]
        [MaxLength(100)]
        public string BrandName { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Allocate))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please enter : 领用数量")]
        [Display(Name = "Qty",Description ="领用数量",Prompt = "领用数量",ResourceType = typeof(resource.Allocate))]
        public decimal Qty { get; set; }

        [Display(Name = "SupplierName",Description ="中标供应商",Prompt = "中标供应商",ResourceType = typeof(resource.Allocate))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "RefId",Description ="关联二级ID",Prompt = "关联二级ID",ResourceType = typeof(resource.Allocate))]
        [MaxLength(50)]
        public string RefId { get; set; }

        [Display(Name = "Remark",Description ="领说明",Prompt = "领说明",ResourceType = typeof(resource.Allocate))]
        [MaxLength(512)]
        public string Remark { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.Allocate))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Feature",Description ="参数",Prompt = "参数",ResourceType = typeof(resource.Allocate))]
        [MaxLength(500)]
        public string Feature { get; set; }

        [Display(Name = "Description",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Allocate))]
        [MaxLength(512)]
        public string Description { get; set; }

        [Display(Name = "PurchaseOrderId",Description ="采购单",Prompt = "采购单",ResourceType = typeof(resource.Allocate))]
        public int PurchaseOrderId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Allocate))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Allocate))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Allocate))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Allocate))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Allocate))]
        public int TenantId { get; set; }

    }

}
