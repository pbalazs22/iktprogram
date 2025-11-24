using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ikt2
{
    public partial class MainWindow : Window
    {
        private readonly string filePath = "asd.txt"; // Most már az asd.txt fájlt használjuk

        public MainWindow()
        {
            InitializeComponent();

            // Beolvassuk a fájlt a DataGrid-be
            LoadData();
        }

        private void Elkuldes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Összegyűjtjük az adatokat a TextBoxokból
                string adatSor = $"{NevBox.Text},{SzobaBox.Text},{TipusBox.Text},{ParkoloBox.Text},{FoBox.Text},{EllatasBox.Text},{Datumtol.Text},{Datumig.Text},{KisallatBox.Text}";

                // Hozzáírjuk a fájlhoz
                File.AppendAllText(filePath, adatSor + Environment.NewLine);

                MessageBox.Show("Az adatok sikeresen elmentve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);

                // TextBoxok ürítése
                NevBox.Clear();
                SzobaBox.Clear();
                FoBox.Clear();
                EllatasBox.Clear();

                // Adatok frissítése a DataGrid-ben
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt mentés közben:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                var foglalasok = new List<Foglalas>();

                // Ellenőrizzük, hogy létezik-e az asd.txt fájl
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("A fájl nem található.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);

                // Az első sor (fejléc) átugrása
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');

                    // Ellenőrizzük, hogy a sor elég hosszú-e
                    if (values.Length >= 9)
                    {
                        var foglalas = new Foglalas
                        {
                            GuestName = values[0],
                            RoomNumber = values[1],
                            RoomType = values[2],
                            NeedsParking = values[3],
                            GuestsSummary = values[4],
                            MealPlan = values[5],
                            FromDate = values[6],
                            ToDate = values[7],
                            HasPet = values[8]
                        };

                        foglalasok.Add(foglalas);
                    }
                }

                // A DataGrid adatforrása
                FogAdatok.ItemsSource = foglalasok;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a fájl beolvasásakor:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NevBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NevBox.Clear();
        }

        private void SzobaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SzobaBox.Clear();
        }

        private void FoBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FoBox.Clear();
        }

        private void EllatasBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EllatasBox.Clear();
        }
    }

    public class Foglalas
    {
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string NeedsParking { get; set; }
        public string GuestsSummary { get; set; }
        public string MealPlan { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string HasPet { get; set; }
    }
}
