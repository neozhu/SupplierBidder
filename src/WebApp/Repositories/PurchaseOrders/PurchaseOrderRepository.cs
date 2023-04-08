using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: PurchaseOrderRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 3/7/2020 7:44:44 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class PurchaseOrderRepository  
    {
                        public static async Task<IEnumerable<Tender>>   GetTendersByPurchaseOrderIdAsync (this IRepositoryAsync<PurchaseOrder> repository,int purchaseorderid)
          => await  repository.GetRepositoryAsync<Tender>()
                    .Queryable()
                    .Include(x => x.PurchaseOrder).Include(x => x.Supplier)
                    .Where(n => n.PurchaseOrderId == purchaseorderid)
                    .ToListAsync();
        
         
	}
}



