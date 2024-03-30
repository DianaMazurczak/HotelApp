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
        public List<Guest> GuestsList { get => guestsList; set => guestsList = value; }
        public List<Room> RoomList { get => roomList; set => roomList = value; }
        public long Phone { get => phone; set => phone = value; }
        internal City CityHotel { get => cityHotel; set => cityHotel = value; }
        public List<Booking> BookingList { get => bookingList; set => bookingList = value; }
        public string Name { get => name; set => name = value; }

        string name;
        long phone;
        City cityHotel;
        List<Guest> guestsList;
        List<Room> roomList;
        List<Booking> bookingList;

        public Hotel() {
            GuestsList = new List<Guest>();
            RoomList = new List<Room>();
            BookingList = new List<Booking>();
        }
        public Hotel(string name, long phone, City cityHotel) :this()
        {
            Phone = phone;
            CityHotel = cityHotel;
            Name = name;
        }
        public void AddingGuest(string firstName, string surname, string flatHouseNumber, City city)
        {
            Guest newGuest = new(firstName, surname, flatHouseNumber, city);
            GuestsList.Add(newGuest);
        }
        public void AddingGuest2(Guest newGuest)
        {
            GuestsList.Add(newGuest);
        }
        public void AddingBooking(Guest guest, DateTime bookingDate, int numberOfAdults, int numberOfChildren, Room room)
        {
            Booking newBooking = new(guest, bookingDate, numberOfAdults, numberOfChildren, room);
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
