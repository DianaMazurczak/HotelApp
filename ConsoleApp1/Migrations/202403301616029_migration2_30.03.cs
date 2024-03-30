namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2_3003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomStandards", "Standard", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomStandards", "Standard");
        }
    }
}
