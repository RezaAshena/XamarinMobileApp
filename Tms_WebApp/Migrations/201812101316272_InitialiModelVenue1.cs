namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialiModelVenue1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        location_address = c.String(),
                        location_crossStreet = c.String(),
                        location_lat = c.Double(nullable: false),
                        location_lng = c.Double(nullable: false),
                        location_distance = c.Int(nullable: false),
                        location_postalCode = c.String(),
                        location_cc = c.String(),
                        location_city = c.String(),
                        location_state = c.String(),
                        location_country = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        pluralName = c.String(),
                        shortName = c.String(),
                        primary = c.Boolean(nullable: false),
                        Venue_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Venues", t => t.Venue_id)
                .Index(t => t.Venue_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Venue_id", "dbo.Venues");
            DropIndex("dbo.Categories", new[] { "Venue_id" });
            DropTable("dbo.Categories");
            DropTable("dbo.Venues");
        }
    }
}
