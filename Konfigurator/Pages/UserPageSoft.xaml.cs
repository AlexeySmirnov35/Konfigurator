using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для UserPageSoft.xaml
    /// </summary>
    public partial class UserPageSoft : Page
    {
        private Files selectedFile = null;
        private Software _software = new Software();
        public UserPageSoft(Software selectedsoftware)
        {
            InitializeComponent();
            if (selectedsoftware != null)
            {
                _software = selectedsoftware;
            }
            DataContext = _software;
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
        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            if (_software != null)
            {
                try
                {
                    var dbContext = KonfigKcEntities.GetContext();
                    var file = dbContext.Files.FirstOrDefault(f => f.FileID == _software.FileID);
                    if (file != null)
                    {
                        string tempFilePath = System.IO.Path.GetTempFileName();
                        File.WriteAllBytes(tempFilePath, file.FileContent);
                        System.Diagnostics.Process.Start("rundll32.exe", $"shell32.dll,OpenAs_RunDLL {tempFilePath}");
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден для выбранной программы.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Файл не был загружен");
            }
        }
    }
}
