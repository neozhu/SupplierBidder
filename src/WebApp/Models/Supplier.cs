using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //供应商表
  public partial class Supplier:Entity
  {
    [Key]
    public int Id { get; set; }
  }
}