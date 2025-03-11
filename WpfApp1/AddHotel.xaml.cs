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
using DataValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using DataValidator = System.ComponentModel.DataAnnotations.Validator;
using DataValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy AddHotel.xaml
    /// </summary>
    public partial class AddHotel : Window
    {
        public AddHotelViewModel ViewModel { get; set; }

        public AddHotel(HotelDbContext context, Hotel hotel)
        {
            InitializeComponent();
            ViewModel = new AddHotelViewModel(context, hotel);
            DataContext = ViewModel;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                ViewModel.SaveHotel();
                MessageBox.Show("Hotel został zapisany poprawnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Zamyka okno z sukcesem
            }
            else
            {
                MessageBox.Show("Formularz zawiera błędy. Popraw dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Zamknięcie okna bez zapisu
        }
    }

    public class AddHotelViewModel
    {
        private readonly HotelDbContext _context;
        private Hotel _hotel;

        [Required(ErrorMessage = "Pole 'Nazwa hotelu' jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole 'Telefon' jest wymagane.")]
        [Range(100000000, 999999999, ErrorMessage = "Numer telefonu musi mieć 9 cyfr.")]
        public long Phone { get; set; }

        // Dane adresowe
        [Required(ErrorMessage = "Pole 'Miasto' jest wymagane.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Pole 'Kod pocztowy' jest wymagane.")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Pole 'Ulica' jest wymagane.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole 'Kraj' jest wymagane.")]
        public string Country { get; set; }

        public AddHotelViewModel(HotelDbContext context, Hotel hotel)
        {
            _context = context;
            _hotel = hotel;
        }

        public bool IsValid()
        {
            var results = new List<DataValidationResult>();
            return DataValidator.TryValidateObject(this, new DataValidationContext(this), results, true);
        }


        public void SaveHotel()
        {
            // Utworzenie nowego miasta
            var city = new City(CityName, Postcode, Street, Country);

            // Dodanie miasta do bazy, jeśli nie istnieje
            var existingCity = _context.Cities.FirstOrDefault(c =>
                c.CityName == CityName &&
                c.Postcode == Postcode &&
                c.Street == Street &&
                c.Country == Country);

            if (existingCity == null)
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
            }
            else
            {
                city = existingCity;
            }

            // Sprawdzenie czy hotel o tej samej nazwie istnieje
            if (_context.Hotele.Any(h => h.Name == Name))
            {
                throw new InvalidOperationException("Hotel o tej nazwie już istnieje.");
            }

            var newHotel = new Hotel(Name, Phone, existingCity);
            _context.Hotele.Add(newHotel);
            _context.SaveChanges();
        }
    }
}
