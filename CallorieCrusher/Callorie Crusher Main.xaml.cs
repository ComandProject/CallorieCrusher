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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CallorieCrusher
{
    /// <summary>
    /// Логика взаимодействия для Callorie_Crusher_Main.xaml
    /// </summary>
    public partial class Callorie_Crusher_Main : Window
    {
        public Callorie_Crusher_Main()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;
            timer.Start();
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
    }

}
