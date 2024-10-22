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
/// File: AllocateRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 5/9/2021 7:43:17 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class AllocateRepository  
    {
                 public static async Task<IEnumerable<Allocate>> GetByPurchaseOrderIdAsync(this IRepositoryAsync<Allocate> repository, int purchaseorderid)
          => await repository
                .Queryable()
                .Where(x => x.PurchaseOrderId==purchaseorderid).ToListAsync();
              
          
                 
	}
}



