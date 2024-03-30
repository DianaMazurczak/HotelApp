namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_3003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "RsId");
        }
    }
}
