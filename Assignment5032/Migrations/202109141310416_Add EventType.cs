namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventTypes");
        }
    }
}
