namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OriginalPricenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "OriginalPrice", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "OriginalPrice", c => c.Single(nullable: false));
        }
    }
}
