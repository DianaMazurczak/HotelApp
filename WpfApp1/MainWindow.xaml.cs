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
        private readonly HotelDbContext dc;
        private Hotel CurrentHotel;

        public MainWindow()
        {
            InitializeComponent();
            dc = new HotelDbContext();
            LoadHotels();
        }

        private void LoadHotels()
        {
            var hotelNames = dc.Hotele.Select(x => x.Name).ToList();
            cbHotels.ItemsSource = hotelNames;
        }

        private void cbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCurrentHotel();
            ReadData();
        }

        private void UpdateCurrentHotel()
        {
            string selectedHotelName = cbHotels.SelectedItem?.ToString();
            CurrentHotel = dc.Hotele.FirstOrDefault(h => h.Name == selectedHotelName);
        }

        public void ReadData()
        {
            if (CurrentHotel != null)
            {
                GuestList.ItemsSource = new ObservableCollection<Guest>(
                    dc.Guests.Where(g => g.Hotel.HotelId == CurrentHotel.HotelId).ToList());
                RoomList.ItemsSource = new ObservableCollection<Room>(
                    dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList());
            }
            else
            {
                GuestList.ItemsSource = null;
                RoomList.ItemsSource = null;
            }
        }

        private void btnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            Hotel newHotel = new();
            var window = new AddHotel(dc, newHotel);
            bool? result = window.ShowDialog();

            if (result == true)
            {
                dc.Hotele.Add(newHotel);
                dc.SaveChanges();
                LoadHotels();
                cbHotels.SelectedItem = newHotel.Name;
            }
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (!EnsureHotelSelected()) return;

            var window = new AddingGuests(CurrentHotel, dc);
            if (window.ShowDialog() == true)
            {
                ReadData(); // Odświeżenie gości
            }
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (!EnsureHotelSelected()) return;

            var window = new AddBooking(dc, CurrentHotel)
            {
                cbRoom = { ItemsSource = new ObservableCollection<Room>(dc.Rooms.Where(r => r.Hotel.HotelId == CurrentHotel.HotelId).ToList()) }
            };

            if (window.ShowDialog() == true)
            {
                dc.SaveChanges();
                MessageBox.Show("Rezerwacja została dodana.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (!EnsureHotelSelected()) return;

            ReadData(); // Odświeżenie listy
            CurrentHotel.AddRoomStandard(dc); // Dodanie standardów pokoi (jeśli nie ma)

            if (!dc.Rooms.Any(r => r.Hotel.HotelId == CurrentHotel.HotelId))
            {
                ImportRoomsFromCSV();
                ReadData();
            }
        }

        private void ImportRoomsFromCSV()
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            try
            {
                using var reader = new StreamReader("C:/Users/dmazu/Desktop/ConsoleApp1/Zeszyt1.csv");
                using var csv = new CsvReader(reader, configuration);
                csv.Context.RegisterClassMap<RoomMap>();
                var records = csv.GetRecords<Room>().ToList();

                foreach (var room in records)
                {
                    room.Hotel = CurrentHotel;
                    room.RoomStandard = dc.RoomStandards.FirstOrDefault(rs => rs.RoomStandardId == room.RoomStandardId);
                    dc.Rooms.Add(room);
                }

                dc.SaveChanges();
                MessageBox.Show($"Pobrano {records.Count} pokoi dla hotelu {CurrentHotel.Name}", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas importu pokoi: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Sprawdza, czy hotel jest wybrany, jeśli nie, wyświetla komunikat.
        /// </summary>
        private bool EnsureHotelSelected()
        {
            if (cbHotels.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać hotel z listy.", "Brak wybranego hotelu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            UpdateCurrentHotel();
            return CurrentHotel != null;
        }
    }
}
