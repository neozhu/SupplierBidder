using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.IO;
using WebApp.Models.ViewModel;

namespace WebApp.Services
{
/// <summary>
/// File: IPurchaseOrderService.cs
/// Purpose: Service interfaces. Services expose a service interface
/// to which all inbound messages are sent. You can think of a service interface
/// as a façade that exposes the business logic implemented in the application
/// Created Date: 3/7/2020 7:44:49 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public interface IPurchaseOrderService:IService<PurchaseOrder>
    {
         Task<IEnumerable<Tender>>   GetTendersByPurchaseOrderIdAsync (int purchaseorderid);
 
		Task ImportDataTableAsync(DataTable datatable,string username="");
    Task ImportDataTableAsync1(DataTable datatable, string username = "");
    Task<Stream> ExportExcelAsync( string filterRules = "",string sort = "Id", string order = "asc");
	    void Delete(int[] id);
    Task DoBidding(BiddingViewModel viewmodel,string user);
    Task<BidViewModel> GetBidViewModel(int id, string username, int companyid);
    Task Bid(BidRequestViewModel request);

    Task BidedSumarry(int id);
    Task OutBid(int purchaseorderid, int bidid,string reason, string user);
    Task ConfirmBid(int[] bidid);
    Task CreateShippingOrder(SORequestViewModel viewmodel);
    Task Shipped(int[] soid);
    Task Received(int[] soid,DateTime date, string user);
    Task Closed(int[] soid);

    Task DeleteShippingOrders(int[] id);

    Task ChagneDueDate(int id, DateTime duedate);

    Task Destroy(int[] id,string reason);
    Task ReSend(int[] id);
    Task ConfirmRejectBid(int[] bidid, string reason);
    Task DeleteReceiptData(int[] id);
    Task Receipt(ReceiptDto[] request,string username);
    Task ConfirmClose(CloseRequestDto request);
  }
}