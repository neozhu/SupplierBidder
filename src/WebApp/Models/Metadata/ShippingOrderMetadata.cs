using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ShippingOrderMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>3/10/2020 9:40:48 AM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ShippingOrderMetadata))]
    public partial class ShippingOrder
    {
    }

    public partial class ShippingOrderMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.ShippingOrder))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 出货单号")]
        [Display(Name = "SO",Description ="出货单号",Prompt = "出货单号",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(20)]
        public string SO { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(10)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 发货日期")]
        [Display(Name = "ShippingDate",Description ="发货日期",Prompt = "发货日期",ResourceType = typeof(resource.ShippingOrder))]
        public DateTime ShippingDate { get; set; }

        [Display(Name = "DeliveryAddress",Description ="送货地址",Prompt = "送货地址",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(200)]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Buyer",Description ="业务联系人",Prompt = "业务联系人",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(20)]
        public string Buyer { get; set; }

        [Display(Name = "BuyerPhone",Description ="联系人电话",Prompt = "联系人电话",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(50)]
        public string BuyerPhone { get; set; }

        [Required(ErrorMessage = "Please enter : 总件数")]
        [Display(Name = "Packages",Description ="总件数",Prompt = "总件数",ResourceType = typeof(resource.ShippingOrder))]
        public decimal Packages { get; set; }

        [Required(ErrorMessage = "Please enter : 总金额")]
        [Display(Name = "TotalAmount",Description ="总金额",Prompt = "总金额",ResourceType = typeof(resource.ShippingOrder))]
        public decimal TotalAmount { get; set; }

        [Display(Name = "InvoiceNo",Description ="发票号",Prompt = "发票号",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(50)]
        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Please enter : 开票金额")]
        [Display(Name = "InvoiceAmount",Description ="开票金额",Prompt = "开票金额",ResourceType = typeof(resource.ShippingOrder))]
        public decimal InvoiceAmount { get; set; }

        [Required(ErrorMessage = "Please enter : 税点")]
        [Display(Name = "TaxRate",Description ="税点",Prompt = "税点",ResourceType = typeof(resource.ShippingOrder))]
        public decimal TaxRate { get; set; }

        [Required(ErrorMessage = "Please enter : 税金")]
        [Display(Name = "Tax",Description ="税金",Prompt = "税金",ResourceType = typeof(resource.ShippingOrder))]
        public decimal Tax { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(200)]
        public string Remark { get; set; }

        [Display(Name = "ReceivedDate",Description ="收货日期",Prompt = "收货日期",ResourceType = typeof(resource.ShippingOrder))]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "ClosedDate",Description ="结案日期",Prompt = "结案日期",ResourceType = typeof(resource.ShippingOrder))]
        public DateTime ClosedDate { get; set; }

        [Display(Name = "UserName",Description ="发货人",Prompt = "发货人",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter : 供应商ID")]
        [Display(Name = "SupplierId",Description ="供应商ID",Prompt = "供应商ID",ResourceType = typeof(resource.ShippingOrder))]
        public int SupplierId { get; set; }

        [Display(Name = "SupplierName",Description ="供应商",Prompt = "供应商",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ShippingOrder))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ShippingOrder))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ShippingOrder))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.ShippingOrder))]
        public int TenantId { get; set; }

    }

}
