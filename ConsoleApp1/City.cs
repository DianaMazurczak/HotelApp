using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class City
    {
        string cityName;
        string postcode;
        string street;
        string country;

        public City(string cityName, string postcode, string street, string country)
        {
            CityName = cityName;
            Postcode = postcode;
            Street = street;
            Country = country;
        }

        public string CityName { get => cityName; set => cityName = value; }
        public string Postcode { get => postcode; set => postcode = value; }
        public string Street { get => street; set => street = value; }
        public string Country { get => country; set => country = value; }

        public override string ToString()
        {
            return Street + "\n" + Postcode + " " + CityName + "\n" + Country;
        }
    }
}
