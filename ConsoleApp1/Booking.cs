using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Booking
    {
        Guest g;
        DateTime bookingDate;
        DateTime dateOfRegestration;
        int numberOfAdults;
        int numberOfChildren;
        Room r;

        public Booking(Guest g, DateTime bookingDate, int numberOfAdults, int numberOfChildren, Room r)
        {
            G = g;
            BookingDate = bookingDate;
            DateOfRegestration = DateTime.Now;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            R = r;
        }

        public DateTime BookingDate { get => bookingDate; set => bookingDate = value; }
        public DateTime DateOfRegestration { get => dateOfRegestration; set => dateOfRegestration = value; }
        public int NumberOfAdults { get => numberOfAdults; set => numberOfAdults = value; }
        public int NumberOfChildren { get => numberOfChildren; set => numberOfChildren = value; }
        internal Guest G { get => g; set => g = value; }
        internal Room R { get => r; set => r = value; }
    }
}
