namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        GuestId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        DateOfRegestration = c.DateTime(nullable: false),
                        NumberOfAdults = c.Int(nullable: false),
                        NumberOfChildren = c.Int(nullable: false),
                        R_RoomId = c.Int(),
                        Specification_BookingId = c.Int(),
                        Hotel_HotelId = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.R_RoomId)
                .ForeignKey("dbo.Specifications", t => t.Specification_BookingId)
                .ForeignKey("dbo.Hotels", t => t.Hotel_HotelId)
                .Index(t => t.GuestId)
                .Index(t => t.R_RoomId)
                .Index(t => t.Specification_BookingId)
                .Index(t => t.Hotel_HotelId);
            
            CreateTable(
                "dbo.BookingRooms",
                c => new
                    {
                        BookingId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookingId, t.RoomId })
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.BookingId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomStandardId = c.Int(nullable: false),
                        RoomNumber = c.String(),
                        Floor = c.Int(nullable: false),
                        MaxNumberOfGuests = c.Int(nullable: false),
                        Vacancy = c.Boolean(nullable: false),
                        Hotel_HotelId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.RoomStandards", t => t.RoomStandardId, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_HotelId)
                .Index(t => t.RoomStandardId)
                .Index(t => t.Hotel_HotelId);
            
            CreateTable(
                "dbo.RoomStandards",
                c => new
                    {
                        RoomStandardId = c.Int(nullable: false, identity: true),
                        PricePerPerson = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerChild = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RoomStandardId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        FlatHouseNumber = c.String(),
                        Hotel_HotelId = c.Int(),
                    })
                .PrimaryKey(t => t.GuestId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_HotelId)
                .Index(t => t.CityId)
                .Index(t => t.Hotel_HotelId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        Postcode = c.String(),
                        Street = c.String(),
                        Country = c.String(),
                        City_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .Index(t => t.City_CityId);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        DateOfCheckOut = c.DateTime(nullable: false),
                        G_GuestId = c.Int(),
                        R_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Guests", t => t.G_GuestId)
                .ForeignKey("dbo.Rooms", t => t.R_RoomId)
                .Index(t => t.G_GuestId)
                .Index(t => t.R_RoomId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        Phone = c.Long(nullable: false),
                        CityHotel_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.Cities", t => t.CityHotel_CityId)
                .Index(t => t.CityHotel_CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Guests", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "CityHotel_CityId", "dbo.Cities");
            DropForeignKey("dbo.Bookings", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Bookings", "Specification_BookingId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "R_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Specifications", "G_GuestId", "dbo.Guests");
            DropForeignKey("dbo.Bookings", "R_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Guests", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Bookings", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Rooms", "RoomStandardId", "dbo.RoomStandards");
            DropForeignKey("dbo.BookingRooms", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.BookingRooms", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Hotels", new[] { "CityHotel_CityId" });
            DropIndex("dbo.Specifications", new[] { "R_RoomId" });
            DropIndex("dbo.Specifications", new[] { "G_GuestId" });
            DropIndex("dbo.Cities", new[] { "City_CityId" });
            DropIndex("dbo.Guests", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Guests", new[] { "CityId" });
            DropIndex("dbo.Rooms", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Rooms", new[] { "RoomStandardId" });
            DropIndex("dbo.BookingRooms", new[] { "RoomId" });
            DropIndex("dbo.BookingRooms", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Bookings", new[] { "Specification_BookingId" });
            DropIndex("dbo.Bookings", new[] { "R_RoomId" });
            DropIndex("dbo.Bookings", new[] { "GuestId" });
            DropTable("dbo.Hotels");
            DropTable("dbo.Specifications");
            DropTable("dbo.Cities");
            DropTable("dbo.Guests");
            DropTable("dbo.RoomStandards");
            DropTable("dbo.Rooms");
            DropTable("dbo.BookingRooms");
            DropTable("dbo.Bookings");
        }
    }
}
