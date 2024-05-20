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
    /// Логика взаимодействия для Report3Page.xaml
    /// </summary>
    public partial class Report3Page : Page
    {
        public Report3Page()
        {
            InitializeComponent();
        }
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParse(tbDate1.Text, out startDate))
            {
                MessageBox.Show("Введите корректную дату начала.");
                return;
            }

            if (!DateTime.TryParse(tbDate2.Text, out endDate))
            {
                MessageBox.Show("Введите корректную дату окончания.");
                return;
            }
            LoadData(startDate, endDate);
        }
        private void LoadData(DateTime startDate, DateTime endDate)
        {
            var context = Entities.GetContext();
            var query = from Booking in context.Booking
                        join Client in context.Client on Booking.client equals Client.id
                        join Room in context.Room on Booking.room equals Room.id
                        join Status in context.Booking_status on Booking.status equals Status.id
                        where Booking.date_of_booking >= startDate && Booking.date_of_booking <= endDate
                        select new
                        {
                            Клиент = Client.first_name + " " + Client.last_name + " " + Client.middle_name,
                            Комната = Room.id,
                            Статус = Status.status,
                            ДатаБрони = Booking.date_of_booking

                        };

            dGridBookingsForPeriod.ItemsSource = query.ToList();

        }
    }
}
