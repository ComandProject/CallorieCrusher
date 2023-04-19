using Microsoft.Win32;
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
    /// Логика взаимодействия для DBFood.xaml
    /// </summary>
    public partial class DBFood : Window
    {
        public DBFood()
        {
            InitializeComponent();
        }
        string path = "";
        private void addpicbutton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png)|*.png|(*.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() != true)
                return;
            path = openFileDialog.FileName;
            MessageBox.Show(path);
            string picname = "";
            foreach (char c in path)
            {
                picname += c;
                if (c == '\\')
                    picname = "";
            }
            picpath.Content = picname;
        }

        private async void createclick(object sender, RoutedEventArgs e)
        {
            #region checks
            if (nametxt.Text == "")
            {
                MessageBox.Show("Enter name of new dish!","Error",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }
            if (desctxt.Text == "")
            {
                MessageBox.Show("Enter some info about new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (prottxt.Text == "")
            {
                MessageBox.Show("Enter count of proteins(g) in new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (fattxt.Text == "")
            {
                MessageBox.Show("Enter count of fats(g) in new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (carbtxt.Text == "")
            {
                MessageBox.Show("Enter count of carbohydrates(g) in new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (watertxt.Text == "")
            {
                MessageBox.Show("Enter amount of water(ml) in new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ccaltxt.Text == "")
            {
                MessageBox.Show("Enter count of callories in new dish!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int f = 0;
            foreach(char c in prottxt.Text)
            {
                if(!Char.IsDigit(c))
                {
                    if (c == ',')
                    {
                        f++;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect count of proteins!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        prottxt.Text = "";
                        return;
                    }
                    if (f > 1)
                    {
                        MessageBox.Show("Incorrect count of proteins!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        prottxt.Text = "";
                        return;
                    }
                }
            }
            f = 0;
            foreach (char c in fattxt.Text)
            {
                if (!Char.IsDigit(c))
                {
                    if (c == ',')
                    {
                        f++;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect count of fats!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        fattxt.Text = "";
                        return;
                    }
                    if (f > 1)
                    {
                        MessageBox.Show("Incorrect count of fats!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        fattxt.Text = "";
                        return;
                    }
                }
            }
            f = 0;
            foreach (char c in carbtxt.Text)
            {
                if (!Char.IsDigit(c))
                {
                    if (c == ',')
                    {
                        f++;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect count of carbohydrates!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        carbtxt.Text = "";
                        return;
                    }
                    if (f > 1)
                    {
                        MessageBox.Show("Incorrect count of carbohydrates!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        carbtxt.Text = "";
                        return;
                    }
                }
            }
            f = 0;
            foreach (char c in watertxt.Text)
            {
                if (!Char.IsDigit(c))
                {
                    if (c == ',')
                    {
                        f++;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect count of water!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        watertxt.Text = "";
                        return;
                    }
                    if (f > 1)
                    {
                        MessageBox.Show("Incorrect count of water!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        watertxt.Text = "";
                        return;
                    }
                }
            }
            f = 0;
            foreach (char c in ccaltxt.Text)
            {
                if (!Char.IsDigit(c))
                {
                    if (c == ',')
                    {
                        f++;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect count of callories!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        ccaltxt.Text = "";
                        return;
                    }
                    if (f > 1)
                    {
                        MessageBox.Show("Incorrect count of callories!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        ccaltxt.Text = "";
                        return;
                    }
                }
            }
            #endregion
            if(picpath.Content == "")
            {
                MessageBoxResult res = MessageBox.Show("Want to create a dish without picture?","Wait",MessageBoxButton.YesNo,MessageBoxImage.Information);
                if (res == MessageBoxResult.No)
                    return;
            }
            string connectionString = @"Data Source = DESKTOP-JA41I9L; Initial Catalog = CalCrush; Trusted_Connection=True";
            string add = $"INSERT INTO Food(names,about,bilok,zhirok,uglevodi,cal,water) VALUES('{nametxt}','{desctxt}',{prottxt},{fattxt},{carbtxt},{ccaltxt},{watertxt})";
            string add2 = $"UPDATE PictureProduct SET Picture =      (SELECT * FROM OPENROWSET(BULK N'{path}', SINGLE_BLOB) AS image)WHERE names = {nametxt}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(add, connection);
                int num = await command.ExecuteNonQueryAsync();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(add2, connection);
                int num = await command.ExecuteNonQueryAsync();
            }
        }

        private void cancelclick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
