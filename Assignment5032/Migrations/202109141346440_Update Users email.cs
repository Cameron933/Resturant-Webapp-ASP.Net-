namespace Assignment5032.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUsersemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestUsers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestUsers", "Email");
        }
    }
}
