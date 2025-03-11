using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        public string CityName { get; set; }
        public string Postcode { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

        public virtual List<Guest> Guests { get; set; }

        public City() { }

        public City(string cityName, string postcode, string street, string country)
        {
            CityName = cityName;
            Postcode = postcode;
            Street = street;
            Country = country;
        }

        public override string ToString()
        {
            return Street + "\n" + Postcode + " " + CityName + "\n" + Country;
        }
    }
}
