namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addReason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "Reason", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "Reason");
        }
    }
}
