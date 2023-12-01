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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Edit_bd_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Uri("Pages/InfoPage.xaml", UriKind.Relative));
        }

        private void Role_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/RolePage.xaml", UriKind.Relative));
        }

        private void Depart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/DepartPage.xaml",UriKind.Relative));
        }

        private void Soft_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/SoftwarePage.xaml", UriKind.Relative));
        }

        private void GoNack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow=new MainWindow();
            mainWindow.Show();
        }
    }
}
