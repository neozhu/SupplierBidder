namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_DeliveryCycle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biddings", "DeliveryCycle", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Biddings", "DeliveryCycle");
        }
    }
}
