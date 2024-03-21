using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Specification
    {
        //[Key]
        //public int SpecificationId {  get; set; }
        Guest g;
        Room r;
        DateTime dateOfCheckOut;
        [Key]
        public int BookingId { get; set; }

        public Specification(Guest g, Room r, DateTime dateOfCheckOut)
        {
            G = g;
            R = r;
            DateOfCheckOut = dateOfCheckOut;
        }

        public DateTime DateOfCheckOut { get => dateOfCheckOut; set => dateOfCheckOut = value; }
        internal Guest G { get => g; set => g = value; }
        internal Room R { get => r; set => r = value; }
    }
}
