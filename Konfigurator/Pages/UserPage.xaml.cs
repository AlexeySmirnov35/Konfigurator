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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
        }

        private void Btn_AddRequest(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/FormPage.xaml", UriKind.Relative));
        }

        private void Btn_Jornul_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/PageJornul.xaml", UriKind.Relative));
        }
    }
}
