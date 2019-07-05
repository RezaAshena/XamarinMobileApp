namespace Tms_WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictureModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        Size = c.String(),
                        URL = c.Double(nullable: false),
                        Content = c.Double(nullable: false),
                        CREATEDAT = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "UserId", "dbo.Users");
            DropIndex("dbo.Pictures", new[] { "UserId" });
            DropTable("dbo.Pictures");
        }
    }
}
