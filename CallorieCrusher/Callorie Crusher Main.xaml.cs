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
        string eda = "Торт";
        int ProcBilok = 0;
        int ProcZhirok = 0;
        int ProcUglevodi = 0;
        int ProcWat = 0;
        string username = "";
        public Callorie_Crusher_Main(string user)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;
            timer.Start();
            filling();
            ResultsTable();
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(0.5);
            timer1.Tick += ResultsTable1;
            timer1.Start();
            username = user;
            ResultLabels();
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
            string connectionString = @"Data Source = USER-PC50; Initial Catalog = CalCrush; Trusted_Connection=True";
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
        void ResultsTable()
        {

            string connectionString = @"Data Source = USER-PC50; Initial Catalog = CalCrush; Trusted_Connection=True";
            string catall = "SELECT * FROM Food";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(catall, connection);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        object nik = reader.GetValue(1);
                        if (eda.ToLower() == nik.ToString().ToLower())
                        {
                            BilokRes += Convert.ToDouble(reader.GetValue(5));
                            ZhirokRes += Convert.ToDouble(reader.GetValue(6));
                            UglevodRes += Convert.ToDouble(reader.GetValue(7));
                            WaterRes += Convert.ToDouble(reader.GetValue(8));
                            ProcBilok = Convert.ToInt32((BilokRes * 100) / 52);
                            ProcZhirok = Convert.ToInt32((ZhirokRes * 100) / 55);
                            ProcUglevodi = Convert.ToInt32((UglevodRes * 100) / 97);
                            ProcWat = Convert.ToInt32((WaterRes * 100) / 2.4);
                            nProtein.Content = $"{ProcBilok} %";
                            nFat.Content = $"{ProcZhirok} %";
                            nCarb.Content = $"{ProcUglevodi} %";
                            nWat.Content = $"{ProcWat} %";
                            eda = "";
                        }
                    }
                    reader.Close();
                }
            }
        }
        void ResultLabels()
        {
            float maxprot = 0;
            float maxfat = 0;
            float maxcarb = 0;
            float maxwater = 2.5F;
            string sqlExpression = "SELECT * FROM Registr";
            string connect = @"Data Source = USER-PC50; Initial Catalog = CalCrush; Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            object nik = reader.GetValue(1);
                            object lovelyweight = reader.GetValue(7);
                            if(nik.ToString() == username)
                            {                             
                                maxprot = float.Parse(lovelyweight.ToString()) * 1.5F;
                                maxfat = float.Parse(lovelyweight.ToString()) * 0.8F;
                                maxcarb = float.Parse(lovelyweight.ToString()) * 2;
                            }
                        }
                    }
                }
                catch (Exception ex) { }
            }
            protlbl.Content = $"{Convert.ToInt32(ProcBilok / 100.0 * maxprot)}g/{maxprot}g";
            fatlbl.Content = $"{Convert.ToInt32(ProcZhirok / 100.0 * maxfat)}g/{maxfat}g";
            carblbl.Content = $"{Convert.ToInt32(ProcUglevodi / 100.0 * maxcarb)}g/{maxcarb}g";
            waterlbl.Content = $"{Convert.ToInt32(ProcWat / 100.0 * maxwater)}L/{maxwater}L";
        }
        void ResultsTable1(object sender, EventArgs e)
        {
            if (TableProt.Height <= Convert.ToInt32((529 * ProcBilok) / 100))
            {
                TableProt.Height += 1;
                if (ProcBilok >= 0 && ProcBilok <= 33 || ProcBilok > 100)
                {
                    TableProt.Background = new SolidColorBrush(Colors.Red);
                }
                else if (ProcBilok >= 33 && ProcBilok <= 66)
                {
                    TableProt.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (ProcBilok >= 66 && ProcBilok <= 100)
                {
                    TableProt.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableFat.Height <= Convert.ToInt32((529 * ProcZhirok) / 100))
            {
                TableFat.Height += 1;
                if (ProcZhirok >= 0 && ProcZhirok <= 33 || ProcZhirok > 100)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Red);
                }
                else if (ProcZhirok >= 33 && ProcZhirok <= 66)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (ProcZhirok >= 66 && ProcZhirok <= 100)
                {
                    TableFat.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableCarb.Height <= Convert.ToInt32((529 * ProcUglevodi) / 100))
            {
                TableCarb.Height += 1;
                if (ProcUglevodi >= 0 && ProcUglevodi <= 33 || ProcUglevodi > 100)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Red);
                }
                else if (ProcUglevodi >= 33 && ProcUglevodi <= 66)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (ProcUglevodi >= 66 && ProcUglevodi <= 100)
                {
                    TableCarb.Background = new SolidColorBrush(Colors.Green);
                }
            }
            if (TableWat.Height <= Convert.ToInt32((529 * ProcWat) / 100))
            {
                TableWat.Height += 1;
            }

            ResultLabels();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DBFood dBFood = new DBFood();
            dBFood.Show();
        }
    }

}
