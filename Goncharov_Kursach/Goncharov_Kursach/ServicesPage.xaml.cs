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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        private bool isAddBtnPressed = false;
        public ServicesPage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Услуги";
            }
            InitializeComponent();
            dGridServices.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Services in context.Services
                        join Staff in context.Staff on Services.responsible equals Staff.id
                        join Service_type in context.Service_type on Services.type equals Service_type.id
                        select new
                        {
                            Id = Services.id,
                            Ответстенный = Staff.last_name + " " + Staff.middle_name,
                            Услуга = Service_type.service

                        };

            dGridServices.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridServices.UnselectAll();
            dGridServices.ItemsSource = Entities.GetContext().Services.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridServices.IsReadOnly = false;
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
            var query = from Services in context.Services
                        join Staff in context.Staff on Services.responsible equals Staff.id
                        join Service_type in context.Service_type on Services.type equals Service_type.id
                        select new
                        {
                            Id = Services.id,
                            Ответстенный = Staff.last_name + " " + Staff.middle_name,
                            Услуга = Service_type.service

                        };

            dGridServices.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridServices.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridServices.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridServices.CanUserAddRows = false;
                dGridServices.UnselectAll();
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
                var a = dGridServices.SelectedItems.Cast<Services>().ToList();
                Entities.GetContext().Services.RemoveRange(a);
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
                    var a = dGridServices.SelectedItems.Cast<Services>().ToList();
                    Entities.GetContext().Services.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridServices.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridServices.CanUserAddRows = true;
                    var a = dGridServices.SelectedItems.Cast<Services>().ToList();
                    Entities.GetContext().Services.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridServices.UnselectAll();
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
