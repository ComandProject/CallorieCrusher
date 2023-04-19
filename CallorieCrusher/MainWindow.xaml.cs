using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallorieCrusher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connect = @"Data Source = DESKTOP-JA41I9L; Initial Catalog = CalCrush; Trusted_connection=True";
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
                            object pass = reader.GetValue(2);

                            if (login.Text.ToLower() == nik.ToString().ToLower())
                            {
                                if (PasswordLogina.Password.ToString() != pass.ToString())
                                {
                                    MessageBox.Show("Incorrect Password, try again");
                                }
                                else
                                {
                                    MessageBox.Show("Successfully");
                                    Callorie_Crusher_Main ccm = new Callorie_Crusher_Main();
                                    ccm.Show();
                                    Close();
                                    //reader.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { MessageBox.Show(e.Message); };
            }
        }
    }
}
