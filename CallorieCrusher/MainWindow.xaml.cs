using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            Callorie_Crusher_Main ccm = new Callorie_Crusher_Main();
            ccm.Show();

        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            SingIn signIp = new SingIn();
            signIp.Show();
        }

        private void PasswordLogina_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            string password = passwordBox.Password;

            // Создаем текстовый блок для отображения символа бургера 🍔
            TextBlock textBlock = new TextBlock
            {
                Text = "🍔",
                FontSize = passwordBox.FontSize,
                Foreground = passwordBox.Foreground,
                Background = passwordBox.Background,
                Margin = passwordBox.Margin,
                Padding = passwordBox.Padding,
                HorizontalAlignment = passwordBox.HorizontalAlignment,
                VerticalAlignment = passwordBox.VerticalAlignment,
                Width = passwordBox.Width,
                Height = passwordBox.Height,
                Visibility = password.Length == 0 ? Visibility.Visible : Visibility.Hidden // Определяем видимость символа бургера 🍔 в зависимости от наличия пароля
            };

            // Заменяем содержимое PasswordBox на текстовый блок
           
            //PasswordBox PasswordLogina = (PasswordBox)sender;
            //string password = PasswordLogina.Password;

            //// Создаем текстовый блок для отображения символа бургера 🍔
            //TextBlock textBlock = new TextBlock
            //{
            //    Text = "🍔",
            //    FontSize = PasswordLogina.FontSize,
            //    Foreground = PasswordLogina.Foreground,
            //    Background = PasswordLogina.Background,
            //    Margin = PasswordLogina.Margin,
            //    Padding = PasswordLogina.Padding,
            //    HorizontalAlignment = PasswordLogina.HorizontalAlignment,
            //    VerticalAlignment = PasswordLogina.VerticalAlignment,
            //    Width = PasswordLogina.Width,
            //    Height = PasswordLogina.Height,
            //    Visibility = password.Length == 0 ? Visibility.Visible : Visibility.Hidden // Определяем видимость символа бургера 🍔 в зависимости от наличия пароля
            //};

            //// Заменяем содержимое PasswordBox на текстовый блок
            //this.PasswordLogina.Content = textBlock;
        }
    }
}
