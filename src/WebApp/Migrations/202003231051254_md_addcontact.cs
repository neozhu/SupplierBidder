namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addcontact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(maxLength: 30),
                        WeChat = c.String(maxLength: 50),
                        Other = c.String(maxLength: 150),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PurchaseOrders", "Dept", c => c.String(maxLength: 30));
            AddColumn("dbo.PurchaseOrders", "Section", c => c.String(maxLength: 30));
            AddColumn("dbo.PurchaseOrders", "GrantNo", c => c.String(maxLength: 30));
            AddColumn("dbo.Companies", "Category", c => c.String(maxLength: 20));
            AddColumn("dbo.Companies", "Scope", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Scope");
            DropColumn("dbo.Companies", "Category");
            DropColumn("dbo.PurchaseOrders", "GrantNo");
            DropColumn("dbo.PurchaseOrders", "Section");
            DropColumn("dbo.PurchaseOrders", "Dept");
            DropTable("dbo.Contacts");
        }
    }
}
