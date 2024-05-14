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

namespace Goncharov_Kursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool isAdmin = false;
        public static bool isLogined = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        public void EnableButtons()
        {

            btnBooking.Visibility = Visibility.Visible;
            btnBooking.IsEnabled = true;
            btnBooking_status.Visibility = Visibility.Visible;
            btnBooking_status.IsEnabled = true;
            btnClient.Visibility = Visibility.Visible;
            btnClient.IsEnabled = true;
            btnInventory.Visibility = Visibility.Visible;
            btnInventory.IsEnabled = true;
            btnRequests.Visibility = Visibility.Visible;
            btnRequests.IsEnabled = true;
            btnReviews.Visibility = Visibility.Visible;
            btnReviews.IsEnabled = true;
            btnRoom.Visibility = Visibility.Visible;
            btnRoom.IsEnabled = true;
            btnRoom_type.Visibility = Visibility.Visible;
            btnRoom_type.IsEnabled = true;
            btnService.Visibility = Visibility.Visible;
            btnService.IsEnabled = true;
            btnService_type.Visibility = Visibility.Visible;
            btnService_type.IsEnabled = true;
            btnStaff.Visibility = Visibility.Visible;
            btnStaff.IsEnabled = true;
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
           MainFrame.NavigationService.Navigate(new Bookings());
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
                if (isLogined == true) EnableButtons();      
        }
    }
}
