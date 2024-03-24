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
    /// Logika interakcji dla klasy AddHotel.xaml
    /// </summary>
    public partial class AddHotel : Window
    {
        public AddHotelViewModel ViewModel { get; set; }
        public Hotel newHotel;
        private readonly HotelDbContext dc; // Deklaracja pola HotelDbContext

        public AddHotel(Hotel newHotel, HotelDbContext dc)
        {
            InitializeComponent();
            ViewModel = new AddHotelViewModel();
            DataContext = ViewModel;
            this.newHotel = newHotel;
            this.dc = dc;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid() && !dc.Hotele.Any(x => x.Name == ViewModel.Name))
            {
                string name = ViewModel.Name;
                long phone = ViewModel.Phone;
                string postcode = ViewModel.Postcode;
                string city = ViewModel.City;
                string country = ViewModel.Country;
                string street = ViewModel.Street;
                string number = ViewModel.Number;
                City newCity = new();
                City existiongCity = dc.Cities.FirstOrDefault(c => c.CityName == name);
                if (existiongCity != null)
                {
                    newCity = existiongCity;
                }
                else
                {
                    newCity = new(city, postcode, street, number);
                    dc.Cities.Add(newCity);
                    dc.SaveChanges();
                }
                this.newHotel = new Hotel(name, phone, newCity);
                dc.Hotele.Add(newHotel);
                dc.SaveChanges();
                MessageBox.Show("The new hotel was saved correctly.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("The form contains errors. Check the validity of the data entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            dc.SaveChanges();
            Close();
        }
    }
    public class AddHotelViewModel
    {
        [Required(ErrorMessage = "Pole 'First name' jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole 'Surname' jest wymagane.")]
        public long Phone { get; set; }

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
