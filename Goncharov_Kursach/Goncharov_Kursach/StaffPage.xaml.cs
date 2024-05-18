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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private bool isAddBtnPressed = false;
        public StaffPage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Персонал";
            }
            InitializeComponent();
            dGridStaff.CanUserAddRows = false;
            var context = Entities.GetContext();
            if (!(MainWindow.isAdmin))
            {
                var query = from Staff in context.Staff
                            select new
                            {
                                Id = Staff.id,
                                Должность = Staff.post,
                                ФИО = Staff.last_name + " " + Staff.first_name + " " + Staff.middle_name,
                                Телефон = Staff.phone_number,
                                Email = Staff.email,
                                Зарплата = Staff.salary,
                                Квалификация = Staff.qualification,
                            };
                dGridStaff.ItemsSource = query.ToList();

            }

            else
            {
                var query = from Staff in context.Staff
                            select new
                            {
                                Id = Staff.id,
                                Должность = Staff.post,
                                ФИО = Staff.last_name + " " + Staff.first_name + " " + Staff.middle_name,
                                Телефон = Staff.phone_number,
                                Email = Staff.email,
                                Зарплата = Staff.salary,
                                Квалификация = Staff.qualification,
                                Логин = Staff.login,
                                Пароль = Staff.password,
                                Роль = Staff.role
                            };
                dGridStaff.ItemsSource = query.ToList();
            }



            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridStaff.UnselectAll();
            dGridStaff.ItemsSource = Entities.GetContext().Staff.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridStaff.IsReadOnly = false;
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
            if (!(MainWindow.isAdmin))
            {
                var query = from Staff in context.Staff
                            select new
                            {
                                Id = Staff.id,
                                Должность = Staff.post,
                                ФИО = Staff.last_name + " " + Staff.first_name + " " + Staff.middle_name,
                                Телефон = Staff.phone_number,
                                Email = Staff.email,
                                Зарплата = Staff.salary,
                                Квалификация = Staff.qualification,
                            };
                dGridStaff.ItemsSource = query.ToList();
            }

            else
            {
                var query = from Staff in context.Staff
                            select new
                            {
                                Id = Staff.id,
                                Должность = Staff.post,
                                ФИО = Staff.last_name + " " + Staff.first_name + " " + Staff.middle_name,
                                Телефон = Staff.phone_number,
                                Email = Staff.email,
                                Зарплата = Staff.salary,
                                Квалификация = Staff.qualification,
                                Логин = Staff.login,
                                Пароль = Staff.password,
                                Роль = Staff.role
                            };
                dGridStaff.ItemsSource = query.ToList();
            }


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridStaff.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridStaff.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridStaff.CanUserAddRows = false;
                dGridStaff.UnselectAll();
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
                var a = dGridStaff.SelectedItems.Cast<Staff>().ToList();
                Entities.GetContext().Staff.RemoveRange(a);
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
                    var a = dGridStaff.SelectedItems.Cast<Staff>().ToList();
                    Entities.GetContext().Staff.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridStaff.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridStaff.CanUserAddRows = true;
                    var a = dGridStaff.SelectedItems.Cast<Staff>().ToList();
                    Entities.GetContext().Staff.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridStaff.UnselectAll();
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
