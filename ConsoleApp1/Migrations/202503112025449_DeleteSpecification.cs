namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSpecification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Bookings", "R_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Specifications", "G_GuestId", "dbo.Guests");
            DropForeignKey("dbo.Specifications", "R_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Bookings", "Specification_BookingId", "dbo.Specifications");
            DropForeignKey("dbo.Bookings", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Rooms", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "CityHotel_CityId", "dbo.Cities");
            DropIndex("dbo.Bookings", new[] { "R_RoomId" });
            DropIndex("dbo.Bookings", new[] { "Specification_BookingId" });
            DropIndex("dbo.Bookings", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Rooms", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Cities", new[] { "City_CityId" });
            DropIndex("dbo.Specifications", new[] { "G_GuestId" });
            DropIndex("dbo.Specifications", new[] { "R_RoomId" });
            DropIndex("dbo.Hotels", new[] { "CityHotel_CityId" });
            RenameColumn(table: "dbo.Bookings", name: "Hotel_HotelId", newName: "HotelId");
            RenameColumn(table: "dbo.Guests", name: "Hotel_HotelId", newName: "HotelId");
            RenameColumn(table: "dbo.Rooms", name: "Hotel_HotelId", newName: "HotelId");
            RenameColumn(table: "dbo.Hotels", name: "CityHotel_CityId", newName: "CityId");
            RenameIndex(table: "dbo.Guests", name: "IX_Hotel_HotelId", newName: "IX_HotelId");
            AddColumn("dbo.Bookings", "DateOfRegistration", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "DateOfCheckOut", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoomStandards", "Standard", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "HotelId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rooms", "HotelId", c => c.Int(nullable: false));
            AlterColumn("dbo.Hotels", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "HotelId");
            CreateIndex("dbo.Rooms", "HotelId");
            CreateIndex("dbo.Hotels", "CityId");
            AddForeignKey("dbo.Bookings", "HotelId", "dbo.Hotels", "HotelId", cascadeDelete: false);
            AddForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels", "HotelId", cascadeDelete: true);
            AddForeignKey("dbo.Hotels", "CityId", "dbo.Cities", "CityId", cascadeDelete: false);
            DropColumn("dbo.Bookings", "DateOfRegestration");
            DropColumn("dbo.Bookings", "R_RoomId");
            DropColumn("dbo.Bookings", "Specification_BookingId");
            DropColumn("dbo.Cities", "City_CityId");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        DateOfCheckOut = c.DateTime(nullable: false),
                        G_GuestId = c.Int(),
                        R_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId);
            
            AddColumn("dbo.Cities", "City_CityId", c => c.Int());
            AddColumn("dbo.Bookings", "Specification_BookingId", c => c.Int());
            AddColumn("dbo.Bookings", "R_RoomId", c => c.Int());
            AddColumn("dbo.Bookings", "DateOfRegestration", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Hotels", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Bookings", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Hotels", new[] { "CityId" });
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.Bookings", new[] { "HotelId" });
            AlterColumn("dbo.Hotels", "CityId", c => c.Int());
            AlterColumn("dbo.Rooms", "HotelId", c => c.Int());
            AlterColumn("dbo.Bookings", "HotelId", c => c.Int());
            DropColumn("dbo.RoomStandards", "Standard");
            DropColumn("dbo.Bookings", "DateOfCheckOut");
            DropColumn("dbo.Bookings", "DateOfRegistration");
            RenameIndex(table: "dbo.Guests", name: "IX_HotelId", newName: "IX_Hotel_HotelId");
            RenameColumn(table: "dbo.Hotels", name: "CityId", newName: "CityHotel_CityId");
            RenameColumn(table: "dbo.Rooms", name: "HotelId", newName: "Hotel_HotelId");
            RenameColumn(table: "dbo.Guests", name: "HotelId", newName: "Hotel_HotelId");
            RenameColumn(table: "dbo.Bookings", name: "HotelId", newName: "Hotel_HotelId");
            CreateIndex("dbo.Hotels", "CityHotel_CityId");
            CreateIndex("dbo.Specifications", "R_RoomId");
            CreateIndex("dbo.Specifications", "G_GuestId");
            CreateIndex("dbo.Cities", "City_CityId");
            CreateIndex("dbo.Rooms", "Hotel_HotelId");
            CreateIndex("dbo.Bookings", "Hotel_HotelId");
            CreateIndex("dbo.Bookings", "Specification_BookingId");
            CreateIndex("dbo.Bookings", "R_RoomId");
            AddForeignKey("dbo.Hotels", "CityHotel_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.Rooms", "Hotel_HotelId", "dbo.Hotels", "HotelId");
            AddForeignKey("dbo.Bookings", "Hotel_HotelId", "dbo.Hotels", "HotelId");
            AddForeignKey("dbo.Bookings", "Specification_BookingId", "dbo.Specifications", "BookingId");
            AddForeignKey("dbo.Specifications", "R_RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.Specifications", "G_GuestId", "dbo.Guests", "GuestId");
            AddForeignKey("dbo.Bookings", "R_RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.Cities", "City_CityId", "dbo.Cities", "CityId");
        }
    }
}
