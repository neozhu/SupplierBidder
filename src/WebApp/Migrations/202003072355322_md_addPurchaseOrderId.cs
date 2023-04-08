namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addPurchaseOrderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biddings", "PurchaseOrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Biddings", "PurchaseOrderId");
            AddForeignKey("dbo.Biddings", "PurchaseOrderId", "dbo.PurchaseOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Biddings", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.Biddings", new[] { "PurchaseOrderId" });
            DropColumn("dbo.Biddings", "PurchaseOrderId");
        }
    }
}
