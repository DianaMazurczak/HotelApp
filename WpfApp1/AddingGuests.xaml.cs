using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy AddingGuests.xaml
    /// </summary>
    public partial class AddingGuests : Window
    {
        public AddGuestViewModel ViewModel { get; set; }
        public Hotel CurrentHotel { get; set; }
        public Guest NewGuest;
        HotelDbContext context = new();
        public AddingGuests(Guest newGuest, Hotel currentHotel, HotelDbContext context)
        {
            InitializeComponent();
            ViewModel = new AddGuestViewModel();
            DataContext = ViewModel;
            CurrentHotel = currentHotel;
            NewGuest = newGuest;
            this.context = context;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                string name = ViewModel.FirstName;
                string surname = ViewModel.Surname;
                string postcode = ViewModel.Postcode;
                string city = ViewModel.City;
                string country = ViewModel.Country;
                string street = ViewModel.Street;
                string number = ViewModel.Number;
                City newCity = new();
                City existiongCity = context.Cities.FirstOrDefault(c => c.Postcode == postcode);
                if(existiongCity != null)
                {
                    newCity = existiongCity;
                }
                else 
                { 
                    newCity = new(city, postcode, street, number); 
                    context.Cities.Add(newCity);
                    context.SaveChanges();
                }
                NewGuest = new Guest(name, surname, number, newCity, CurrentHotel);
                context.Guests.Add(NewGuest);
                context.SaveChanges();
                MessageBox.Show("The new guest was saved correctly.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("The form contains errors. Check the validity of the data entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            //context.SaveChanges();
            Close();
        }
    }
    public class AddGuestViewModel
    {
        [Required(ErrorMessage = "Pole 'First name' jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole 'Surname' jest wymagane.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole 'Postcode' jest wymagane.")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Pole 'City' jest wymagane.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole 'Street' jest wymagane.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole 'Flat or house number' jest wymagane.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Pole 'Country' jest wymagane.")]
        public string Country { get; set; }

        public Guest NewGuest { get; set; }
        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
