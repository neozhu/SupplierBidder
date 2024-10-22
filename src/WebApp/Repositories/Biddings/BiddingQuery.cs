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
  /// File: BiddingQuery.cs
  /// Purpose: easyui datagrid filter query 
  /// Created Date: 3/8/2020 7:58:03 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class BiddingQuery : QueryObject<Bidding>
  {
    public BiddingQuery WithShippingfilter(int supplierid, IEnumerable<filterRule> filters)
    {
      var array = new string[] { "已确认" };
      this.And(x => array.Contains(x.Status));
      this.And(x => x.SupplierId == supplierid);
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
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
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
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
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.UserName.Contains(rule.value));
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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

    public BiddingQuery WithOutBidfilter(int supplierid,IEnumerable<filterRule> filters)
    {
      //var array = new string[] { "中标", "废标" };
      var array = new string[] { "中标" };
      this.And(x => array.Contains(x.Status));
      this.And(x => x.SupplierId == supplierid);
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
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
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
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
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.UserName.Contains(rule.value));
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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

    public BiddingQuery Withfilter(IEnumerable<filterRule> filters)
    {
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
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
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
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
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.UserName.Contains(rule.value));
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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

    public BiddingQuery WithMefilter(int id,IEnumerable<filterRule> filters)
    {
      this.And(x => x.SupplierId == id);
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
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
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
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
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.UserName.Contains(rule.value));
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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
    public BiddingQuery ByPurchaseOrderIdWithfilter(int purchaseorderid, IEnumerable<filterRule> filters)
    {
      And(x => x.PurchaseOrderId == purchaseorderid);
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
                break;
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
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.Buyer == rule.value);
            }
            else
            {
              And(x => x.Buyer.Contains(rule.value));
            }
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.BuyerPhone == rule.value);
            }
            else
            {
              And(x => x.BuyerPhone.Contains(rule.value));
            }
          }
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.UserName == rule.value);
            }
            else
            {
              And(x => x.UserName.Contains(rule.value));
            }
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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
          if (rule.field == "DocNo" && !string.IsNullOrEmpty(rule.value))
          {
            if (rule.op == "equal")
            {
              And(x => x.DocNo == rule.value);
            }
            else
            {
              And(x => x.DocNo.Contains(rule.value));
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


    public BiddingQuery WithMeDestroyfilter(int id, IEnumerable<filterRule> filters)
    {
      if (id > 0)
      {
        this.And(x => x.SupplierId == id);
      }
      var array = new string[] { "废标", "废标待确认", "废标已确认" };
      this.And(x => array.Contains(x.Status));
      if (filters != null)
      {
        foreach (var rule in filters)
        {
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
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
          }
          if (rule.field == "PO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.PO.Contains(rule.value));
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
          if (rule.field == "LineNum" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.LineNum.Contains(rule.value));
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
          if (rule.field == "BiddingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.BiddingPrice == val);
                break;
              case "notequal":
                And(x => x.BiddingPrice != val);
                break;
              case "less":
                And(x => x.BiddingPrice < val);
                break;
              case "lessorequal":
                And(x => x.BiddingPrice <= val);
                break;
              case "greater":
                And(x => x.BiddingPrice > val);
                break;
              case "greaterorequal":
                And(x => x.BiddingPrice >= val);
                break;
              default:
                And(x => x.BiddingPrice == val);
                break;
            }
          }
          if (rule.field == "TotalPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalPrice == val);
                break;
              case "notequal":
                And(x => x.TotalPrice != val);
                break;
              case "less":
                And(x => x.TotalPrice < val);
                break;
              case "lessorequal":
                And(x => x.TotalPrice <= val);
                break;
              case "greater":
                And(x => x.TotalPrice > val);
                break;
              case "greaterorequal":
                And(x => x.TotalPrice >= val);
                break;
              default:
                And(x => x.TotalPrice == val);
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
          if (rule.field == "UserName" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.UserName.Contains(rule.value));
          }
          if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
          {
            var val = Convert.ToInt32(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.SupplierId == val);
                break;
              case "notequal":
                And(x => x.SupplierId != val);
                break;
              case "less":
                And(x => x.SupplierId < val);
                break;
              case "lessorequal":
                And(x => x.SupplierId <= val);
                break;
              case "greater":
                And(x => x.SupplierId > val);
                break;
              case "greaterorequal":
                And(x => x.SupplierId >= val);
                break;
              default:
                And(x => x.SupplierId == val);
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
  }
}
