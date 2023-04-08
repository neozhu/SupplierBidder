using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //采购单类型分配合格供应商关系
  public partial class CategoryAllocation:Entity
  {
    [Key]
    [Display(Name = "主键", Description = "主键")]
    public int Id { get; set; }
    [Display(Name = "采购类别", Description = "采购类别")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [Display(Name = "采购类别", Description = "采购类别")]
    public Category Category { get; set; }
    [Display(Name = "采购类别", Description = "采购类别")]
    [MaxLength(128)]
    [Required]
    public string CategoryName { get; set; }
    
    [Display(Name = "合格供应商", Description = "合格供应商")]
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    [Display(Name = "合格供应商", Description = "合格供应商")]
    public Company Company { get; set; }

    [Display(Name = "合格供应商", Description = "厂商名称")]
    [MaxLength(50)]
    public string CompanyName { get; set; }

    [Display(Name = "备注", Description = "备注")]
    [MaxLength(128)]
    public string Remark { get; set; }
    [Display(Name = "是否禁止", Description = "是否禁止")]
    public bool IsDisabled { get; set; }
  }
}