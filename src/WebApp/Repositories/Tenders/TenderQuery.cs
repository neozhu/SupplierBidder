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
/// File: TenderQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 3/7/2020 9:41:39 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class TenderQuery:QueryObject<Tender>
   {
		public TenderQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "DocNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DocNo.Contains(rule.value));
						}
						if (rule.field == "PurchaseOrderId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.PurchaseOrderId >= val);
                                break;
                            default:
                                And(x => x.PurchaseOrderId == val);
                                break;
                        }
						}
						if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.SupplierId >= val);
                                break;
                            default:
                                And(x => x.SupplierId == val);
                                break;
                        }
						}
						if (rule.field == "CreatedDate" && !string.IsNullOrEmpty(rule.value) )
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
						if (rule.field == "CreatedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreatedBy.Contains(rule.value));
						}
						if (rule.field == "LastModifiedDate" && !string.IsNullOrEmpty(rule.value) )
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
						if (rule.field == "LastModifiedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LastModifiedBy.Contains(rule.value));
						}
						if (rule.field == "TenantId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
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
         public  TenderQuery ByPurchaseOrderIdWithfilter(int purchaseorderid, IEnumerable<filterRule> filters)
         {
            And(x => x.PurchaseOrderId == purchaseorderid);
            if (filters != null)
           {
               foreach (var rule in filters)
               {     
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "DocNo"  && !string.IsNullOrEmpty(rule.value))
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
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.PurchaseOrderId >= val);
                                break;
                            default:
                                And(x => x.PurchaseOrderId == val);
                                break;
                        }
						}
						if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.SupplierId >= val);
                                break;
                            default:
                                And(x => x.SupplierId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
         public  TenderQuery BySupplierIdWithfilter(int supplierid, IEnumerable<filterRule> filters)
         {
            And(x => x.SupplierId == supplierid);
            if (filters != null)
           {
               foreach (var rule in filters)
               {     
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "DocNo"  && !string.IsNullOrEmpty(rule.value))
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
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.PurchaseOrderId >= val);
                                break;
                            default:
                                And(x => x.PurchaseOrderId == val);
                                break;
                        }
						}
						if (rule.field == "SupplierId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
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
                            case "greaterorequal" :
                                And(x => x.SupplierId >= val);
                                break;
                            default:
                                And(x => x.SupplierId == val);
                                break;
                        }
						}
               }
            }
            return this;
         }    
    }
}
