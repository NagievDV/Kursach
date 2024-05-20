using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    /// Логика взаимодействия для Report2Page.xaml
    /// </summary>
    public partial class Report2Page : Page
    {
        public Report2Page()
        {
            InitializeComponent();
            var context = Entities.GetContext();
            var query = from Remaining_inventory in context.Remaining_inventory
                        select new
                        {
                            Наименование = Remaining_inventory.inventory_item,
                            Остаток = Remaining_inventory.amount
                        };

            dGridRemainingInventory.ItemsSource = query.ToList();
        }
    }
}
