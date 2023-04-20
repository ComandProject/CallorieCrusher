using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallorieCrusher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connect = @"Data Source = USER-PC50; Initial Catalog = CalCrush; Trusted_Connection=True";
        string sqlExpression = "SELECT * FROM Registr";
        public MainWindow()
        {
            InitializeComponent();            
            
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            SingIn signIp = new SingIn();
            signIp.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ProverkaAutorization();
        }
        private void ProverkaAutorization()
        {
            if (login.Text == "" || PasswordLogina.Password.ToString() == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }

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
                            object passHash = reader.GetValue(2);

                            if (login.Text.ToLower() == nik.ToString().ToLower())
                            {
                                string password = PasswordLogina.Password.ToString().ToLower();
                                byte[] hashedPassword = HashPassword(password);
                                byte[] storedPassword = Convert.FromBase64String(passHash.ToString()); // преобразование строки в массив байт

                                if (hashedPassword.SequenceEqual(storedPassword))
                                {
                                    MessageBox.Show("Successfully");
                                    Callorie_Crusher_Main ccm = new Callorie_Crusher_Main(nik.ToString());
                                    ccm.Show();
                                    Close();
                                    return;
                                }
                            }
                        }
                    }
                    MessageBox.Show("Incorrect Login or Password, try again");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
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
    }
}
