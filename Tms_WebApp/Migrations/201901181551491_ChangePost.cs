namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AdminArea", c => c.String());
            AddColumn("dbo.Posts", "CountryCode", c => c.String());
            AddColumn("dbo.Posts", "CountryName", c => c.String());
            AddColumn("dbo.Posts", "FeatureName", c => c.String());
            AddColumn("dbo.Posts", "Locality", c => c.String());
            AddColumn("dbo.Posts", "PostalCode", c => c.String());
            AddColumn("dbo.Posts", "SubAdminArea", c => c.String());
            AddColumn("dbo.Posts", "SubLocality", c => c.String());
            AddColumn("dbo.Posts", "SubThroughfare", c => c.String());
            AddColumn("dbo.Posts", "Throughfare", c => c.String());
            DropColumn("dbo.Posts", "VenueName");
            DropColumn("dbo.Posts", "CategoryId");
            DropColumn("dbo.Posts", "CategoryName");
            DropColumn("dbo.Posts", "Address");
            DropColumn("dbo.Posts", "Distance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Distance", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Address", c => c.String());
            AddColumn("dbo.Posts", "CategoryName", c => c.String());
            AddColumn("dbo.Posts", "CategoryId", c => c.String());
            AddColumn("dbo.Posts", "VenueName", c => c.String());
            DropColumn("dbo.Posts", "Throughfare");
            DropColumn("dbo.Posts", "SubThroughfare");
            DropColumn("dbo.Posts", "SubLocality");
            DropColumn("dbo.Posts", "SubAdminArea");
            DropColumn("dbo.Posts", "PostalCode");
            DropColumn("dbo.Posts", "Locality");
            DropColumn("dbo.Posts", "FeatureName");
            DropColumn("dbo.Posts", "CountryName");
            DropColumn("dbo.Posts", "CountryCode");
            DropColumn("dbo.Posts", "AdminArea");
        }
    }
}
