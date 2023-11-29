using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для SoftInfoPage.xaml
    /// </summary>
    
    public partial class SoftInfoPage : Page
    {
        private Software _software=new Software();
        public SoftInfoPage(Software selectedsoftware)
        {
            InitializeComponent();
            if (selectedsoftware != null)
            {
                _software = selectedsoftware;
            }
            
            DataContext = _software;
            cbFile.ItemsSource=KonfigKcEntities.GetContext().Files.ToList();
            cbFile.Items.Refresh();

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (Uri.TryCreate(e.Uri.ToString(), UriKind.Absolute, out Uri uriResult))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("chrome.exe", _software.WebUrl));

                e.Handled = true;
            }
            else
            {
                Debug.WriteLine("Некорректный URL");
            }
        }

        private void Btn_Save_Soft_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_software.SoftwareName))
                errors.AppendLine("Укажите название программы!");
            if (string.IsNullOrWhiteSpace(_software.Description))
                errors.AppendLine("Напишите описание!");
            if (string.IsNullOrWhiteSpace(_software.Version))
                errors.AppendLine("Укажите версию!");
            if (string.IsNullOrWhiteSpace(_software.UpdateDescription))
                errors.AppendLine("Напишите описание обновления!");
            if (string.IsNullOrWhiteSpace(_software.WebUrl))
                errors.AppendLine("Укажите ссылку на документацию!");
            if (_software.LastUpdateDate == null)
                errors.AppendLine("Выберите дату последнего обновления!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_software.SoftwareID == 0)
            {


                var soft = new Software
                {
                    SoftwareName=tbName.Text,
                    Description=tbDes.Text,
                    LastUpdateDate=DtPicker.SelectedDate,
                    UpdateDescription=tbUpDesc.Text,
                    Version=tbVer.Text,
                    WebUrl=tbWeb.Text

                };
                KonfigKcEntities.GetContext().Software.Add(soft);

            }

            try
            {
                KonfigKcEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно сохранено");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/FilePage.xaml", UriKind.Relative));
            
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbFile.ItemsSource = KonfigKcEntities.GetContext().Files.ToList();
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
