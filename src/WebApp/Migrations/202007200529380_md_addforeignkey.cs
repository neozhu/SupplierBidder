namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addforeignkey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CategoryAllocations", "CategoryId");
            CreateIndex("dbo.CategoryAllocations", "CompanyId");
            AddForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryAllocations", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryAllocations", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryAllocations", new[] { "CompanyId" });
            DropIndex("dbo.CategoryAllocations", new[] { "CategoryId" });
        }
    }
}
