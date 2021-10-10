namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "RestID", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "TypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "RestID");
            CreateIndex("dbo.Events", "TypeID");
            AddForeignKey("dbo.Events", "TypeID", "dbo.EventTypes", "TypeID", cascadeDelete: true);
            AddForeignKey("dbo.Events", "RestID", "dbo.Restaurants", "RestID", cascadeDelete: true);
            DropColumn("dbo.Events", "EventName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventName", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.Events", "RestID", "dbo.Restaurants");
            DropForeignKey("dbo.Events", "TypeID", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "TypeID" });
            DropIndex("dbo.Events", new[] { "RestID" });
            DropColumn("dbo.Events", "TypeID");
            DropColumn("dbo.Events", "RestID");
        }
    }
}
