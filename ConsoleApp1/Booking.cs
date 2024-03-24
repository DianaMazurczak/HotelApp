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
        public int BookingId {  get; set; }
        //Guest g;
        DateTime bookingDate;
        DateTime dateOfRegestration;
        int numberOfAdults;
        int numberOfChildren;
        Room r;
        public virtual List<BookingRoom> BookingRooms { get; set; }
        public virtual Specification Specification { get; set; }
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual Hotel Hotel { get; set; }

        public Booking() { }
        public Booking(Guest guest, DateTime bookingDate, int numberOfAdults, int numberOfChildren, Room r)
        {
            Guest = guest;
            BookingDate = bookingDate;
            DateOfRegestration = DateTime.Now;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            R = r;
        }
        public Booking(Guest guest, DateTime bookingDate, int numberOfAdults, int numberOfChildren, Room r, Hotel hotel): this(guest, bookingDate, numberOfAdults, numberOfChildren, r)
        {
            Hotel = hotel;
        }
        public DateTime BookingDate { get => bookingDate; set => bookingDate = value; }
        public DateTime DateOfRegestration { get => dateOfRegestration; set => dateOfRegestration = value; }
        public int NumberOfAdults { get => numberOfAdults; set => numberOfAdults = value; }
        public int NumberOfChildren { get => numberOfChildren; set => numberOfChildren = value; }
        //internal Guest G { get => g; set => g = value; }
        internal Room R { get => r; set => r = value; }
    }
}
