using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConsoleApp1;
using CsvHelper;
using CsvHelper.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HotelDbContext dc;
        public Hotel CurrentHotel;
        public MainWindow()
        {
            dc = new HotelDbContext();
            InitializeComponent();
            var hotelNames = dc.Hotele.Select(x => x.Name).ToList();
            cbHotels.ItemsSource = hotelNames;
            ReadData();
        }
        public void ReadData()
        {
            CurrentHotel = (Hotel)cbHotels.SelectedItem;
            if(CurrentHotel is not null )
            {
                GuestList.ItemsSource = new ObservableCollection<Guest>(dc.Guests.Where(g => g.Hotel.HotelId == CurrentHotel.HotelId).ToList());
                RoomList.ItemsSource = new ObservableCollection<Room>(dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList());
                //CurrentHotel.AddRoomStandard(dc);
            }
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            string selectedHotelName = cbHotels.SelectedItem?.ToString();
            CurrentHotel = dc.Hotele.FirstOrDefault(h => h.Name == selectedHotelName);
            if (CurrentHotel is null)
            {
                MessageBox.Show("Proszę wybrać hotel z listy.", "Brak wybranego hotelu", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Guest newGuest = new();
            AddingGuests window = new AddingGuests(newGuest, CurrentHotel, dc);
            bool? result = window.ShowDialog();
            if(result == true )
            {
                CurrentHotel.AddingGuest2(newGuest);
                dc.Guests.Add(newGuest);
                dc.SaveChanges();
                GuestList.ItemsSource = new ObservableCollection<Guest>(CurrentHotel.GuestsList);
            }
            window.DataContext = new AddGuestViewModel { NewGuest = newGuest };
        }

        private void btnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            Hotel newHotel = new();
            AddHotel window = new AddHotel(newHotel, dc);
            bool? result = window.ShowDialog();
            if(result == true )
            {
                dc.Hotele.Add(newHotel);
                dc.SaveChanges();
            }
            var hotelNames = dc.Hotele.Select(x => x.Name).ToList();
            cbHotels.ItemsSource = hotelNames;
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            CurrentHotel = dc.Hotele.FirstOrDefault(h => h.Name == cbHotels.SelectedItem);
            GuestList.ItemsSource = new ObservableCollection<Guest>(dc.Guests.Where(g => g.Hotel.HotelId == CurrentHotel.HotelId).ToList());
            CurrentHotel.AddRoomStandard(dc);
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                //HasHeaderRecord = false,
                HeaderValidated = null,
                MissingFieldFound = null,
            };
            using var reader = new StreamReader("C:/Users/dmazu/Desktop/ConsoleApp1/Zeszyt1.csv");
            using var csv = new CsvReader(reader, configuration);
            csv.Context.RegisterClassMap<RoomMap>();
            var records = csv.GetRecords<Room>().ToList();
            foreach (var room in records)
            {
                room.Hotel = CurrentHotel;
                RoomStandard roomStandard = dc.RoomStandards.FirstOrDefault(rs => rs.RoomStandardId == room.RsId);
                room.RoomStandard = roomStandard;
                //room.RoomStandardId = roomStandard.RoomStandardId;
                dc.Rooms.Add(room);
            }
            dc.SaveChanges();
            MessageBox.Show($"Pobrano {dc.Rooms.Count()} pokoi dla hotelu {CurrentHotel.Name}", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            RoomList.ItemsSource = new ObservableCollection<Room>(dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList());
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            string selectedHotelName = cbHotels.SelectedItem?.ToString();
            CurrentHotel = dc.Hotele.FirstOrDefault(h => h.Name == selectedHotelName);
            if (CurrentHotel is null)
            {
                MessageBox.Show("Proszę wybrać hotel z listy.", "Brak wybranego hotelu", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            RoomDataImporter roomDataImporter = new RoomDataImporter();
            string filePath = "C:/Users/dmazu/Desktop/ConsoleApp1/Zeszyt1.csv";
            roomDataImporter.ImportRoomsFromCSV(filePath, dc, CurrentHotel);
            MessageBox.Show("Import successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Booking newBooking = new();
            AddBooking window = new AddBooking(newBooking, dc, CurrentHotel);
            window.cbRoom.ItemsSource = new ObservableCollection<Room>(dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList());
            bool? result = window.ShowDialog();
            if(result == true )
            {
                dc.SaveChanges();
            }
            window.DataContext = new BookingViewModel();
        }
    }
}