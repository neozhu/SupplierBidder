using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ContactMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>3/23/2020 7:56:55 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ContactMetadata))]
    public partial class Contact
    {
    }

    public partial class ContactMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.Contact))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 联系人名称")]
        [Display(Name = "Name",Description ="联系人名称",Prompt = "联系人名称",ResourceType = typeof(resource.Contact))]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "PhoneNumber",Description ="联系电话",Prompt = "联系电话",ResourceType = typeof(resource.Contact))]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Display(Name = "WeChat",Description ="微信",Prompt = "微信",ResourceType = typeof(resource.Contact))]
        [MaxLength(50)]
        public string WeChat { get; set; }

        [Display(Name = "Other",Description ="其它",Prompt = "其它",ResourceType = typeof(resource.Contact))]
        [MaxLength(150)]
        public string Other { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Contact))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Contact))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Contact))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Contact))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Contact))]
        public int TenantId { get; set; }

    }

}
