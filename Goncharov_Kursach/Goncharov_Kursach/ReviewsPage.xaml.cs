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
    /// Логика взаимодействия для ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage : Page
    {
        private bool isAddBtnPressed = false;
        public ReviewsPage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Отзывы";
            }
            InitializeComponent();
            dGridReviews.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Reviews in context.Reviews
                        join Client in context.Client on Reviews.client equals Client.id
                        select new
                        {
                            Id = Reviews.id,
                            Клиент = Client.first_name + " " + Client.last_name,
                            Текст = Reviews.text,
                            Оценка = Reviews.rating
                        };

            dGridReviews.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridReviews.UnselectAll();
            dGridReviews.ItemsSource = Entities.GetContext().Reviews.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridReviews.IsReadOnly = false;
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
            var query = from Reviews in context.Reviews
                        join Client in context.Client on Reviews.client equals Client.id
                        select new
                        {
                            Id = Reviews.id,
                            Клиент = Client.first_name + " " + Client.last_name,
                            Текст = Reviews.text
                        };

            dGridReviews.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridReviews.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridReviews.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridReviews.CanUserAddRows = false;
                dGridReviews.UnselectAll();
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
                var a = dGridReviews.SelectedItems.Cast<Reviews>().ToList();
                Entities.GetContext().Reviews.RemoveRange(a);
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
                    var a = dGridReviews.SelectedItems.Cast<Reviews>().ToList();
                    Entities.GetContext().Reviews.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridReviews.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridReviews.CanUserAddRows = true;
                    var a = dGridReviews.SelectedItems.Cast<Reviews>().ToList();
                    Entities.GetContext().Reviews.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridReviews.UnselectAll();
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
