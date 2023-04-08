using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.Configuration.Attributes;

namespace WebApp.Models.ViewModel
{
  public class ReagentInfo
  {
    public string Id { get; set; }
    public string ProductName { get; set; }
    public string CasNO { get; set; }
    public string Volume { get; set; }
    public string Unit { get; set; }
    public string Purity { get; set; }
    public string SupplierName { get; set; }
    public string CustomerName { get; set; }
  }
  public class ReagentLoc
  {
    [Ignore]
    public string Id { get; set; }
    [Name("仓库名")]
    public string Loc { get; set; }
    [Name("商品编码")]
    public string ProductCode { get; set; }
    [Name("品名")]
    public string ProductName { get; set; }
    [Name("Cas NO")]
    public string CasNO { get; set; }
    [Name("体积")]
    public string Volume { get; set; }
    [Name("单位")]
    public string Unit { get; set; }
    [Name("浓度")]
    public string Purity { get; set; }
    [Name("供应商")]
    public string SupplierName { get; set; }
    [Name("厂商")]
    public string CustomerName { get; set; }
  
    [Name("库存数量")]
    public decimal Qty { get; set; }
  }
}