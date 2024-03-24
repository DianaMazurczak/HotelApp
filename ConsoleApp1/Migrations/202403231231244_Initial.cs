namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Name");
        }
    }
}
