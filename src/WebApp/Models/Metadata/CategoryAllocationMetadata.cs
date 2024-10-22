using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CategoryAllocationMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/20 13:32:28 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CategoryAllocationMetadata))]
    public partial class CategoryAllocation
    {
    }

    public partial class CategoryAllocationMetadata
    {
        [Display(Name = "Category",Description ="采购单类别",Prompt = "采购单类别",ResourceType = typeof(resource.CategoryAllocation))]
        public Category Category { get; set; }

        [Display(Name = "Company",Description ="合格供应商",Prompt = "合格供应商",ResourceType = typeof(resource.CategoryAllocation))]
        public Company Company { get; set; }

        [Required(ErrorMessage = "Please enter : 主键")]
        [Display(Name = "Id",Description ="主键",Prompt = "主键",ResourceType = typeof(resource.CategoryAllocation))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单类别")]
        [Display(Name = "CategoryId",Description ="采购单类别",Prompt = "采购单类别",ResourceType = typeof(resource.CategoryAllocation))]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单类别")]
        [Display(Name = "CategoryName",Description ="采购单类别",Prompt = "采购单类别",ResourceType = typeof(resource.CategoryAllocation))]
        [MaxLength(128)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter : 合格供应商")]
        [Display(Name = "CompanyId",Description ="合格供应商",Prompt = "合格供应商",ResourceType = typeof(resource.CategoryAllocation))]
        public int CompanyId { get; set; }

        [Display(Name = "CompanyName",Description ="厂商名称",Prompt = "厂商名称",ResourceType = typeof(resource.CategoryAllocation))]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CategoryAllocation))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 是否禁止")]
        [Display(Name = "IsDisabled",Description ="是否禁止",Prompt = "是否禁止",ResourceType = typeof(resource.CategoryAllocation))]
        public bool IsDisabled { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CategoryAllocation))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CategoryAllocation))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CategoryAllocation))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CategoryAllocation))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CategoryAllocation))]
        public int TenantId { get; set; }

    }

}
