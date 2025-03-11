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

        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public bool Vacancy { get; set; } = true;

        public int RoomStandardId { get; set; }
        public virtual RoomStandard RoomStandard { get; set; }

        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public Room() { }

        public Room(int roomId, string roomNumber, int floor, int maxNumberOfGuests, RoomStandard roomStandard, bool vacancy, Hotel hotel) :this()
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            Floor = floor;
            MaxNumberOfGuests = maxNumberOfGuests;
            RoomStandard = roomStandard;
            Vacancy = vacancy;
            Hotel = hotel;
        }

        //public Room(string roomNumber, int floor, int maxNumberOfGuests, int rsId, Hotel hotel):this()
        //{
        //    RoomNumber = roomNumber;
        //    Floor = floor;
        //    MaxNumberOfGuests = maxNumberOfGuests;
        //    RsId = rsId;
        //    Vacancy = true;
        //    Hotel = hotel;
        //}

        public override string ToString()
        {
            return RoomNumber + " - Max: " + MaxNumberOfGuests;
        }
    }
    //public class RoomMap : ClassMap<Room>
    //{
    //    public RoomMap()
    //    {
    //        Map(r => r.RoomId)
    //            .Index(0)
    //            .Name("RoomId");
    //        Map(r => r.RsId)
    //            .Index(1)
    //            .Name("RoomStandardId");
    //        Map(r => r.RoomNumber)
    //            .Index(2)
    //            .Name("RoomNumber");
    //        Map(r => r.Floor)
    //            .Index(3)
    //            .Name("Floor");
    //        Map(r => r.MaxNumberOfGuests)
    //            .Index(4)
    //            .Name("MaxNumberOfGuests");
    //    }
    //}
}
