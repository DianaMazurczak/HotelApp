using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class BookingRoom
    {
        // Composite key
        [Key]
        [Column(Order = 1)]
        public int BookingId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RoomId { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public Room Room { get; set; }
    }
}
