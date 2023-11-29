using Konfigurator.Windos;
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

namespace Konfigurator.Pages
{
    /// <summary>
    /// Логика взаимодействия для GlavPage.xaml
    /// </summary>
    public partial class GlavPage : Page
    {
        public GlavPage()
        {
            InitializeComponent();
        }

        private void Btn_User_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow=new UserWindow();
            userWindow.ShowDialog();
        }

        private void Btn_Admin_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("Pages/VxodAdminPage.xaml", UriKind.Relative));
        }
    }
}
