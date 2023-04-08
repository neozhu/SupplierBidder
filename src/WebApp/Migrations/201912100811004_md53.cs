namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md53 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Works", "Status", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "Status", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
