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

namespace AppLocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //String sDate = myCalendar.SelectedDate.ToString();
            String sDate = DateTime.Now.ToString();
            textBlock.Text = sDate;
            Console.WriteLine(sDate);

            //button.Content = "YEET";
        }

        private void lockButton_Click(object sender, RoutedEventArgs e)
        {
            App.LockApp();
        }

        private void releaseButton_Click(object sender, RoutedEventArgs e)
        {
            App.ReleaseApp();
        }
    }
}
