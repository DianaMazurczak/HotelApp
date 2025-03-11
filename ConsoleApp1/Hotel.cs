using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual List<Guest> GuestsList { get; set; }
        public virtual List<Room> RoomList { get; set; }
        public virtual List<Booking> BookingList { get; set; } 

        public Hotel() 
        {
            GuestsList = new List<Guest>();
            RoomList = new List<Room>();
            BookingList = new List<Booking>();
        }
        public Hotel(string name, long phone, City city) :this()
        {
            Phone = phone;
            City = city;
            Name = name;
        }
        public void AddingGuest(string firstName, string surname, string flatHouseNumber, City city)
        {
            Guest newGuest = new(firstName, surname, flatHouseNumber, city, this);
            GuestsList.Add(newGuest);
        }
        public void AddingGuest2(Guest newGuest)
        {
            GuestsList.Add(newGuest);
        }
        public void AddingBooking(Guest guest, DateTime bookingDate, DateTime dateOfCheckOut, int numberOfAdults, int numberOfChildren, Room room)
        {
            Booking newBooking = new(guest, bookingDate, dateOfCheckOut, numberOfAdults, numberOfChildren, room, this);
            BookingList.Add(newBooking);
        }
        public void AddRoomStandard(HotelDbContext dc)
        {
            RoomStandard rs1 = new(Standard.medium, 200, 180, 1);
            RoomStandard rs2 = new(Standard.medium, 180, 150, 2);
            RoomStandard rs3 = new(Standard.medium, 150, 100, 1);
            dc.RoomStandards.Add(rs1);
            dc.RoomStandards.Add(rs2);
            dc.RoomStandards.Add(rs3);
            dc.SaveChanges();
        }
    }
}
