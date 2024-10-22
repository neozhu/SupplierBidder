namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_Outbounds : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Oubounds", newName: "Outbounds");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Outbounds", newName: "Oubounds");
        }
    }
}
