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
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            timelabel.Content = dateTime.ToLongDateString();
        }
    }
}
