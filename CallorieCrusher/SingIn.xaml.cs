using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для SingIn.xaml
    /// </summary>
    public partial class SingIn : Window
    {
        private string connect = @"Data Source = USER-PC50; Initial Catalog = CalCrush; Trusted_Connection=True";
        string sqlExpression = "SELECT * FROM Registr";
        private string str = "";
        public SingIn()
        {
            InitializeComponent();
            str = "INSERT INTO Registr(Logins, Passwords, Weights, Grow, Lose, Stay, DesiredWeight) ";
        }

        private void RegiButton_Click(object sender, RoutedEventArgs e)
        {
            if (LogInBox.Text == "" || PassBox.Password == "" || WeightBox.Text == "" || desiredweightBox.Text == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }
            else
            {
                ProverkaRegi();
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        private void Registr()
        {
            byte[] hashedPassword = HashPassword(PassBox.Password);
            string hashedPasswordString = Convert.ToBase64String(hashedPassword);

            using (SqlConnection connection = new SqlConnection(connect))
            {
                string str2 = str + "VALUES('" + LogInBox.Text + "', '" + hashedPasswordString + "', " + WeightBox.Text + ", '" + growRadio.IsChecked.Value.ToString() + "', '" + loseRadio.IsChecked.Value.ToString() + "', '" + stayRadio.IsChecked.Value.ToString() + "', " + desiredweightBox.Text + ")";
                connection.Open();
                SqlCommand command = new SqlCommand(str2, connection);
                int num = command.ExecuteNonQuery();

                Close();
            }
        }

        private byte[] HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(passwordBytes);
                return hash;
            }
        }
        private void ProverkaRegi()
        {
            if (LogInBox.Text == "" || PassBox.ToString() == "" || WeightBox.Text == "" || desiredweightBox.Text == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }

            int kolProverka = 0;
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
                            if (LogInBox.Text.ToLower() == nik.ToString().ToLower())
                            {
                                kolProverka++;
                                MessageBox.Show("Этот ник занят. Попробуйте другой.");
                                return;
                            }
                        }

                        reader.Close();
                    }

                    // Generate salt and hash password
                    byte[] salt = GenerateSalt();
                    byte[] hashedPassword = HashPassword(PassBox.Password.ToString());

                    MessageBox.Show("Successfully");
                    Registr();

                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
