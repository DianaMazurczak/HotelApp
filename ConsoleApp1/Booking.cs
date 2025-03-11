using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }

        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public virtual List<BookingRoom> BookingRooms { get; set; } = new();
        public virtual Specification Specification { get; set; }

        public Booking(Guest guest, DateTime bookingDate, int numberOfAdults, int numberOfChildren, Room r, Hotel hotel)
        {
            Hotel = hotel;
            Guest = guest;
            BookingDate = bookingDate;
            DateOfRegistration = DateTime.Now;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
        }
    }
}
