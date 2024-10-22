namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Loc_Allocate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocates", "ProductInfo", c => c.String(maxLength: 512));
            AddColumn("dbo.Allocates", "Loc", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocates", "Loc");
            DropColumn("dbo.Allocates", "ProductInfo");
        }
    }
}
