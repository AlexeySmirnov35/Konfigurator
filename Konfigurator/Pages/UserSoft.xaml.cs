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
    /// Логика взаимодействия для UserSoft.xaml
    /// </summary>
    public partial class UserSoft : Page
    {
        
        public UserSoft()
        {
            InitializeComponent();
            listview.ItemsSource=KonfigKcEntities.GetContext().Software.ToList();
        }

        private void SoftwareListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listview.SelectedItem != null)
            {
                Software selectedSoftware = (Software)listview.SelectedItem;
                UserPageSoft userPageSoft = new UserPageSoft(selectedSoftware);
                NavigationService.Navigate(userPageSoft);
                
            }
        }

        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {
            UpdateSoftware();
        }
        private void UpdateSoftware()
        {
            var allSoftware = KonfigKcEntities.GetContext().Software.ToList();

            // Применяем фильтр по имени программы
            allSoftware = allSoftware.Where(s => s.SoftwareName.ToLower().Contains(tbox_Search.Text.ToLower())).ToList();

            // Сортируем программы по количеству (предполагается, что у Software есть свойство Count)
            listview.ItemsSource = allSoftware.OrderBy(s => s.SoftwareName).ToList();
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {

        }
    }
}
