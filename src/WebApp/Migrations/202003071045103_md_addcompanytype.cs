namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addcompanytype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Type", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Type");
        }
    }
}
