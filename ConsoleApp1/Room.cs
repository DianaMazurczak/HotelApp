using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        string roomNumber;
        int floor;
        int maxNumberOfGuests;
        public bool vacancy;
        int rsId;
        //public virtual List<BookingRoom> BookingRooms { get; set; }
        public virtual RoomStandard RoomStandard { get; set; }
        public int RoomStandardId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public Room() { }
        public Room(int roomId, string roomNumber, int floor, int maxNumberOfGuests, RoomStandard roomStandard, bool vacancy) :this()
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            Floor = floor;
            MaxNumberOfGuests = maxNumberOfGuests;
            RoomStandard = roomStandard;
            Vacancy = vacancy;
        }
        public Room(int roomId, string roomNumber, int floor, int maxNumberOfGuests, RoomStandard roomStandard, bool vacancy, Hotel hotel): this(roomId, roomNumber, floor, maxNumberOfGuests, roomStandard, vacancy)
        {
            Hotel = hotel;
        }

        public Room(string roomNumber, int floor, int maxNumberOfGuests, int rsId):this()
        {
            RoomNumber = roomNumber;
            Floor = floor;
            MaxNumberOfGuests = maxNumberOfGuests;
            RsId = rsId;
            Vacancy = true;
        }
        public Room(string roomNumber, int floor, int maxNumberOfGuests, int rsId, bool vacancy, Hotel hotel) : this(roomNumber, floor, maxNumberOfGuests, rsId)
        {
            Hotel = hotel;
        }
        public string RoomNumber { get => roomNumber; set => roomNumber = value; }
        public int Floor { get => floor; set => floor = value; }
        public int MaxNumberOfGuests { get => maxNumberOfGuests; set => maxNumberOfGuests = value; }
        public bool Vacancy { get => vacancy; set => vacancy = value; }
        public int RsId { get => rsId; set => rsId = value; }

        //internal RoomStandard RoomStandard { get => roomStandard; set => roomStandard = value; }

        public override string ToString()
        {
            return RoomNumber + " " + MaxNumberOfGuests;
        }
    }
    public class RoomMap : ClassMap<Room>
    {
        public RoomMap()
        {
            Map(r => r.RoomId)
                .Index(0)
                .Name("RoomId");
            Map(r => r.RsId)
                .Index(1)
                .Name("RoomStandardId");
            Map(r => r.RoomNumber)
                .Index(2)
                .Name("RoomNumber");
            Map(r => r.Floor)
                .Index(3)
                .Name("Floor");
            Map(r => r.MaxNumberOfGuests)
                .Index(4)
                .Name("MaxNumberOfGuests");
        }
    }
}
