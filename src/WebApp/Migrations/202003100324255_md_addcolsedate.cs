namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addcolsedate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biddings", "SO", c => c.String(maxLength: 20));
            AddColumn("dbo.Biddings", "ShippingDate", c => c.DateTime());
            AddColumn("dbo.Biddings", "ReceivedDate", c => c.DateTime());
            AddColumn("dbo.Biddings", "ClosedDate", c => c.DateTime());
            AddColumn("dbo.PurchaseOrders", "ClosedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "ClosedDate");
            DropColumn("dbo.Biddings", "ClosedDate");
            DropColumn("dbo.Biddings", "ReceivedDate");
            DropColumn("dbo.Biddings", "ShippingDate");
            DropColumn("dbo.Biddings", "SO");
        }
    }
}
