using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //联系人表
  public partial class Contact:Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "联系人名称", Description = "联系人名称")]
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "联系电话", Description = "联系电话")]
    [MaxLength(30)]
    public string PhoneNumber { get; set; }
    [Display(Name = "微信", Description = "微信")]
    [MaxLength(50)]
    public string WeChat { get; set; }
    [Display(Name = "其它", Description = "其它")]
    [MaxLength(150)]
    public string Other { get; set; }
  }
}