using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CategoryMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/20 13:13:43 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    public partial class CategoryMetadata
    {
        [Required(ErrorMessage = "Please enter : 主键")]
        [Display(Name = "Id",Description ="主键",Prompt = "主键",ResourceType = typeof(resource.Category))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 采购单类别")]
        [Display(Name = "Name",Description ="采购单类别",Prompt = "采购单类别",ResourceType = typeof(resource.Category))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Category))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 合格供应商数")]
        [Display(Name = "AllowSuppliers",Description ="合格供应商数",Prompt = "合格供应商数",ResourceType = typeof(resource.Category))]
        public int AllowSuppliers { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Category))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Category))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Category))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Category))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Category))]
        public int TenantId { get; set; }

    }

}
