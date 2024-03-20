using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Room
    {
        string roomNumber;
        int floor;
        int maxNumberOfGuests;
        RoomStandard roomStandard;
        bool vacancy;

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
        internal RoomStandard RoomStandard { get => roomStandard; set => roomStandard = value; }
    }
}
