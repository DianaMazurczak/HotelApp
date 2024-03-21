using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        internal List<Guest> GuestsList { get => guestsList; set => guestsList = value; }
        internal List<Room> RoomList { get => roomList; set => roomList = value; }
        public long Phone { get => phone; set => phone = value; }
        internal City CityHotel { get => cityHotel; set => cityHotel = value; }
        internal List<Booking> BookingList { get => bookingList; set => bookingList = value; }

        List<Guest> guestsList;
        List<Room> roomList;
        long phone;
        City cityHotel;
        List<Booking> bookingList;

        public Hotel(long phone, City cityHotel)
        {
            GuestsList = new List<Guest>();
            RoomList = new List<Room>();
            Phone = phone;
            CityHotel = cityHotel;
            BookingList = new List<Booking>();
        }
    }
}
