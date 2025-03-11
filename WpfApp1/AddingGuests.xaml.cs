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
    /// Logika interakcji dla klasy AddingGuests.xaml
    /// </summary>
    public partial class AddingGuests : Window
    {
        public AddGuestViewModel ViewModel { get; set; }

        public AddingGuests(Hotel currentHotel, HotelDbContext context)
        {
            InitializeComponent();
            ViewModel = new AddGuestViewModel(context, currentHotel);
            DataContext = ViewModel;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                ViewModel.SaveGuest();
                MessageBox.Show("Gość został zapisany poprawnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Zamyka okno z sukcesem
            }
            else
            {
                MessageBox.Show("Formularz zawiera błędy. Popraw dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Zamknięcie okna
        }
    }

    public class AddGuestViewModel
    {
        private readonly HotelDbContext _context;
        private readonly Hotel _hotel;

        // Dane gościa
        [Required(ErrorMessage = "Pole 'Imię' jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole 'Nr domu/mieszkania' jest wymagane.")]
        public string FlatHouseNumber { get; set; }

        // Dane adresowe
        [Required(ErrorMessage = "Pole 'Miasto' jest wymagane.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Pole 'Kod pocztowy' jest wymagane.")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Pole 'Ulica' jest wymagane.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole 'Kraj' jest wymagane.")]
        public string Country { get; set; }

        public AddGuestViewModel(HotelDbContext context, Hotel hotel)
        {
            _context = context;
            _hotel = hotel;
        }

        public bool IsValid()
        {
            var results = new List<DataValidationResult>();
            return DataValidator.TryValidateObject(this, new DataValidationContext(this), results, true);
        }

        public void SaveGuest()
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

            var newGuest = new Guest(FirstName, Surname, FlatHouseNumber, city, _hotel);

            _context.Guests.Add(newGuest);
            _context.SaveChanges();
        }

    }
}
