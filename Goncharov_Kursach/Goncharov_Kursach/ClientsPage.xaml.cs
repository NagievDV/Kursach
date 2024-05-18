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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private bool isAddBtnPressed = false;
        public ClientsPage()
        {
            InitializeComponent();
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Клиенты";
            }
            dGridClients.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Client in context.Client
                        select new
                        {
                            Id = Client.id,
                            Имя = Client.first_name,
                            Фамилия = Client.middle_name,
                            Отчество = Client.last_name,
                            Телефон = Client.phone_number,
                            Email = Client.email,
                            СчётОплаты = Client.payment_account
                        };

            dGridClients.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridClients.UnselectAll();
            dGridClients.ItemsSource = Entities.GetContext().Client.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridClients.IsReadOnly = false;
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
            var query = from Client in context.Client
                        select new
                        {
                            Id = Client.id,
                            Имя = Client.first_name,
                            Фамилия = Client.middle_name,
                            Отчество = Client.last_name,
                            Телефон = Client.phone_number,
                            Email = Client.email,
                            СчётОплаты = Client.payment_account
                        };

            dGridClients.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridClients.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridClients.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridClients.CanUserAddRows = false;
                dGridClients.UnselectAll();
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
                var a = dGridClients.SelectedItems.Cast<Client>().ToList();
                Entities.GetContext().Client.RemoveRange(a);
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
                    var a = dGridClients.SelectedItems.Cast<Client>().ToList();
                    Entities.GetContext().Client.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridClients.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridClients.CanUserAddRows = true;
                    var a = dGridClients.SelectedItems.Cast<Client>().ToList();
                    Entities.GetContext().Client.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridClients.UnselectAll();
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
