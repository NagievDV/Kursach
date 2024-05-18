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
    /// Логика взаимодействия для InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        private bool isAddBtnPressed = false;
        public InventoryPage()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.lblText.Content = "Инвентарь";
            }
            InitializeComponent();
            dGridInventory.CanUserAddRows = false;
            var context = Entities.GetContext();
            var query = from Inventory in context.Remaining_inventory
                        select new
                        {
                            Id = Inventory.id,
                            Наименовая = Inventory.inventory_item,
                            Количество = Inventory.amount

                        };

            dGridInventory.ItemsSource = query.ToList();

            if (MainWindow.isAdmin == true)
            {
                btnEdit.Visibility = Visibility.Visible;
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dGridInventory.UnselectAll();
            dGridInventory.ItemsSource = Entities.GetContext().Remaining_inventory.ToList();
            btnEdit.Visibility = Visibility.Hidden;
            btnReturnFromEdit.Visibility = Visibility.Visible;
            dGridInventory.IsReadOnly = false;
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
            var query = from Inventory in context.Remaining_inventory
                        select new
                        {
                            Id = Inventory.id,
                            Наименование = Inventory.inventory_item,
                            Количество = Inventory.amount

                        };

            dGridInventory.ItemsSource = query.ToList();


            btnEdit.Content = "Редактировать";
            btnDelete.Visibility = Visibility.Hidden;
            btnDelete.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;
            dGridInventory.IsReadOnly = true;
            btnEdit.Visibility = Visibility.Visible;
            btnReturnFromEdit.Visibility = Visibility.Hidden;
            dGridInventory.CanUserAddRows = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                dGridInventory.CanUserAddRows = false;
                dGridInventory.UnselectAll();
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
                var a = dGridInventory.SelectedItems.Cast<Remaining_inventory>().ToList();
                Entities.GetContext().Remaining_inventory.RemoveRange(a);
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
                    var a = dGridInventory.SelectedItems.Cast<Remaining_inventory>().ToList();
                    Entities.GetContext().Remaining_inventory.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridInventory.UnselectAll();
                    btnSave.Visibility = Visibility.Visible;
                    btnSave.IsEnabled = true;
                    isAddBtnPressed = !isAddBtnPressed;
                }
                else
                {
                    btnSave.Visibility = Visibility.Hidden;
                    dGridInventory.CanUserAddRows = true;
                    var a = dGridInventory.SelectedItems.Cast<Remaining_inventory>().ToList();
                    Entities.GetContext().Remaining_inventory.AddRange(a);
                    Entities.GetContext().SaveChanges();
                    dGridInventory.UnselectAll();
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

