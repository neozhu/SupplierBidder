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
  /// File: ShippingOrderQuery.cs
  /// Purpose: easyui datagrid filter query 
  /// Created Date: 3/10/2020 9:40:39 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class ShippingOrderQuery : QueryObject<ShippingOrder>
  {
    public ShippingOrderQuery WithReceivingfilter(IEnumerable<filterRule> filters)
    {
      this.And(x => x.Status == "发货中");
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
          if (rule.field == "SO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SO.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
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
          if (rule.field == "DeliveryAddress" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.DeliveryAddress.Contains(rule.value));
          }
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Buyer.Contains(rule.value));
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BuyerPhone.Contains(rule.value));
          }
          if (rule.field == "Packages" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Packages == val);
                break;
              case "notequal":
                And(x => x.Packages != val);
                break;
              case "less":
                And(x => x.Packages < val);
                break;
              case "lessorequal":
                And(x => x.Packages <= val);
                break;
              case "greater":
                And(x => x.Packages > val);
                break;
              case "greaterorequal":
                And(x => x.Packages >= val);
                break;
              default:
                And(x => x.Packages == val);
                break;
            }
          }
          if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalAmount == val);
                break;
              case "notequal":
                And(x => x.TotalAmount != val);
                break;
              case "less":
                And(x => x.TotalAmount < val);
                break;
              case "lessorequal":
                And(x => x.TotalAmount <= val);
                break;
              case "greater":
                And(x => x.TotalAmount > val);
                break;
              case "greaterorequal":
                And(x => x.TotalAmount >= val);
                break;
              default:
                And(x => x.TotalAmount == val);
                break;
            }
          }
          if (rule.field == "InvoiceNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvoiceNo.Contains(rule.value));
          }
          if (rule.field == "InvoiceAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.InvoiceAmount == val);
                break;
              case "notequal":
                And(x => x.InvoiceAmount != val);
                break;
              case "less":
                And(x => x.InvoiceAmount < val);
                break;
              case "lessorequal":
                And(x => x.InvoiceAmount <= val);
                break;
              case "greater":
                And(x => x.InvoiceAmount > val);
                break;
              case "greaterorequal":
                And(x => x.InvoiceAmount >= val);
                break;
              default:
                And(x => x.InvoiceAmount == val);
                break;
            }
          }
          if (rule.field == "TaxRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TaxRate == val);
                break;
              case "notequal":
                And(x => x.TaxRate != val);
                break;
              case "less":
                And(x => x.TaxRate < val);
                break;
              case "lessorequal":
                And(x => x.TaxRate <= val);
                break;
              case "greater":
                And(x => x.TaxRate > val);
                break;
              case "greaterorequal":
                And(x => x.TaxRate >= val);
                break;
              default:
                And(x => x.TaxRate == val);
                break;
            }
          }
          if (rule.field == "Tax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Tax == val);
                break;
              case "notequal":
                And(x => x.Tax != val);
                break;
              case "less":
                And(x => x.Tax < val);
                break;
              case "lessorequal":
                And(x => x.Tax <= val);
                break;
              case "greater":
                And(x => x.Tax > val);
                break;
              case "greaterorequal":
                And(x => x.Tax >= val);
                break;
              default:
                And(x => x.Tax == val);
                break;
            }
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Remark.Contains(rule.value));
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
    public ShippingOrderQuery WithReceivedfilter(IEnumerable<filterRule> filters)
    {
      var array = new string[] { "结案中" };
      this.And(x => array.Contains(x.Status) );
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
          if (rule.field == "SO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SO.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
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
          if (rule.field == "DeliveryAddress" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.DeliveryAddress.Contains(rule.value));
          }
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Buyer.Contains(rule.value));
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BuyerPhone.Contains(rule.value));
          }
          if (rule.field == "Packages" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Packages == val);
                break;
              case "notequal":
                And(x => x.Packages != val);
                break;
              case "less":
                And(x => x.Packages < val);
                break;
              case "lessorequal":
                And(x => x.Packages <= val);
                break;
              case "greater":
                And(x => x.Packages > val);
                break;
              case "greaterorequal":
                And(x => x.Packages >= val);
                break;
              default:
                And(x => x.Packages == val);
                break;
            }
          }
          if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalAmount == val);
                break;
              case "notequal":
                And(x => x.TotalAmount != val);
                break;
              case "less":
                And(x => x.TotalAmount < val);
                break;
              case "lessorequal":
                And(x => x.TotalAmount <= val);
                break;
              case "greater":
                And(x => x.TotalAmount > val);
                break;
              case "greaterorequal":
                And(x => x.TotalAmount >= val);
                break;
              default:
                And(x => x.TotalAmount == val);
                break;
            }
          }
          if (rule.field == "InvoiceNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvoiceNo.Contains(rule.value));
          }
          if (rule.field == "InvoiceAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.InvoiceAmount == val);
                break;
              case "notequal":
                And(x => x.InvoiceAmount != val);
                break;
              case "less":
                And(x => x.InvoiceAmount < val);
                break;
              case "lessorequal":
                And(x => x.InvoiceAmount <= val);
                break;
              case "greater":
                And(x => x.InvoiceAmount > val);
                break;
              case "greaterorequal":
                And(x => x.InvoiceAmount >= val);
                break;
              default:
                And(x => x.InvoiceAmount == val);
                break;
            }
          }
          if (rule.field == "TaxRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TaxRate == val);
                break;
              case "notequal":
                And(x => x.TaxRate != val);
                break;
              case "less":
                And(x => x.TaxRate < val);
                break;
              case "lessorequal":
                And(x => x.TaxRate <= val);
                break;
              case "greater":
                And(x => x.TaxRate > val);
                break;
              case "greaterorequal":
                And(x => x.TaxRate >= val);
                break;
              default:
                And(x => x.TaxRate == val);
                break;
            }
          }
          if (rule.field == "Tax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Tax == val);
                break;
              case "notequal":
                And(x => x.Tax != val);
                break;
              case "less":
                And(x => x.Tax < val);
                break;
              case "lessorequal":
                And(x => x.Tax <= val);
                break;
              case "greater":
                And(x => x.Tax > val);
                break;
              case "greaterorequal":
                And(x => x.Tax >= val);
                break;
              default:
                And(x => x.Tax == val);
                break;
            }
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Remark.Contains(rule.value));
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
    public ShippingOrderQuery WithSupplierIdfilter(int supplierId,IEnumerable<filterRule> filters)
    {
      this.And(x => x.SupplierId == supplierId);
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
          if (rule.field == "SO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SO.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
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
          if (rule.field == "DeliveryAddress" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.DeliveryAddress.Contains(rule.value));
          }
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Buyer.Contains(rule.value));
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BuyerPhone.Contains(rule.value));
          }
          if (rule.field == "Packages" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Packages == val);
                break;
              case "notequal":
                And(x => x.Packages != val);
                break;
              case "less":
                And(x => x.Packages < val);
                break;
              case "lessorequal":
                And(x => x.Packages <= val);
                break;
              case "greater":
                And(x => x.Packages > val);
                break;
              case "greaterorequal":
                And(x => x.Packages >= val);
                break;
              default:
                And(x => x.Packages == val);
                break;
            }
          }
          if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalAmount == val);
                break;
              case "notequal":
                And(x => x.TotalAmount != val);
                break;
              case "less":
                And(x => x.TotalAmount < val);
                break;
              case "lessorequal":
                And(x => x.TotalAmount <= val);
                break;
              case "greater":
                And(x => x.TotalAmount > val);
                break;
              case "greaterorequal":
                And(x => x.TotalAmount >= val);
                break;
              default:
                And(x => x.TotalAmount == val);
                break;
            }
          }
          if (rule.field == "InvoiceNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvoiceNo.Contains(rule.value));
          }
          if (rule.field == "InvoiceAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.InvoiceAmount == val);
                break;
              case "notequal":
                And(x => x.InvoiceAmount != val);
                break;
              case "less":
                And(x => x.InvoiceAmount < val);
                break;
              case "lessorequal":
                And(x => x.InvoiceAmount <= val);
                break;
              case "greater":
                And(x => x.InvoiceAmount > val);
                break;
              case "greaterorequal":
                And(x => x.InvoiceAmount >= val);
                break;
              default:
                And(x => x.InvoiceAmount == val);
                break;
            }
          }
          if (rule.field == "TaxRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TaxRate == val);
                break;
              case "notequal":
                And(x => x.TaxRate != val);
                break;
              case "less":
                And(x => x.TaxRate < val);
                break;
              case "lessorequal":
                And(x => x.TaxRate <= val);
                break;
              case "greater":
                And(x => x.TaxRate > val);
                break;
              case "greaterorequal":
                And(x => x.TaxRate >= val);
                break;
              default:
                And(x => x.TaxRate == val);
                break;
            }
          }
          if (rule.field == "Tax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Tax == val);
                break;
              case "notequal":
                And(x => x.Tax != val);
                break;
              case "less":
                And(x => x.Tax < val);
                break;
              case "lessorequal":
                And(x => x.Tax <= val);
                break;
              case "greater":
                And(x => x.Tax > val);
                break;
              case "greaterorequal":
                And(x => x.Tax >= val);
                break;
              default:
                And(x => x.Tax == val);
                break;
            }
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Remark.Contains(rule.value));
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
    public ShippingOrderQuery Withfilter(IEnumerable<filterRule> filters)
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
          if (rule.field == "SO" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.SO.Contains(rule.value));
          }
          if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Status.Contains(rule.value));
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
          if (rule.field == "DeliveryAddress" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.DeliveryAddress.Contains(rule.value));
          }
          if (rule.field == "Buyer" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Buyer.Contains(rule.value));
          }
          if (rule.field == "BuyerPhone" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.BuyerPhone.Contains(rule.value));
          }
          if (rule.field == "Packages" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Packages == val);
                break;
              case "notequal":
                And(x => x.Packages != val);
                break;
              case "less":
                And(x => x.Packages < val);
                break;
              case "lessorequal":
                And(x => x.Packages <= val);
                break;
              case "greater":
                And(x => x.Packages > val);
                break;
              case "greaterorequal":
                And(x => x.Packages >= val);
                break;
              default:
                And(x => x.Packages == val);
                break;
            }
          }
          if (rule.field == "TotalAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TotalAmount == val);
                break;
              case "notequal":
                And(x => x.TotalAmount != val);
                break;
              case "less":
                And(x => x.TotalAmount < val);
                break;
              case "lessorequal":
                And(x => x.TotalAmount <= val);
                break;
              case "greater":
                And(x => x.TotalAmount > val);
                break;
              case "greaterorequal":
                And(x => x.TotalAmount >= val);
                break;
              default:
                And(x => x.TotalAmount == val);
                break;
            }
          }
          if (rule.field == "InvoiceNo" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.InvoiceNo.Contains(rule.value));
          }
          if (rule.field == "InvoiceAmount" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.InvoiceAmount == val);
                break;
              case "notequal":
                And(x => x.InvoiceAmount != val);
                break;
              case "less":
                And(x => x.InvoiceAmount < val);
                break;
              case "lessorequal":
                And(x => x.InvoiceAmount <= val);
                break;
              case "greater":
                And(x => x.InvoiceAmount > val);
                break;
              case "greaterorequal":
                And(x => x.InvoiceAmount >= val);
                break;
              default:
                And(x => x.InvoiceAmount == val);
                break;
            }
          }
          if (rule.field == "TaxRate" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.TaxRate == val);
                break;
              case "notequal":
                And(x => x.TaxRate != val);
                break;
              case "less":
                And(x => x.TaxRate < val);
                break;
              case "lessorequal":
                And(x => x.TaxRate <= val);
                break;
              case "greater":
                And(x => x.TaxRate > val);
                break;
              case "greaterorequal":
                And(x => x.TaxRate >= val);
                break;
              default:
                And(x => x.TaxRate == val);
                break;
            }
          }
          if (rule.field == "Tax" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
          {
            var val = Convert.ToDecimal(rule.value);
            switch (rule.op)
            {
              case "equal":
                And(x => x.Tax == val);
                break;
              case "notequal":
                And(x => x.Tax != val);
                break;
              case "less":
                And(x => x.Tax < val);
                break;
              case "lessorequal":
                And(x => x.Tax <= val);
                break;
              case "greater":
                And(x => x.Tax > val);
                break;
              case "greaterorequal":
                And(x => x.Tax >= val);
                break;
              default:
                And(x => x.Tax == val);
                break;
            }
          }
          if (rule.field == "Remark" && !string.IsNullOrEmpty(rule.value))
          {
            And(x => x.Remark.Contains(rule.value));
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
