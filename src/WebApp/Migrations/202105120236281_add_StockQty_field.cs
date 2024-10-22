namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_StockQty_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocates", "StockQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocates", "StockQty");
        }
    }
}
