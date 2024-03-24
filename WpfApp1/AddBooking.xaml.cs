using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        public BookingViewModel ViewModel { get; set; }
        HotelDbContext dc = new();
        public Hotel CurrentHotel { get; set; }
        public AddBooking(Booking newBooking, HotelDbContext context, Hotel currentHotel)
        {
            InitializeComponent();
            dc = context;
            ViewModel = new BookingViewModel();
            DataContext = ViewModel;
            CurrentHotel = currentHotel;
            cbGuest.ItemsSource = new ObservableCollection<Guest>(dc.Guests.Where(g => g.Hotel.HotelId == CurrentHotel.HotelId).ToList());
            cbRoom.ItemsSource = new ObservableCollection<Room>(dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                DateTime bookingDate = ViewModel.BookingDate;
                int numOfAdults = ViewModel.NumOfAdults;
                int numOfChildren = ViewModel.NumOfChildren;
                Room room = ViewModel.Room;
                Guest guest = ViewModel.Guest;
                Booking newBooking = new(guest, bookingDate, numOfAdults, numOfChildren, room, CurrentHotel);
                dc.Bookings.Add(newBooking);
                dc.SaveChanges();
                MessageBox.Show("The new booking was saved correctly.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
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
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Pole 'Booking date' jest wymagane.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Pole 'Number of adults' jest wymagane.")]
        public int NumOfAdults { get; set; }

        [Required(ErrorMessage = "Pole 'Number of children' jest wymagane.")]
        public int NumOfChildren { get; set; }

        [Required(ErrorMessage = "Pole 'Room' jest wymagane.")]
        public Room Room { get; set; }

        [Required(ErrorMessage = "Pole 'Guest' jest wymagane.")]
        public Guest Guest { get; set; }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
