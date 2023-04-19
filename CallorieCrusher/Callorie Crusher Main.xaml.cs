using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
using System.Windows.Threading;

namespace CallorieCrusher
{
    /// <summary>
    /// Логика взаимодействия для Callorie_Crusher_Main.xaml
    /// </summary>
    public partial class Callorie_Crusher_Main : Window
    {
        double ResAll = 0;
        double BilokRes = 0;
        double ZhirokRes = 0;
        double UglevodRes = 0;
        double WaterRes = 0;
        public Callorie_Crusher_Main()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;
            timer.Start();
            filling();
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(0.5);
            timer1.Tick += ResultsTable;
            timer1.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            timelabel.Content = $"{dateTime.ToLongDateString()}  {dateTime.ToShortTimeString()}";
        }
        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Clear();
        }
        private void osnova_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Visible;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Hidden;
        }

        private void food_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Visible;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Hidden;
        }

        private void calendar_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Visible;
            Page4.Visibility = Visibility.Hidden;

        }

        private void stats_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Visible;
        }

        private void addFood_Click(object sender, RoutedEventArgs e)
        {
            WindowAddFood windowAddFood = new WindowAddFood();
            windowAddFood.ShowDialog();
        }
    

    public class FoodClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public BitmapImage picture { get; set; }
        public string about { get; set; }
        public float bilok { get; set; }
        public float zhirok { get; set; }
        public float uglevodi { get; set; }
        public float cal { get; set; }
        public float water { get; set; } 
    }

        public ObservableCollection<FoodClass> Foods { get; set; }
        private async void filling()
        {
            Foods = new ObservableCollection<FoodClass>();
            string connectionString = @"Data Source = DESKTOP-JA41I9L; Initial Catalog = CalCrush; Trusted_Connection=True";
            string catall = "SELECT * FROM Food";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(catall, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();


                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        object id_ = reader.GetValue(0);
                        object name_ = reader.GetValue(1);
                        byte[] Images = (byte[])reader.GetValue(2);
                        Stream StreamObj = new MemoryStream(Images);
                        BitmapImage BitObj = new BitmapImage();
                        BitObj.BeginInit();
                        BitObj.StreamSource = StreamObj;
                        BitObj.EndInit();
                        
                        object about_ = reader.GetValue(3);
                        object bilok_ = reader.GetValue(4);
                        object zhirok_ = reader.GetValue(5);
                        object uglevodi_ = reader.GetValue(6);
                        object cal_ = reader.GetValue(7);
                        object water_ = reader.GetValue(8);
                        Foods.Add(new FoodClass() { id = Convert.ToInt32(id_), name = name_.ToString(), picture = BitObj, about = about_.ToString(), bilok = float.Parse(bilok_.ToString(), CultureInfo.InvariantCulture.NumberFormat), zhirok = float.Parse(zhirok_.ToString(), CultureInfo.InvariantCulture.NumberFormat), uglevodi = float.Parse(uglevodi_.ToString(), CultureInfo.InvariantCulture.NumberFormat), cal = float.Parse(cal_.ToString(), CultureInfo.InvariantCulture.NumberFormat), water = float.Parse(water_.ToString(), CultureInfo.InvariantCulture.NumberFormat) });

                    }
                    reader.Close();
                }
                FoodList.ItemsSource = Foods;
            }

        }
        void ResultsTable(object sender, EventArgs e)
        {

            //string connect = @"Data Source = HOME-PC; Initial Catalog = CalCrush; Trusted_connection=True";
            //string sqlExpression = "SELECT * FROM Registr";
            //WindowAddFood addFood = new WindowAddFood();
            //using (SqlConnection connection = new SqlConnection(connect))
            //{

            //    try
            //    {
            //        connection.Open();
            //        SqlCommand command = new SqlCommand(sqlExpression, connection);
            //        SqlDataReader reader = command.ExecuteReader();

            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                object nik = reader.GetValue(1);
            //                if (addFood.NameEda.Text.ToString() != "")
            //                {
            //                    if (addFood.NameEda.Text.ToLower() == nik.ToString().ToLower())
            //                    {
            //                        BilokRes += Convert.ToDouble(reader.GetValue(5));
            //                        ZhirokRes += Convert.ToDouble(reader.GetValue(6));
            //                        UglevodRes += Convert.ToDouble(reader.GetValue(7));
            //                        WaterRes += Convert.ToDouble(reader.GetValue(9));
            //                        nProtein.Content = $"{(BilokRes * 100) / 52}";
            //                        nFat.Content = $"{(BilokRes * 100) / 55}";
            //                        nCarb.Content = $"{(BilokRes * 100) / 97}";
            //                        nWat.Content = $"{(WaterRes * 100) /2.4}";
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception i) { MessageBox.Show(i.Message); };
            //}


            //ResAll = BilokRes + ZhirokRes + UglevodRes + WaterRes;
            //nProtein.Content = $"{Convert.ToInt32((BilokRes * 100) / ResAll)} %";

            if (TableProt.Height <= Convert.ToInt32((529 * 90) / 100))
            {
                TableProt.Height += 1;
                if (TableProt.Height>=0&& TableProt.Height<=176)
                {
                    TableProt.Background= new SolidColorBrush(Colors.Red);
                }
                else if (TableProt.Height >= 176 && TableProt.Height <= 352)
                {
                    TableProt.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (TableProt.Height >= 352 && TableProt.Height <= 529)
                {
                    TableProt.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableFat.Height <= Convert.ToInt32((529 * 30) / 100))
            {
                TableFat.Height += 1;
                if (TableFat.Height >= 0 && TableFat.Height <= 176)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Red);
                }
                else if (TableFat.Height >= 176 && TableFat.Height <= 352)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (TableFat.Height >= 352 && TableFat.Height <= 529)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableCarb.Height <= Convert.ToInt32((529 * 10) / 100))
            {
                TableCarb.Height += 1;
                if (TableCarb.Height >= 0 && TableCarb.Height <= 176)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Red);
                }
                else if (TableCarb.Height >= 176 && TableCarb.Height <= 352)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (TableCarb.Height >= 352 && TableCarb.Height <= 529)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableWat.Height <= Convert.ToInt32((529 * 60) / 100))
            {
                TableWat.Height += 1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DBFood dBFood = new DBFood();
            dBFood.Show();
        }
    }

}
