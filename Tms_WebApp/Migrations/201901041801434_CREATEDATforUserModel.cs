namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CREATEDATforUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CREATEDAT", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CREATEDAT");
        }
    }
}
