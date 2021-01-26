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
                steamStatus.Text = "Steam found in \"" + App.GetSteamPath() + "\"";
            else
                steamStatus.Text = "Steam locked!";
            if (App.CheckR6Exist())
                r6Status.Text = "R6S found in \""+ App.GetR6Path() + "\"";
            else
                r6Status.Text = "R6S locked!";
        }

        private void steamLockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckSteamExist())
                return;
            App.LockSteam();
            System.Environment.Exit(0);
        }

        private void steamReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckSteamExist())
                return;
            App.ReleaseSteam();
            System.Environment.Exit(0);
        }

        private void r6LockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckR6Exist())
                return;
            App.LockR6();
            System.Environment.Exit(0);
        }

        private void r6ReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckR6Exist())
                return;
            App.ReleaseR6();
            System.Environment.Exit(0);
        }
    }
}
