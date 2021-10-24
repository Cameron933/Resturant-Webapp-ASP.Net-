namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringbook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "EventId", "dbo.Events");
            DropIndex("dbo.Books", new[] { "EventId" });
            DropIndex("dbo.Books", new[] { "User_Id" });
            DropColumn("dbo.Books", "UserId");
            RenameColumn(table: "dbo.Books", name: "User_Id", newName: "UserId");
            DropPrimaryKey("dbo.Books");
            AddColumn("dbo.Books", "Event_EventId", c => c.Int());
            AlterColumn("dbo.Books", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Books", "EventId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Books", "EventId");
            CreateIndex("dbo.Books", "UserId");
            CreateIndex("dbo.Books", "Event_EventId");
            AddForeignKey("dbo.Books", "Event_EventId", "dbo.Events", "EventId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Event_EventId", "dbo.Events");
            DropIndex("dbo.Books", new[] { "Event_EventId" });
            DropIndex("dbo.Books", new[] { "UserId" });
            DropPrimaryKey("dbo.Books");
            AlterColumn("dbo.Books", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "Event_EventId");
            AddPrimaryKey("dbo.Books", new[] { "UserId", "EventId" });
            RenameColumn(table: "dbo.Books", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Books", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "User_Id");
            CreateIndex("dbo.Books", "EventId");
            AddForeignKey("dbo.Books", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
    }
}
