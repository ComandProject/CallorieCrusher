using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace CallorieCrusher
{
    /// <summary>
    /// Interaction logic for WindowAddFood.xaml
    /// </summary>
    public partial class WindowAddFood : Window
    {
        public WindowAddFood()
        {
            InitializeComponent();
        }
        int selected = 0;
        private void Combo1select(object sender, SelectionChangedEventArgs e)
        {
            if (combo1.SelectedItem.ToString().Contains("First")) { selected = 1; }
            else if (combo1.SelectedItem.ToString().Contains("Second")){ selected = 2; }
            else if (combo1.SelectedItem.ToString().Contains("Drinks")){ selected = 3; }
            else if (combo1.SelectedItem.ToString().Contains("Dessert")){ selected = 4; }
            combo2.IsEnabled = true;
            fillingcombo2();
        }
        private async void fillingcombo2()
        {
            combo2.Items.Clear();
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
                        object name_ = reader.GetValue(1);
                        object first_ = reader.GetValue(9);
                        object second_ = reader.GetValue(10);
                        object drink_ = reader.GetValue(12);
                        object dessert_ = reader.GetValue(11);
                        if (selected == 1 && Convert.ToBoolean(first_))
                        {                            
                                combo2.Items.Add(name_.ToString());                            
                        }else if(selected == 2 && Convert.ToBoolean(second_))
                        {
                            combo2.Items.Add(name_.ToString());
                        }
                        else if (selected == 3 && Convert.ToBoolean(drink_))
                        {
                            combo2.Items.Add(name_.ToString());
                        }
                        else if (selected == 4 && Convert.ToBoolean(dessert_))
                        {
                            combo2.Items.Add(name_.ToString());
                        }
                    }
                    reader.Close();
                }
            }
        }
        private async void combo2selected(object sender, SelectionChangedEventArgs e)
        {
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
                        object name_ = reader.GetValue(1);
                        object about_ = reader.GetValue(3);
                        object prots_ = reader.GetValue(4);
                        object fats_ = reader.GetValue(5);
                        object carbs_ = reader.GetValue(6);
                        object water_ = reader.GetValue(7);
                        object ccal_ = reader.GetValue(8);
                        desc.Text = about_.ToString();
                        prot = float.Parse(prots_.ToString());
                        fat = float.Parse(fats_.ToString());
                        carb = float.Parse(carbs_.ToString());
                        water = float.Parse(water_.ToString());
                    }
                    reader.Close();
                    
                }
            }
        }
        float prot = 0;
        float fat = 0;
        float carb = 0;
        float water = 0;
        private void weightchanged(object sender, TextChangedEventArgs e)
        {
            if (weightlbl.Text == "")
                return;
            foreach (char c in weightlbl.Text)
            {
                if (!Char.IsDigit(c))
                {
                    protlbl.Content = "Wrong weight!";
                    fatlbl.Content = "";
                    carblbl.Content = "";
                    waterlbl.Content = "";
                    return;
                }
            }
            protlbl.Content = $"Prots: {prot * float.Parse(weightlbl.Text.ToString()) / 100.0}g";
            fatlbl.Content = $"Fats: {fat * float.Parse(weightlbl.Text.ToString()) / 100.0}g";
            carblbl.Content = $"Carbs: {carb * float.Parse(weightlbl.Text.ToString()) / 100.0}g";
            waterlbl.Content = $"Water: {water * float.Parse(weightlbl.Text.ToString()) / 100.0}ml";
        }
        private void cancelclick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (fatlbl.Content == "")
                return;
            if (timecombo.SelectedIndex == -1)
                return;
            MessageBox.Show("add");
            string time = "";
            foreach(char c in timecombo.SelectedItem.ToString())
            {
                time+=c;
                if (c == ' ')
                    time = "";
            }
            Cjntainer.name = combo2.SelectedItem.ToString();
            Cjntainer.time = time;
            Cjntainer.weight = weightlbl.Text;
            Close();
        }
    }
}
