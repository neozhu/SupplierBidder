using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
  public partial class Company : Entity
  {
    public Company()
    {
      Departments = new HashSet<Department>();
      Employees = new HashSet<Employee>();
    }
    [Key]
    public int Id { get; set; }
    [Display(Name = "厂商名称", Description = "厂商名称")]
    [MaxLength(50)]
    [Required]
    [Index(IsUnique = true)]
    public string Name { get; set; }
    [Display(Name = "供应商类别", Description = "供应商类别")]
    [MaxLength(20)]
    public string Category { get; set; }
    [Display(Name = "主要经营范围", Description = "主要经营范围")]
    [MaxLength(500)]
    public string Scope { get; set; }

    [Display(Name = "厂商类型", Description = "厂商类型")]
    [MaxLength(20)]
    [DefaultValue("供应商")]
    public string Type { get; set; }
    [Display(Name = "组织代码", Description = "组织代码")]
    [MaxLength(12)]
    public string Code { get; set; }
    [Display(Name = "地址", Description = "地址")]
    [MaxLength(50)]
    [DefaultValue("-")]
    public string Address { get; set; }
    [Display(Name = "主要联系人", Description = "主要联系人")]
    [MaxLength(12)]
    public string Contect { get; set; }
    [Display(Name = " 短信通知手机", Description = " 短信通知手机")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [Display(Name = "注册日期", Description = "注册日期")]
    [DefaultValue("now")]
    public DateTime RegisterDate { get; set; }
    [Display(Name = "雇员人数", Description = "雇员人数")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int EmployeeNumber { get => this.Employees.Count; }

    public virtual ICollection<Department> Departments { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
  }



}