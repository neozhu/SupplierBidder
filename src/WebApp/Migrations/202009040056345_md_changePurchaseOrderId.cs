namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changePurchaseOrderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "PurchaseOrderId", c => c.Int());
            AddColumn("dbo.Outbounds", "PurchaseOrderId", c => c.Int());
            CreateIndex("dbo.Inventories", "PurchaseOrderId");
            CreateIndex("dbo.Outbounds", "PurchaseOrderId");
            AddForeignKey("dbo.Inventories", "PurchaseOrderId", "dbo.PurchaseOrders", "Id");
            AddForeignKey("dbo.Outbounds", "PurchaseOrderId", "dbo.PurchaseOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Outbounds", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.Inventories", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.Outbounds", new[] { "PurchaseOrderId" });
            DropIndex("dbo.Inventories", new[] { "PurchaseOrderId" });
            DropColumn("dbo.Outbounds", "PurchaseOrderId");
            DropColumn("dbo.Inventories", "PurchaseOrderId");
        }
    }
}
