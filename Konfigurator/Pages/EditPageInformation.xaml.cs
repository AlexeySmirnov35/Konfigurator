using Microsoft.Win32;
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
    /// Логика взаимодействия для EditPageInformation.xaml
    /// </summary>
    public partial class EditPageInformation : Page
    {
        
        private SoftwarePosition _software=new SoftwarePosition();
        public EditPageInformation(SoftwarePosition selectSoftware)
        {
            InitializeComponent();
            if (selectSoftware != null)
            {
                _software = selectSoftware;
            }
            cbAllProg.ItemsSource=KonfigKcEntities.GetContext().Software.ToList();
            cbLinc.ItemsSource=KonfigKcEntities.GetContext().LicensiaInfo.ToList();
            DataContext = _software;
            tbVProg.Text=_software.Software.SoftwareName.ToString();
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            var prog = cbAllProg.SelectedItem as Software;
            var linc = cbLinc.SelectedItem as LicensiaInfo;
            var isDuplicate = KonfigKcEntities.GetContext().SoftwarePosition.Any(sp =>sp.PositionID == _software.PositionID && sp.SoftwareID == prog.SoftwareID);
            StringBuilder errors= new StringBuilder();
            if (prog == null)
                errors.AppendLine("Выберите программу для замены");
            if (linc == null)
                errors.AppendLine("Выберите необходимость лицензии");
            if (isDuplicate)
            {
                errors.AppendLine("Такая запись существует");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_software.PositionID == 0)
            {
                

                 var softpos = new SoftwarePosition
                 {
                     
                     SoftwareID = prog.SoftwareID,
                     LicenseID= linc.LicenseID

                 };
                KonfigKcEntities.GetContext().SoftwarePosition.Add(softpos);
             
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

        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Image_Load(object sender, RoutedEventArgs e)
        {
            

        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
