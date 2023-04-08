using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
  public class SummaryReportViewModel
  {
    public string status { get; set; }
    public int count { get; set; }
    public decimal qty { get; set; }
    public decimal total { get; set; }

  }
  public class SummaryMonthViewModel
  {
    public int year { get; set; }
    public int month { get; set; }
    public int count { get; set; }
    public decimal qty { get; set; }
    public decimal total { get; set; }

  }

  public class invmonthdata {
    public string date { get; set; }
    public string direct { get; set; }
    public decimal qty { get; set; }
    }

}