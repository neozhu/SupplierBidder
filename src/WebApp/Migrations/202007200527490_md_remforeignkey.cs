namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_remforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Companies");
            DropIndex("dbo.CategoryAllocations", new[] { "CategoryId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.CategoryAllocations", "CategoryId");
            AddForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
