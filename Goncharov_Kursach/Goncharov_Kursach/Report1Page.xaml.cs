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
    /// Логика взаимодействия для Report1Page.xaml
    /// </summary>
    public partial class Report1Page : Page
    {
        public Report1Page()
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
            var query = from Reviews in context.Reviews
                        join Client in context.Client on Reviews.client equals Client.id
                        where Reviews.date >= startDate && Reviews.date <= endDate
                        select new
                        {
                            Оценка = Reviews.rating,
                            Отзыв = Reviews.text,
                            Клиент = Client.first_name + " "+ Client.last_name + " " + Client.middle_name,

                        };

            dGridReviewsForPeriod.ItemsSource = query.ToList();
            
        }
     
    }
    
}
