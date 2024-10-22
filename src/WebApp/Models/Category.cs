using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //采购单类型
  public partial class Category:Entity
  {
    public Category()
    {
      this.Allocations=new HashSet<CategoryAllocation>();
    }
    [Key]
    [Display(Name ="主键",Description = "主键")]
    public int Id { get; set; }
    [Display(Name = "采购单类别", Description = "采购单类别")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(128)]
    public string Remark { get; set; }
    [Display(Name = "合格供应商数", Description = "合格供应商数")]
    public int AllowSuppliers { get; set; }


    public virtual ICollection<CategoryAllocation> Allocations { get; set; }
  }
}