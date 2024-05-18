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
    /// Логика взаимодействия для RoomPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        private bool isAddBtnPressed = false;
        public RoomsPage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Номера";
            }
            InitializeComponent();
            dGridRooms.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Room in context.Room
                        select new
                        {
                            Id = Room.id,
                            Тип = Room.room_type,
                            Кроватей = Room.beds_count,
                            Ванная = Room.has_bathroom,
                            Площадь = Room.area
                        };

            dGridRooms.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridRooms.UnselectAll();
            dGridRooms.ItemsSource = Entities.GetContext().Room.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridRooms.IsReadOnly = false;
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
            var query = from Room in context.Room
                        select new
                        {
                            Id = Room.id,
                            Тип = Room.room_type,
                            Кроватей = Room.beds_count,
                            Ванная = Room.has_bathroom,
                            Площадь = Room.area
                        };

            dGridRooms.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridRooms.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridRooms.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridRooms.CanUserAddRows = false;
                dGridRooms.UnselectAll();
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
                var a = dGridRooms.SelectedItems.Cast<Room>().ToList();
                Entities.GetContext().Room.RemoveRange(a);
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
                    var a = dGridRooms.SelectedItems.Cast<Room>().ToList();
                    Entities.GetContext().Room.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridRooms.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridRooms.CanUserAddRows = true;
                    var a = dGridRooms.SelectedItems.Cast<Room>().ToList();
                    Entities.GetContext().Room.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridRooms.UnselectAll();
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
