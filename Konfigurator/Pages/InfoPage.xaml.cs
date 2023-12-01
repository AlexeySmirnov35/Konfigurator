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
    /// Логика взаимодействия для InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();
            softwareListView.ItemsSource = KonfigKcEntities.GetContext().SoftwarePosition.ToList();
        }

        private void SoftwareListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (softwareListView.SelectedItem != null)
            {
                SoftwarePosition selectedSoftware = (SoftwarePosition)softwareListView.SelectedItem;
                EditPageInformation editSoftwarePage = new EditPageInformation(selectedSoftware);
                NavigationService.Navigate(editSoftwarePage);
            }
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Btn_Create(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
