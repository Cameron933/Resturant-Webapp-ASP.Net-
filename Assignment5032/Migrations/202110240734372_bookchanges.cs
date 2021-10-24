namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "UserId", "dbo.RestUsers");
            DropIndex("dbo.Books", new[] { "UserId" });
            DropPrimaryKey("dbo.Books");
            AlterColumn("dbo.Books", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Books", new[] { "UserId", "EventId" });
            CreateIndex("dbo.Books", "UserId");
            AddForeignKey("dbo.Books", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BookId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Books", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "UserId" });
            DropPrimaryKey("dbo.Books");
            AlterColumn("dbo.Books", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Books", "BookId");
            CreateIndex("dbo.Books", "UserId");
            AddForeignKey("dbo.Books", "UserId", "dbo.RestUsers", "UserId", cascadeDelete: true);
        }
    }
}
