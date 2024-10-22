namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_mcode : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Companies", new[] { "Code" });
            AlterColumn("dbo.Companies", "Code", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Code", c => c.String(nullable: false, maxLength: 12));
            CreateIndex("dbo.Companies", "Code", unique: true);
        }
    }
}
