using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum Standard { high = 1, medium = 2, low = 3}

    public class RoomStandard
    {
        [Key]
        public int RoomStandardId { get; set; }

        public Standard Standard { get; set; }
        public decimal PricePerPerson { get; set; }
        public decimal PricePerChild { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        public RoomStandard() { }

        public RoomStandard(Standard standard, decimal pricePerPerson, decimal pricePerChild)
        {
            Standard = standard;
            PricePerPerson = pricePerPerson;
            PricePerChild = pricePerChild;
        }
        //public RoomStandard(Standard standard, decimal pricePerPerson, decimal pricePerChild, int roomStandardId):this(standard, pricePerPerson, pricePerChild)
        //{
        //    RoomStandardId = roomStandardId;
        //}
    }
}
