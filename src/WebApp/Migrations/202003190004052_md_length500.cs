namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_length500 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseOrders", "Feature", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "Feature", c => c.String(maxLength: 100));
        }
    }
}
