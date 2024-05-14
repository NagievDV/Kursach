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
            AlterButtonsState();
        }
        public void AlterButtonsState()
        {
            if (btnBooking.IsEnabled == true)
            {
                btnBooking.IsEnabled = false;
                btnBooking_status.IsEnabled = false;
                btnClient.IsEnabled = false;
                btnInventory.IsEnabled = false;
                btnRequests.IsEnabled = false;
                btnReviews.IsEnabled = false;
                btnRoom.IsEnabled = false;
                btnRoom_type.IsEnabled = false;
                btnService.IsEnabled = false;
                btnService_type.IsEnabled = false;
                btnStaff.IsEnabled = false;
            }
            else
            {
                btnBooking.IsEnabled = true;
                btnBooking_status.IsEnabled = true;
                btnClient.IsEnabled = true;
                btnInventory.IsEnabled = true;
                btnRequests.IsEnabled = true;
                btnReviews.IsEnabled = true;
                btnRoom.IsEnabled = true;
                btnRoom_type.IsEnabled = true;
                btnService.IsEnabled = true;
                btnService_type.IsEnabled = true;
                btnStaff.IsEnabled = true;
            }
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
           MainFrame.NavigationService.Navigate(new Bookings());
        }

    }
}
