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
    public partial class RequestsPage : Page
    {
            private bool isAddBtnPressed = false;
            public RequestsPage()
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.lblText.Content = "Запросы";
                }
                InitializeComponent();
                dGridRequests.CanUserAddRows = false;
                var context = Entities.GetContext();
                var query = from Requests in context.Requests
                            join Client in context.Client on Requests.client equals Client.id
                            select new
                            {
                                Id = Requests.id,
                                Клиент = Client.first_name + " " + Client.last_name,
                                Запрос = Requests.text,
                                ДатаЗапроса = Requests.date_of_request

                            };

                dGridRequests.ItemsSource = query.ToList();

                if (MainWindow.isAdmin == true)
                {
                    btnEdit.Visibility = Visibility.Visible;
                }

            }


            private void btnEdit_Click(object sender, RoutedEventArgs e)
            {
                dGridRequests.UnselectAll();
                dGridRequests.ItemsSource = Entities.GetContext().Requests.ToList();
                btnEdit.Visibility = Visibility.Hidden;
                btnReturnFromEdit.Visibility = Visibility.Visible;
                dGridRequests.IsReadOnly = false;
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
                var query = from Requests in context.Requests
                            join Client in context.Client on Requests.client equals Client.id
                            select new
                            {
                                Id = Requests.id,
                                Клиент = Client.id,
                                Запрос = Requests.text,
                                ДатаЗапроса = Requests.date_of_request

                            };

                dGridRequests.ItemsSource = query.ToList();


                btnEdit.Content = "Редактировать";
                btnDelete.Visibility = Visibility.Hidden;
                btnDelete.IsEnabled = false;
                btnSave.Visibility = Visibility.Hidden;
                btnSave.IsEnabled = false;
                dGridRequests.IsReadOnly = true;
                btnEdit.Visibility = Visibility.Visible;
                btnReturnFromEdit.Visibility = Visibility.Hidden;
                dGridRequests.CanUserAddRows = false;
                btnAdd.Visibility = Visibility.Hidden;
                btnAdd.IsEnabled = false;


            }

            private void btnSave_Click(object sender, RoutedEventArgs e)
            {

                try
                {
                    dGridRequests.CanUserAddRows = false;
                    dGridRequests.UnselectAll();
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
                    var a = dGridRequests.SelectedItems.Cast<Requests>().ToList();
                    Entities.GetContext().Requests.RemoveRange(a);
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
                        var a = dGridRequests.SelectedItems.Cast<Requests>().ToList();
                        Entities.GetContext().Requests.AddRange(a);
                        Entities.GetContext().SaveChanges();
                        dGridRequests.UnselectAll();
                        btnSave.Visibility = Visibility.Visible;
                        btnSave.IsEnabled = true;
                        isAddBtnPressed = !isAddBtnPressed;
                    }
                    else
                    {
                        btnSave.Visibility = Visibility.Hidden;
                        dGridRequests.CanUserAddRows = true;
                        var a = dGridRequests.SelectedItems.Cast<Requests>().ToList();
                        Entities.GetContext().Requests.AddRange(a);
                        Entities.GetContext().SaveChanges();
                        dGridRequests.UnselectAll();
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