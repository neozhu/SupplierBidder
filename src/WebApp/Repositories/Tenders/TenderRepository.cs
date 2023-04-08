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
/// File: TenderRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 3/7/2020 9:41:33 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class TenderRepository  
    {
                 public static async Task<IEnumerable<Tender>> GetByPurchaseOrderIdAsync(this IRepositoryAsync<Tender> repository, int purchaseorderid)
          => await repository
                .Queryable()
                .Where(x => x.PurchaseOrderId==purchaseorderid).ToListAsync();
              
          
                 public static async Task<IEnumerable<Tender>> GetBySupplierIdAsync(this IRepositoryAsync<Tender> repository, int supplierid)
          => await repository
                .Queryable()
                .Where(x => x.SupplierId==supplierid).ToListAsync();
              
          
                 
	}
}



