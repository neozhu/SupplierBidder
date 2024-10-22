namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Biddings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BiddingDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        PO = c.String(nullable: false, maxLength: 20),
                        DemandedDate = c.DateTime(),
                        LineNum = c.String(nullable: false, maxLength: 3),
                        ProductNo = c.String(maxLength: 50),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Spec = c.String(maxLength: 100),
                        BrandName = c.String(maxLength: 100),
                        Unit = c.String(maxLength: 10),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BiddingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Feature = c.String(maxLength: 100),
                        Description = c.String(),
                        Buyer = c.String(maxLength: 20),
                        BuyerPhone = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 20),
                        SupplierId = c.Int(nullable: false),
                        SupplierName = c.String(maxLength: 50),
                        DocNo = c.String(maxLength: 20),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PO = c.String(nullable: false, maxLength: 20),
                        PODate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        DemandedDate = c.DateTime(),
                        LineNum = c.String(nullable: false, maxLength: 3),
                        ProductNo = c.String(maxLength: 50),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Spec = c.String(maxLength: 100),
                        BrandName = c.String(maxLength: 100),
                        Unit = c.String(maxLength: 10),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ControlledPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Feature = c.String(maxLength: 100),
                        Description = c.String(),
                        Buyer = c.String(maxLength: 20),
                        BuyerPhone = c.String(maxLength: 50),
                        BiddingDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        ShippingDate = c.DateTime(),
                        SO = c.String(maxLength: 20),
                        InvoiceNo = c.String(maxLength: 50),
                        OpenDate = c.DateTime(),
                        ReceivedDate = c.DateTime(),
                        BiddingTime = c.Int(nullable: false),
                        BiddingUsers = c.Int(nullable: false),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierName = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tenders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocNo = c.String(maxLength: 20),
                        PurchaseOrderId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenders", "SupplierId", "dbo.Companies");
            DropForeignKey("dbo.Tenders", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.Tenders", new[] { "SupplierId" });
            DropIndex("dbo.Tenders", new[] { "PurchaseOrderId" });
            DropTable("dbo.Tenders");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.Biddings");
        }
    }
}
