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
        public virtual List<Guest> Guests { get; set; }
        string cityName;
        string postcode;
        string street;
        string country;
        private List<City> cities;

        public City() { }
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
        public List<City> Cities { get => cities; set => cities = value; }

        public override string ToString()
        {
            return Street + "\n" + Postcode + " " + CityName + "\n" + Country;
        }
    }
}
