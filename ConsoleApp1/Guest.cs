using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Guest
    {
        string firstName;
        string surname;
        string flatHouseNumber;        //I supposed that number of house can include a letter for example 41B, 34A.
        City city;

        public Guest(string firstName, string surname, string flatHouseNumber, City city)
        {
            FirstName = firstName;
            Surname = surname;
            FlatHouseNumber = flatHouseNumber;
            City = city;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string Surname { get => surname; set => surname = value; }
        public string FlatHouseNumber { get => flatHouseNumber; set => flatHouseNumber = value; }
        internal City City { get => city; set => city = value; }
    }
}
