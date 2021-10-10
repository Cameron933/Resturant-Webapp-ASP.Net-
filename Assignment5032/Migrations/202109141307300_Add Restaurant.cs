namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestaurant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 50),
                        ContactNumb = c.String(nullable: false, maxLength: 9),
                    })
                .PrimaryKey(t => t.RestID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurants");
        }
    }
}
