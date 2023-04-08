namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Remark = c.String(maxLength: 128),
                        AllowSuppliers = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 128),
                        IsDisabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.PurchaseOrders", "Category", c => c.String(maxLength: 128));
            AddColumn("dbo.Tenders", "Category", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Companies");
            DropForeignKey("dbo.CategoryAllocations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryAllocations", new[] { "CategoryId" });
            DropColumn("dbo.Tenders", "Category");
            DropColumn("dbo.PurchaseOrders", "Category");
            DropTable("dbo.CategoryAllocations");
            DropTable("dbo.Categories");
        }
    }
}
