namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypefloattostringURLandContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pictures", "URL", c => c.String());
            AlterColumn("dbo.Pictures", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pictures", "Content", c => c.Double(nullable: false));
            AlterColumn("dbo.Pictures", "URL", c => c.Double(nullable: false));
        }
    }
}
