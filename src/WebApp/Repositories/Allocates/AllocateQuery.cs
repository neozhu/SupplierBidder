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
  /// File: AllocateQuery.cs
  /// Purpose: easyui datagrid filter query 
  /// Created Date: 5/9/2021 7:43:18 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class AllocateQuery : QueryObject<Allocate>
  {
    public AllocateQuery Withfilter(IEnumerable<filterRule> filters)
    {
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
          if (rule.field == "ConfirmDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.ConfirmDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.ConfirmDate) <= 0);
            }
          }
          if (rule.field == "OuboundDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.OuboundDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.OuboundDate) <= 0);
            }
          }
          if (rule.field == "RecordUser" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.RecordUser.Contains(rule.value));
          }
          if (rule.field == "ProductNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.ProductNo.Contains(rule.value));
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
          if (rule.field == "SupplierName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SupplierName.Contains(rule.value));
          }
          if (rule.field == "RefId" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.RefId.Contains(rule.value));
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Remark.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "Feature" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Feature.Contains(rule.value));
          }
          if (rule.field == "Description" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Description.Contains(rule.value));
          }
          if (rule.field == "PurchaseOrderId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.PurchaseOrderId == val);
                break;
              case "notequal":
                And(x => x.PurchaseOrderId != val);
                break;
              case "less":
                And(x => x.PurchaseOrderId < val);
                break;
              case "lessorequal":
                And(x => x.PurchaseOrderId <= val);
                break;
              case "greater":
                And(x => x.PurchaseOrderId > val);
                break;
              case "greaterorequal":
                And(x => x.PurchaseOrderId >= val);
                break;
              default:
                And(x => x.PurchaseOrderId == val);
                break;
            }
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
    public AllocateQuery ByPurchaseOrderIdWithfilter(int purchaseorderid, IEnumerable<filterRule> filters)
    {
      And(x => x.PurchaseOrderId == purchaseorderid);
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
            if (rule.op == "equal")
            {
              And(x => x.PO == rule.value);
            }
            else
            {
              And(x => x.PO.Contains(rule.value));
            }
          }
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.LineNum == rule.value);
            }
            else
            {
              And(x => x.LineNum.Contains(rule.value));
            }
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
          if (rule.field == "OuboundDate" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "between")
            {
              var datearray = rule.value.Split(new char[] { '-' });
              var start = Convert.ToDateTime(datearray[0]);
              var end = Convert.ToDateTime(datearray[1]);

              And(x => SqlFunctions.DateDiff("d", start, x.OuboundDate) >= 0);
              And(x => SqlFunctions.DateDiff("d", end, x.OuboundDate) <= 0);
            }
          }
          if (rule.field == "RecordUser" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.RecordUser == rule.value);
            }
            else
            {
              And(x => x.RecordUser.Contains(rule.value));
            }
          }
          if (rule.field == "ProductNo" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.ProductNo == rule.value);
            }
            else
            {
              And(x => x.ProductNo.Contains(rule.value));
            }
          }
          if (rule.field == "ProductName" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.ProductName == rule.value);
            }
            else
            {
              And(x => x.ProductName.Contains(rule.value));
            }
          }
          if (rule.field == "Spec" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Spec == rule.value);
            }
            else
            {
              And(x => x.Spec.Contains(rule.value));
            }
          }
          if (rule.field == "BrandName" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.BrandName == rule.value);
            }
            else
            {
              And(x => x.BrandName.Contains(rule.value));
            }
          }
          if (rule.field == "Unit" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Unit == rule.value);
            }
            else
            {
              And(x => x.Unit.Contains(rule.value));
            }
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
          if (rule.field == "SupplierName" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.SupplierName == rule.value);
            }
            else
            {
              And(x => x.SupplierName.Contains(rule.value));
            }
          }
          if (rule.field == "RefId" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.RefId == rule.value);
            }
            else
            {
              And(x => x.RefId.Contains(rule.value));
            }
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Remark == rule.value);
            }
            else
            {
              And(x => x.Remark.Contains(rule.value));
            }
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Status == rule.value);
            }
            else
            {
              And(x => x.Status.Contains(rule.value));
            }
          }
          if (rule.field == "Feature" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Feature == rule.value);
            }
            else
            {
              And(x => x.Feature.Contains(rule.value));
            }
          }
          if (rule.field == "Description" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Description == rule.value);
            }
            else
            {
              And(x => x.Description.Contains(rule.value));
            }
          }
          if (rule.field == "PurchaseOrderId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.PurchaseOrderId == val);
                break;
              case "notequal":
                And(x => x.PurchaseOrderId != val);
                break;
              case "less":
                And(x => x.PurchaseOrderId < val);
                break;
              case "lessorequal":
                And(x => x.PurchaseOrderId <= val);
                break;
              case "greater":
                And(x => x.PurchaseOrderId > val);
                break;
              case "greaterorequal":
                And(x => x.PurchaseOrderId >= val);
                break;
              default:
                And(x => x.PurchaseOrderId == val);
                break;
            }
          }
        }
      }
      return this;
    }
  }
}
