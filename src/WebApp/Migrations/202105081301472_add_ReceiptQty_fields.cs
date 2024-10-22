namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ReceiptQty_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "ReceiptQty", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "OpenQty", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "InvoiceAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "InvoiceAmount");
            DropColumn("dbo.PurchaseOrders", "OpenQty");
            DropColumn("dbo.PurchaseOrders", "ReceiptQty");
        }
    }
}
