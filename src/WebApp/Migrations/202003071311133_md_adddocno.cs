namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_adddocno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "DocNo", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "DocNo");
        }
    }
}
