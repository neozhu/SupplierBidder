using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //发标记录,标注哪些人可以竞标
  public partial class Tender:Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    public string DocNo { get; set; }
    [Display(Name ="采购单ID",Description ="采购单ID")]
    public int PurchaseOrderId { get; set; }
    [Display(Name = "采购单类别", Description = "采购单类别")]
    [MaxLength(128)]
    public string Category { get; set; }
    [ForeignKey("PurchaseOrderId")]
    public PurchaseOrder PurchaseOrder { get; set; }
    [Display(Name = "供应商ID", Description = "供应商ID")]
    public int SupplierId { get; set; }
    [ForeignKey("SupplierId")]
    public Company Supplier { get; set; }
  
  }
}