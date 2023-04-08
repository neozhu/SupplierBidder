using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlHelper2;

namespace WebApp
{
  public class Dashboard
  {
    private static Dashboard _db;
    public static Dashboard Instance {
      get
      {
        if (_db == null)
        {
          _db= new Dashboard();
        }
        return _db;
        
      }
  }
    private IDatabaseAsync db;
    private Dashboard() => this.db = SqlHelper2.DatabaseFactory.CreateDatabase();

    //发标的
    public int ALLCount()
    {
      try
      {
        var sql = "select count(1) from [dbo].[PurchaseOrders] where Status!=N'待处理1'";
        return db.ExecuteScalar<int>(sql);
      }
      catch {
        return 0;
        }
     
    }

    //待发标
    public int SCount() {
      try
      {
        var sql = "select count(1) from [dbo].[PurchaseOrders] where Status=N'待处理'";
      return db.ExecuteScalar<int>(sql);
    }
      catch {
        return 0;
        }
    }
    //可竞标
    public int BCount(int id)
    {
      try
      {
        var sql = $"select count(1) from [dbo].[PurchaseOrders] t1 where t1.Status=N'发标中' and exists(select * from dbo.Tenders t2 where t1.Id=t2.PurchaseOrderId and t2.SupplierId={id})";
      return db.ExecuteScalar<int>(sql);
  }
      catch {
        return 0;
        }
    }
    //确认 不要了
    public int DCount()
    {
      try
      {
        var sql = "select count(1) from [dbo].[PurchaseOrders] where Status=N'确认中'";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }
    //待确认
    public int ACount(int id)
    {
      try
      {
        var sql = $"select count(1) from [dbo].[Biddings] where Status in(N'中标',N'废标待确认') and SupplierId={id}";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }
    //待发货
    public int PCount(int id)
    {
      try
      {
        var sql = $"select count(1) from [dbo].[Biddings] where Status in(N'已确认') and SupplierId={id}";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }
    //待开标
    public int OCount()
    {
      try
      {
        var sql = "select count(1) from [dbo].[PurchaseOrders] where Status=N'开标中'";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }
    //待收货
    public int RCount()
    {
      try
      {
        var sql = "select count(1) from [dbo].[PurchaseOrders] where Status in (N'已发货',N'待收货')";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }
    //待结案
    public int CCount()
    {
      try
      {
        var sql = " select count(1) from [dbo].[PurchaseOrders] where Status=N'结案中'";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }

    //待结案
    public int SCount(int id)
    {
      try
      {
        var sql = $" select count(1) from [dbo].[ShippingOrders] where Status=N'结案中' and SupplierId={id}";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }

    //是否发送短信
    public int SMS() {
      try
      {
        var sql = "select CAST(code AS int)  from [dbo].[CodeItems] where codetype='sms'";
      return db.ExecuteScalar<int>(sql);
}
      catch {
        return 0;
        }
    }

  }
}