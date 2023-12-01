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
        private SoftwarePosition _software=new SoftwarePosition();
        public InfoPage()
        {
            InitializeComponent();
            softwareListView.ItemsSource = KonfigKcEntities.GetContext().SoftwarePosition.ToList();
            cbSoft.ItemsSource = KonfigKcEntities.GetContext().Software.ToList();
            cbLis.ItemsSource = KonfigKcEntities.GetContext().LicensiaInfo.ToList();
            cbPosir.ItemsSource=KonfigKcEntities.GetContext().Positions.ToList();
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
            var prog = cbSoft.SelectedItem as Software;
            var linc = cbLis.SelectedItem as LicensiaInfo;
            var posit = cbPosir.SelectedItem as Positions;

            if (prog == null || linc == null || posit == null)
            {
                MessageBox.Show("Выберите программу, должность и необходимость лицензии");
                return;
            }

            var dbContext = KonfigKcEntities.GetContext();

            try
            {
                if (_software.PositionID == 0)
                {
                    var isDuplicate = dbContext.SoftwarePosition
                        .Any(sp => sp.PositionID == posit.PositionID && sp.SoftwareID == prog.SoftwareID);

                    if (isDuplicate)
                    {
                        MessageBox.Show("Такая запись существует");
                        return;
                    }

                    var newSoftwarePosition = new SoftwarePosition
                    {
                        SoftwareID = prog.SoftwareID,
                        LicenseID = linc.LicenseID,
                        PositionID = posit.PositionID
                    };

                    dbContext.SoftwarePosition.Add(newSoftwarePosition);
                    dbContext.SaveChanges();

                    MessageBox.Show("Успешно сохранено");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }



        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            var selectedSoftwareList = softwareListView.SelectedItems.Cast<SoftwarePosition>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить эти {selectedSoftwareList.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var dbContext = KonfigKcEntities.GetContext();

                    foreach (var selectedSoftware in selectedSoftwareList)
                    {
                        dbContext.SoftwarePosition.Remove(selectedSoftware);
                    }

                    dbContext.SaveChanges();
                    MessageBox.Show("Удаление прошло успешно");
                    softwareListView.ItemsSource = dbContext.SoftwarePosition.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении элементов: {ex.Message}");
                }
            }
        }
    }
}
