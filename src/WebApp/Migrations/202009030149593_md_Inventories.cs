namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_Inventories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PO = c.String(nullable: false, maxLength: 20),
                        LineNum = c.String(nullable: false, maxLength: 3),
                        PODate = c.DateTime(),
                        ReceivedDate = c.DateTime(),
                        Receiver = c.String(maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 10),
                        InvStatus = c.String(nullable: false, maxLength: 10),
                        OutboundDate = c.DateTime(),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Spec = c.String(maxLength: 100),
                        BrandName = c.String(maxLength: 100),
                        Unit = c.String(maxLength: 10),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ControlledPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Feature = c.String(maxLength: 500),
                        Description = c.String(maxLength: 512),
                        Buyer = c.String(maxLength: 20),
                        BuyerPhone = c.String(maxLength: 50),
                        BiddingDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        DemandedDate = c.DateTime(),
                        ProductNo = c.String(maxLength: 50),
                        ShippingDate = c.DateTime(),
                        SO = c.String(maxLength: 20),
                        InvoiceNo = c.String(maxLength: 50),
                        OpenDate = c.DateTime(),
                        BiddingTime = c.Int(nullable: false),
                        BiddingUsers = c.Int(nullable: false),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierName = c.String(maxLength: 50),
                        DocNo = c.String(maxLength: 20),
                        ClosedDate = c.DateTime(),
                        Category = c.String(maxLength: 128),
                        Dept = c.String(maxLength: 30),
                        Section = c.String(maxLength: 30),
                        GrantNo = c.String(maxLength: 30),
                        Reason = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Oubounds",
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
                        StockQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierName = c.String(maxLength: 50),
                        Feature = c.String(maxLength: 500),
                        Description = c.String(maxLength: 512),
                        Remark = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.PurchaseOrders", "Description", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "Description", c => c.String());
            DropTable("dbo.Oubounds");
            DropTable("dbo.Inventories");
        }
    }
}
