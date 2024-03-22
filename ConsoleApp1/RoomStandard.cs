using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        Standard standard;
        decimal pricePerPerson;
        decimal pricePerChild;
        public virtual List<Room> Rooms { get; set; }

        public RoomStandard(Standard standard, decimal pricePerPerson, decimal pricePerChild)
        {
            Standard = standard;
            PricePerPerson = pricePerPerson;
            PricePerChild = pricePerChild;
        }

        public decimal PricePerPerson { get => pricePerPerson; set => pricePerPerson = value; }
        public decimal PricePerChild { get => pricePerChild; set => pricePerChild = value; }
        internal Standard Standard { get => standard; set => standard = value; }
    }
}
