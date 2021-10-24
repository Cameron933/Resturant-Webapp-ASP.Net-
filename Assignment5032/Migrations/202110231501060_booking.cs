namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "UserId", "dbo.RestUsers");
            DropIndex("dbo.Books", new[] { "UserId" });
            DropPrimaryKey("dbo.Books");
            AddColumn("dbo.Books", "User_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Books", new[] { "UserId", "EventId" });
            CreateIndex("dbo.Books", "User_Id");
            AddForeignKey("dbo.Books", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BookId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Books", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "User_Id" });
            DropPrimaryKey("dbo.Books");
            DropColumn("dbo.Books", "User_Id");
            AddPrimaryKey("dbo.Books", "BookId");
            CreateIndex("dbo.Books", "UserId");
            AddForeignKey("dbo.Books", "UserId", "dbo.RestUsers", "UserId", cascadeDelete: true);
        }
    }
}
