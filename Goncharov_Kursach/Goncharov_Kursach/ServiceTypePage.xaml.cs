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
    /// Логика взаимодействия для ServiceTypePage.xaml
    /// </summary>
    public partial class ServiceTypePage : Page
    {
        private bool isAddBtnPressed = false;
        public ServiceTypePage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Перечень";
            }
            InitializeComponent();
            dGridServiceType.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Service_type in context.Service_type
                        select new
                        {
                            Id = Service_type.id,
                            Услуга = Service_type.service,
                            Стоимость = Service_type.cost,
                            ПоСкидке = Service_type.on_sale
                        };

            dGridServiceType.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridServiceType.UnselectAll();
            dGridServiceType.ItemsSource = Entities.GetContext().Service_type.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridServiceType.IsReadOnly = false;
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
            var query = from Service_type in context.Service_type
                        select new
                        {
                            Id = Service_type.id,
                            Услуга = Service_type.service,
                            Стоимость = Service_type.cost,
                            ПоСкидке = Service_type.on_sale
                        };

            dGridServiceType.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridServiceType.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridServiceType.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridServiceType.CanUserAddRows = false;
                dGridServiceType.UnselectAll();
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
                var a = dGridServiceType.SelectedItems.Cast<Service_type>().ToList();
                Entities.GetContext().Service_type.RemoveRange(a);
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
                    var a = dGridServiceType.SelectedItems.Cast<Service_type>().ToList();
                    Entities.GetContext().Service_type.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridServiceType.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridServiceType.CanUserAddRows = true;
                    var a = dGridServiceType.SelectedItems.Cast<Service_type>().ToList();
                    Entities.GetContext().Service_type.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridServiceType.UnselectAll();
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
