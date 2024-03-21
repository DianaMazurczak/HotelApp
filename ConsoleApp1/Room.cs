using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Room
    {
        [Key]
        public int RoomId { get; set; }
        string roomNumber;
        int floor;
        int maxNumberOfGuests;
        //RoomStandard roomStandard;
        bool vacancy;
        public virtual List<BookingRoom> BookingRooms { get; set; }
        public virtual RoomStandard RoomStandard { get; set; }
        public int RoomStandardId { get; set; }
        public Room(string roomNumber, int floor, int maxNumberOfGuests, RoomStandard roomStandard, bool vacancy)
        {
            RoomNumber = roomNumber;
            Floor = floor;
            MaxNumberOfGuests = maxNumberOfGuests;
            RoomStandard = roomStandard;
            Vacancy = vacancy;
        }

        public string RoomNumber { get => roomNumber; set => roomNumber = value; }
        public int Floor { get => floor; set => floor = value; }
        public int MaxNumberOfGuests { get => maxNumberOfGuests; set => maxNumberOfGuests = value; }
        public bool Vacancy { get => vacancy; set => vacancy = value; }
        //internal RoomStandard RoomStandard { get => roomStandard; set => roomStandard = value; }
    }
}
