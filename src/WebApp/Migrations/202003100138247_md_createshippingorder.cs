namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_createshippingorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SO = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 10),
                        ShippingDate = c.DateTime(nullable: false),
                        DeliveryAddress = c.String(maxLength: 200),
                        Buyer = c.String(maxLength: 20),
                        BuyerPhone = c.String(maxLength: 50),
                        Packages = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNo = c.String(maxLength: 50),
                        InvoiceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 200),
                        ReceivedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        UserName = c.String(maxLength: 20),
                        SupplierId = c.Int(nullable: false),
                        SupplierName = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SO, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ShippingOrders", new[] { "SO" });
            DropTable("dbo.ShippingOrders");
        }
    }
}
