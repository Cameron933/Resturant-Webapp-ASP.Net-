namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addbooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.RestUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
            DropColumn("dbo.Events", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Description", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Books", "UserId", "dbo.RestUsers");
            DropForeignKey("dbo.Books", "EventId", "dbo.Events");
            DropIndex("dbo.Books", new[] { "EventId" });
            DropIndex("dbo.Books", new[] { "UserId" });
            DropTable("dbo.Books");
        }
    }
}
