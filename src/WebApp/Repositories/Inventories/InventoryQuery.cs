using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using System.Web.WebPages;
using WebApp.Models;

namespace WebApp.Repositories
{
  /// <summary>
  /// File: InventoryQuery.cs
  /// Purpose: easyui datagrid filter query 
  /// Created Date: 2020/9/3 10:07:19
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class InventoryQuery : QueryObject<Inventory>
  {
    public InventoryQuery Withfilter(bool nz, IEnumerable<filterRule> filters)
    {
      if (nz == true)
      {
        And(x => x.Qty > 0);
      }
      if (filters != null)
      {
        foreach (var rule in filters)
        {
          if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Id == val);
                break;
              case "notequal":
                And(x => x.Id != val);
                break;
              case "less":
                And(x => x.Id < val);
                break;
              case "lessorequal":
                And(x => x.Id <= val);
                break;
              case "greater":
                And(x => x.Id > val);
                break;
              case "greaterorequal":
                And(x => x.Id >= val);
                break;
              default:
                And(x => x.Id == val);
                break;
            }
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
          }
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
          }
          if (rule.field == "PODate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.PODate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.PODate) <= 0);
            }
          }
          if (rule.field == "ReceivedDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.ReceivedDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.ReceivedDate) <= 0);
            }
          }
          if (rule.field == "Receiver" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Receiver.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "InvStatus" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvStatus.Contains(rule.value));
          }
          if (rule.field == "OutboundDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.OutboundDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.OutboundDate) <= 0);
            }
          }
          if (rule.field == "ProductName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.ProductName.Contains(rule.value));
          }
          if (rule.field == "Spec" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Spec.Contains(rule.value));
          }
          if (rule.field == "BrandName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BrandName.Contains(rule.value));
          }
          if (rule.field == "Unit" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Unit.Contains(rule.value));
          }
          if (rule.field == "Qty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Qty == val);
                break;
              case "notequal":
                And(x => x.Qty != val);
                break;
              case "less":
                And(x => x.Qty < val);
                break;
              case "lessorequal":
                And(x => x.Qty <= val);
                break;
              case "greater":
                And(x => x.Qty > val);
                break;
              case "greaterorequal":
                And(x => x.Qty >= val);
                break;
              default:
                And(x => x.Qty == val);
                break;
            }
          }
          if (rule.field == "ControlledPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.ControlledPrice == val);
                break;
              case "notequal":
                And(x => x.ControlledPrice != val);
                break;
              case "less":
                And(x => x.ControlledPrice < val);
                break;
              case "lessorequal":
                And(x => x.ControlledPrice <= val);
                break;
              case "greater":
                And(x => x.ControlledPrice > val);
                break;
              case "greaterorequal":
                And(x => x.ControlledPrice >= val);
                break;
              default:
                And(x => x.ControlledPrice == val);
                break;
            }
          }
          if (rule.field == "Feature" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Feature.Contains(rule.value));
          }
          if (rule.field == "Description" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Description.Contains(rule.value));
          }
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Buyer.Contains(rule.value));
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BuyerPhone.Contains(rule.value));
          }
          if (rule.field == "BiddingDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.BiddingDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.BiddingDate) <= 0);
            }
          }
          if (rule.field == "DueDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.DueDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.DueDate) <= 0);
            }
          }
          if (rule.field == "DemandedDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.DemandedDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.DemandedDate) <= 0);
            }
          }
          if (rule.field == "ProductNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.ProductNo.Contains(rule.value));
          }
          if (rule.field == "ShippingDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.ShippingDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.ShippingDate) <= 0);
            }
          }
          if (rule.field == "SO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SO.Contains(rule.value));
          }
          if (rule.field == "InvoiceNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvoiceNo.Contains(rule.value));
          }
          if (rule.field == "OpenDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.OpenDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.OpenDate) <= 0);
            }
          }
          if (rule.field == "BiddingTime" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingTime == val);
                break;
              case "notequal":
                And(x => x.BiddingTime != val);
                break;
              case "less":
                And(x => x.BiddingTime < val);
                break;
              case "lessorequal":
                And(x => x.BiddingTime <= val);
                break;
              case "greater":
                And(x => x.BiddingTime > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingTime >= val);
                break;
              default:
                And(x => x.BiddingTime == val);
                break;
            }
          }
          if (rule.field == "BiddingUsers" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingUsers == val);
                break;
              case "notequal":
                And(x => x.BiddingUsers != val);
                break;
              case "less":
                And(x => x.BiddingUsers < val);
                break;
              case "lessorequal":
                And(x => x.BiddingUsers <= val);
                break;
              case "greater":
                And(x => x.BiddingUsers > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingUsers >= val);
                break;
              default:
                And(x => x.BiddingUsers == val);
                break;
            }
          }
          if (rule.field == "MinPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.MinPrice == val);
                break;
              case "notequal":
                And(x => x.MinPrice != val);
                break;
              case "less":
                And(x => x.MinPrice < val);
                break;
              case "lessorequal":
                And(x => x.MinPrice <= val);
                break;
              case "greater":
                And(x => x.MinPrice > val);
                break;
              case "greaterorequal":
                And(x => x.MinPrice >= val);
                break;
              default:
                And(x => x.MinPrice == val);
                break;
            }
          }
          if (rule.field == "MaxPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.MaxPrice == val);
                break;
              case "notequal":
                And(x => x.MaxPrice != val);
                break;
              case "less":
                And(x => x.MaxPrice < val);
                break;
              case "lessorequal":
                And(x => x.MaxPrice <= val);
                break;
              case "greater":
                And(x => x.MaxPrice > val);
                break;
              case "greaterorequal":
                And(x => x.MaxPrice >= val);
                break;
              default:
                And(x => x.MaxPrice == val);
                break;
            }
          }
          if (rule.field == "BidedPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BidedPrice == val);
                break;
              case "notequal":
                And(x => x.BidedPrice != val);
                break;
              case "less":
                And(x => x.BidedPrice < val);
                break;
              case "lessorequal":
                And(x => x.BidedPrice <= val);
                break;
              case "greater":
                And(x => x.BidedPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BidedPrice >= val);
                break;
              default:
                And(x => x.BidedPrice == val);
                break;
            }
          }
          if (rule.field == "SupplierName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SupplierName.Contains(rule.value));
          }
          if (rule.field == "DocNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.DocNo.Contains(rule.value));
          }
          if (rule.field == "ClosedDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.ClosedDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.ClosedDate) <= 0);
            }
          }
          if (rule.field == "Category" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Category.Contains(rule.value));
          }
          if (rule.field == "Dept" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Dept.Contains(rule.value));
          }
          if (rule.field == "Section" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Section.Contains(rule.value));
          }
          if (rule.field == "GrantNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.GrantNo.Contains(rule.value));
          }
          if (rule.field == "Reason" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Reason.Contains(rule.value));
          }
          if (rule.field == "CreatedDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.CreatedDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.CreatedDate) <= 0);
            }
          }
          if (rule.field == "CreatedBy" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.CreatedBy.Contains(rule.value));
          }
          if (rule.field == "LastModifiedDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.LastModifiedDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.LastModifiedDate) <= 0);
            }
          }
          if (rule.field == "LastModifiedBy" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LastModifiedBy.Contains(rule.value));
          }
          if (rule.field == "TenantId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TenantId == val);
                break;
              case "notequal":
                And(x => x.TenantId != val);
                break;
              case "less":
                And(x => x.TenantId < val);
                break;
              case "lessorequal":
                And(x => x.TenantId <= val);
                break;
              case "greater":
                And(x => x.TenantId > val);
                break;
              case "greaterorequal":
                And(x => x.TenantId >= val);
                break;
              default:
                And(x => x.TenantId == val);
                break;
            }
          }

        }
      }
      return this;
    }
  }
}
