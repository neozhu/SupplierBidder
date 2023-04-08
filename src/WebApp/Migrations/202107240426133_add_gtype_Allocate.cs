namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_gtype_Allocate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allocates", "GType", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allocates", "GType");
        }
    }
}
