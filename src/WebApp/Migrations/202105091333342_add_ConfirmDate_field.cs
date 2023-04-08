namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ConfirmDate_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocates", "ConfirmDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocates", "ConfirmDate");
        }
    }
}
