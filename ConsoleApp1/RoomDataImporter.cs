using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class RoomDataImporter
    {
        public void ImportRoomsFromCSV(string filePath, HotelDbContext dc, Hotel hotel)
        {

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Pomijaj pierwszą linię, ponieważ zawiera nagłówki kolumn
                    reader.ReadLine();

                    // Odczytuj kolejne linie z pliku
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Podziel linię na poszczególne wartości za pomocą przecinka jako separatora
                        string[] values = line.Split(';');

                        // Parsuj wartości i twórz obiekt Room
                        if (values.Length >= 5)
                        {
                            int roomId = int.Parse(values[0]);
                            string roomNumber = values[1];
                            int floor = int.Parse(values[2]);
                            int maxNumberOfGuests = int.Parse(values[3]);
                            int roomStandardId = int.Parse(values[4]);
                            RoomStandard roomStandard = dc.RoomStandards.FirstOrDefault(rs => rs.RoomStandardId == roomStandardId);
                            bool vacancy = true;
                            // Poniżej możesz dodać dodatkowe parsowanie dla innych właściwości, takich jak RoomStandardId, itp.
                            // Możesz również utworzyć obiekty RoomStandard, Hotel itp. na podstawie dodatkowych wartości w pliku CSV
                            // hotel.AddRoomStandard(dc);
                            Room room = new Room(roomId, roomNumber, floor, maxNumberOfGuests, roomStandard, vacancy, hotel);
                            dc.Rooms.Add(room);
                            hotel.RoomList.Add(room);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Obsługa wyjątku w przypadku problemów z odczytem pliku
                Console.WriteLine($"Wystąpił błąd podczas importowania danych z pliku CSV: {ex.Message}");
            }
        }
    }
}
