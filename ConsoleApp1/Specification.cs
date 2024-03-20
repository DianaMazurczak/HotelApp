using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Specification
    {
        Guest g;
        Room r;
        DateTime dateOfCheckOut;

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
