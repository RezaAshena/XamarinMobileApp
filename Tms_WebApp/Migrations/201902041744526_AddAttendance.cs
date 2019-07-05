namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PostId, t.AttendeeId })
                .ForeignKey("dbo.Users", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.AttendeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.Users");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropIndex("dbo.Attendances", new[] { "PostId" });
            DropTable("dbo.Attendances");
        }
    }
}
