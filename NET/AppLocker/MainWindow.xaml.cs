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
            if (App.CheckSteamExist())
                steamStatus.Text = "Steam exists!";
            else
                steamStatus.Text = "Steam locked!";
        }

        private void lockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckSteamExist())
                return;
            App.LockApp();
            System.Environment.Exit(0);
        }

        private void releaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckSteamExist())
                return;
            App.ReleaseApp();
            System.Environment.Exit(0);
        }
    }
}
