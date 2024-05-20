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

        public static bool isAdmin = true;
        public static bool isLogined = true;

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
            btnReport1.Visibility = Visibility.Visible;
            btnReport1.IsEnabled = true;
            btnReport2.Visibility = Visibility.Visible;
            btnReport2.IsEnabled = true;
            btnReport3.Visibility = Visibility.Visible;
            btnReport3.IsEnabled = true;
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLogined == true) EnableButtons();
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
           MainFrame.NavigationService.Navigate(new Bookings());
        }

        private void btnBooking_status_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Booking_statuses());
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ClientsPage());
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new InventoryPage());
        }

        private void btnRequests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new RequestsPage());
        }

        private void btnReviews_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ReviewsPage());
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new RoomsPage());
        }

        private void btnRoom_type_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new RoomTypePage());
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new StaffPage());
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ServicesPage());
        }

        private void btnService_type_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ServiceTypePage());
        }

        private void btnReport1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Report1Page());
        }

        private void btnReport2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Report2Page());
        }

        private void btnReport3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Report3Page());
        }
    }
}
