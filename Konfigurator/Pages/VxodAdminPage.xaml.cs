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
    /// Логика взаимодействия для VxodAdminPage.xaml
    /// </summary>
    public partial class VxodAdminPage : Page
    {
        public VxodAdminPage()
        {
            InitializeComponent();
        }

        private void Btn_Vxod(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbLog.Text == "1" && tbPas.Password=="1")
                {
                  MessageBox.Show("Hello!");
                  AdminWindow adminWindow=new AdminWindow();
                  adminWindow.Show();
                  
                }
                else
                {
                    MessageBox.Show("Ne verno");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Net");
            }
            
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
