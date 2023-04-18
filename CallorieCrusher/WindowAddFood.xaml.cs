using MahApps.Metro.Controls;
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

        private void TimePicker_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = (sender as TimePicker)?.Template?.FindName("PART_TextBox", sender as TimePicker) as TextBox;
            if (textBox != null && !textBox.IsReadOnly)
            {
                e.Handled = true;
            }
        }
    }
}
