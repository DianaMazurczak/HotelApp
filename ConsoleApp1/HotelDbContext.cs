using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Hotel> Hotele { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomStandard> RoomStandards { get; set;}
        public DbSet<BookingRoom> BookingsRooms { get; set;}

    }
}
