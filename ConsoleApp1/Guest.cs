using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FlatHouseNumber { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public int? HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public virtual List<Booking> Bookings { get; set; }

        public Guest() 
        {
            Bookings = new List<Booking>();
        }

        public Guest(string firstName, string surname, string flatHouseNumber, City city, Hotel hotel) : this()
        {
            FirstName = firstName;
            Surname = surname;
            FlatHouseNumber = flatHouseNumber;
            City = city;
            Hotel = hotel;
        }

        public override string ToString()
        {
            return FirstName + " " + Surname;
        }
    }
}
