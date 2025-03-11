using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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
    /// Logika interakcji dla klasy AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        public BookingViewModel ViewModel { get; set; }

        public AddBooking(HotelDbContext context, Hotel currentHotel)
        {
            InitializeComponent();
            ViewModel = new BookingViewModel(context, currentHotel);
            DataContext = ViewModel;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                ViewModel.SaveBooking();
                MessageBox.Show("The new booking was saved correctly.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Zamknij okno po zapisie
            }
            else
            {
                MessageBox.Show("The form contains errors. Check the validity of the data entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class BookingViewModel
    {
        private readonly HotelDbContext _context;
        private readonly Hotel _currentHotel;

        [Required(ErrorMessage = "Pole 'Booking date' jest wymagane.")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Pole 'DateOfCheckOut' jest wymagane.")]
        public DateTime DateOfCheckOut { get; set; }

        [Required(ErrorMessage = "Pole 'Number of adults' jest wymagane.")]
        public int NumOfAdults { get; set; }

        [Required(ErrorMessage = "Pole 'Number of children' jest wymagane.")]
        public int NumOfChildren { get; set; }

        [Required(ErrorMessage = "Pole 'Room' jest wymagane.")]
        public Room Room { get; set; }

        [Required(ErrorMessage = "Pole 'Guest' jest wymagane.")]
        public Guest Guest { get; set; }

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Guest> Guests { get; set; }

        public BookingViewModel(HotelDbContext context, Hotel currentHotel)
        {
            _context = context;
            _currentHotel = currentHotel;
            LoadRoomsAndGuests();
        }

        private void LoadRoomsAndGuests()
        {
            Rooms = new ObservableCollection<Room>(_context.Rooms.Where(r => r.Hotel.HotelId == _currentHotel.HotelId));
            Guests = new ObservableCollection<Guest>(_context.Guests.Where(g => g.Hotel.HotelId == _currentHotel.HotelId));
        }

        public bool IsValid()
        {
            var results = new List<DataValidationResult>();
            return DataValidator.TryValidateObject(this, new DataValidationContext(this), results, true);
        }


        public void SaveBooking()
        {
            var newBooking = new Booking(Guest, BookingDate, DateOfCheckOut, NumOfAdults, NumOfChildren, Room, _currentHotel);
            _context.Bookings.Add(newBooking);
            _context.SaveChanges();
        }
    }
}
