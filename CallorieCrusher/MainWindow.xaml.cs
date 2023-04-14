using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Time.Content = $"{dateTime.ToLongDateString()}    {dateTime.ToShortTimeString()}";
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility=Visibility.Visible;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Hidden;
        }

        private void Eat_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Visible;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Hidden;
        }

        private void Calend_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Visible;
            Page4.Visibility = Visibility.Hidden;
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            Page1.Visibility = Visibility.Hidden;
            Page2.Visibility = Visibility.Hidden;
            Page3.Visibility = Visibility.Hidden;
            Page4.Visibility = Visibility.Visible;
        }
    }
}
