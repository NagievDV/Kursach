using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrEmpty(pbPassword.Password)) MessageBox.Show("Введите логин и пароль!");

            using (var db = new Entities())
            {
                var user = db.Staff.AsNoTracking().FirstOrDefault(u => u.login == tbLogin.Text && u.password == pbPassword.Password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                } else
                {
                    MainWindow.isLogined = true;
                }
                if (user.role == "admin")
                {
                    MainWindow.isAdmin = true;
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbLogin.Text = "";
            pbPassword.Password = "";
        }
        

        
    }
}
