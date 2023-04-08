using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="TenderMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>3/7/2020 9:41:53 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(TenderMetadata))]
    public partial class Tender
    {
    }

    public partial class TenderMetadata
    {
        [Display(Name = "PurchaseOrder",Description ="PurchaseOrder",Prompt = "PurchaseOrder",ResourceType = typeof(resource.Tender))]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Display(Name = "Supplier",Description ="Supplier",Prompt = "Supplier",ResourceType = typeof(resource.Tender))]
        public Company Supplier { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.Tender))]
        public int Id { get; set; }

        [Display(Name = "DocNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.Tender))]
        [MaxLength(20)]
        public string DocNo { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单ID")]
        [Display(Name = "PurchaseOrderId",Description ="采购单ID",Prompt = "采购单ID",ResourceType = typeof(resource.Tender))]
        public int PurchaseOrderId { get; set; }

        [Required(ErrorMessage = "Please enter : 供应商ID")]
        [Display(Name = "SupplierId",Description ="供应商ID",Prompt = "供应商ID",ResourceType = typeof(resource.Tender))]
        public int SupplierId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Tender))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Tender))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Tender))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Tender))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Tender))]
        public int TenantId { get; set; }

    }

}
