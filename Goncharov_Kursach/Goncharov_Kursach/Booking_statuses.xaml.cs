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
    /// Логика взаимодействия для Booking_statuses.xaml
    /// </summary>
    public partial class Booking_statuses : Page
    {
        private bool isAddBtnPressed = false;

        public Booking_statuses()
        {
            InitializeComponent();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Статусы";
            }

            dgridBooking_statuses.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Booking_status in context.Booking_status
                        select new
                        {
                            Id = Booking_status.id,
                            Status = Booking_status.status

                        };

            dgridBooking_statuses.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dgridBooking_statuses.ItemsSource = Entities.GetContext().Booking_status.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dgridBooking_statuses.IsReadOnly = false;
            btnDelete.Visibility = Visibility.Visible;
            btnDelete.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnSave.IsEnabled = true;
            btnAdd.Visibility = Visibility.Visible;
            btnAdd.IsEnabled = true;

        }

        private void btnReturnFromEdit_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();
            var query = from Booking_status in context.Booking_status
                        select new
                        {
                            Id = Booking_status.id,
                            Status = Booking_status.status
                        };

            dgridBooking_statuses.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dgridBooking_statuses.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dgridBooking_statuses.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dgridBooking_statuses.CanUserAddRows = false;
                dgridBooking_statuses.UnselectAll();
                Entities.GetContext().SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var a = dgridBooking_statuses.SelectedItems.Cast<Booking_status>().ToList();
                Entities.GetContext().Booking_status.RemoveRange(a);
                Entities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (isAddBtnPressed)
                {
                    var a = dgridBooking_statuses.SelectedItems.Cast<Booking_status>().ToList();
                    Entities.GetContext().Booking_status.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dgridBooking_statuses.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dgridBooking_statuses.CanUserAddRows = true;
                    var a = dgridBooking_statuses.SelectedItems.Cast<Booking_status>().ToList();
                    Entities.GetContext().Booking_status.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dgridBooking_statuses.UnselectAll();
                    isAddBtnPressed = !isAddBtnPressed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
