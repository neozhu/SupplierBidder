namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Allocate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allocates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PO = c.String(nullable: false, maxLength: 20),
                        LineNum = c.String(nullable: false, maxLength: 3),
                        PODate = c.DateTime(),
                        ReceivedDate = c.DateTime(),
                        OuboundDate = c.DateTime(nullable: false),
                        RecordUser = c.String(maxLength: 20),
                        ProductNo = c.String(maxLength: 50),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Spec = c.String(maxLength: 100),
                        BrandName = c.String(maxLength: 100),
                        Unit = c.String(maxLength: 10),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierName = c.String(maxLength: 50),
                        RefId = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 512),
                        Status = c.String(maxLength: 20),
                        Feature = c.String(maxLength: 500),
                        Description = c.String(maxLength: 512),
                        PurchaseOrderId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId)
                .Index(t => t.PurchaseOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocates", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.Allocates", new[] { "PurchaseOrderId" });
            DropTable("dbo.Allocates");
        }
    }
}
