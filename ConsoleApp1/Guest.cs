﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        string firstName;
        string surname;
        string flatHouseNumber; //I supposed that number of house can include a letter for example 41B, 34A.
        //City city;
        public virtual List<Booking> Bookings { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public Hotel Hotel { get; set; }

        public Guest() { 
        }
        public Guest(string firstName, string surname, string flatHouseNumber, City city) : this()
        {
            FirstName = firstName;
            Surname = surname;
            FlatHouseNumber = flatHouseNumber;
            City = city;
        }

        public Guest(string firstName, string surname, string flatHouseNumber, City city, Hotel hotel) : this(firstName, surname, flatHouseNumber, city)
        {
            Hotel = hotel;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string Surname { get => surname; set => surname = value; }
        public string FlatHouseNumber { get => flatHouseNumber; set => flatHouseNumber = value; }
        //internal City City { get => city; set => city = value; }

        public override string ToString()
        {
            return FirstName + " " + Surname;
        }
    }
}
