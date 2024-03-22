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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HotelDbContext dc;
        public Hotel CurrentHotel;
        public MainWindow()
        {
            dc = new HotelDbContext();
            InitializeComponent();
            ReadData();
        }
        public void ReadData()
        {
            CurrentHotel = (Hotel)cbHotels.SelectedItem;
            GuestList.ItemsSource = CurrentHotel.GuestsList;
            RoomList.ItemsSource = CurrentHotel.RoomList;
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            Guest newGuest = new();
            AddingGuests window = new AddingGuests(newGuest, CurrentHotel);
        }
    }
}