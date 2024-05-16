using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для Bookings.xaml
    /// </summary>
    public partial class Bookings : Page
    {
        private bool isAddBtnPressed = false;
        public Bookings()
        {
            InitializeComponent();
            dGridBookings.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Booking in context.Booking
                        join Staff in context.Staff on Booking.responsible equals Staff.id
                        join Client in context.Client on Booking.client equals Client.id
                        join Room in context.Room on Booking.room equals Room.id
                        join Booking_status in context.Booking_status on Booking.status equals Booking_status.id
                        select new
                        {
                            Id = Booking.id,
                            Ответственный = Staff.last_name + " " + Staff.first_name,
                            Клиент = Client.middle_name + " " + Client.first_name + " " + Client.last_name,
                            Номер = Room.id,
                            Статус = Booking_status.status,
                            ДатаБронирования = Booking.date_of_booking,
                            ДатаЗаезда = Booking.checkin_date,
                            ДатаВыезда = Booking.leaving_date,
                            ДопТребования = Booking.add_requirements

                        };

            dGridBookings.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridBookings.UnselectAll();
            dGridBookings.ItemsSource = Entities.GetContext().Booking.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridBookings.IsReadOnly = false;
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
            var query = from Booking in context.Booking
                        join Staff in context.Staff on Booking.responsible equals Staff.id
                        join Client in context.Client on Booking.client equals Client.id
                        join Room in context.Room on Booking.room equals Room.id
                        join Booking_status in context.Booking_status on Booking.status equals Booking_status.id
                        select new
                        {
                            Id = Booking.id,
                            Ответственный = Staff.last_name + " " + Staff.first_name,
                            Клиент = Client.middle_name + " " + Client.first_name + " " + Client.last_name,
                            Номер = Room.id,
                            Статус = Booking_status.status,
                            ДатаБронирования = Booking.date_of_booking,
                            ДатаЗаезда = Booking.checkin_date,
                            ДатаВыезда = Booking.leaving_date,
                            ДопТребования = Booking.add_requirements

                        };

            dGridBookings.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridBookings.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridBookings.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridBookings.CanUserAddRows = false;
                dGridBookings.UnselectAll();
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
                var a = dGridBookings.SelectedItems.Cast<Booking>().ToList();
                Entities.GetContext().Booking.RemoveRange(a);
                Entities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            try {
                if (isAddBtnPressed)
                {
                    var a = dGridBookings.SelectedItems.Cast<Booking>().ToList();
                    Entities.GetContext().Booking.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridBookings.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridBookings.CanUserAddRows = true;
                    var a = dGridBookings.SelectedItems.Cast<Booking>().ToList();
                    Entities.GetContext().Booking.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridBookings.UnselectAll();
                    isAddBtnPressed = !isAddBtnPressed;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
