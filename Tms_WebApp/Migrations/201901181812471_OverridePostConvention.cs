namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverridePostConvention : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Experience", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Posts", "AdminArea", c => c.String(maxLength: 150));
            AlterColumn("dbo.Posts", "CountryCode", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "CountryName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "FeatureName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "Locality", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "PostalCode", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "SubAdminArea", c => c.String(maxLength: 250));
            AlterColumn("dbo.Posts", "SubLocality", c => c.String(maxLength: 250));
            AlterColumn("dbo.Posts", "SubThroughfare", c => c.String(maxLength: 100));
            AlterColumn("dbo.Posts", "Throughfare", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Throughfare", c => c.String());
            AlterColumn("dbo.Posts", "SubThroughfare", c => c.String());
            AlterColumn("dbo.Posts", "SubLocality", c => c.String());
            AlterColumn("dbo.Posts", "SubAdminArea", c => c.String());
            AlterColumn("dbo.Posts", "PostalCode", c => c.String());
            AlterColumn("dbo.Posts", "Locality", c => c.String());
            AlterColumn("dbo.Posts", "FeatureName", c => c.String());
            AlterColumn("dbo.Posts", "CountryName", c => c.String());
            AlterColumn("dbo.Posts", "CountryCode", c => c.String());
            AlterColumn("dbo.Posts", "AdminArea", c => c.String());
            AlterColumn("dbo.Posts", "Experience", c => c.String());
        }
    }
}
