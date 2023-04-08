namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_adddept : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Biddings", "Dept", c => c.String(maxLength: 30));
            AddColumn("dbo.Biddings", "Section", c => c.String(maxLength: 30));
            AddColumn("dbo.Biddings", "GrantNo", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Biddings", "GrantNo");
            DropColumn("dbo.Biddings", "Section");
            DropColumn("dbo.Biddings", "Dept");
        }
    }
}
