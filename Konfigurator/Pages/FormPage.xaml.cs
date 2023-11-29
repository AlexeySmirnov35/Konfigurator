using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Linq;
using Org.BouncyCastle;
using Microsoft.Win32;
using System.Runtime.Remoting.Contexts;
using iText.Kernel.XMP.Impl;


namespace Konfigurator.Pages
{
    /// <summary>
    /// Логика взаимодействия для FormPage.xaml
    /// </summary>
    public partial class FormPage : Page
    {
        private Requests _requests = new Requests();
        public FormPage()
        {
            InitializeComponent();
            var context = KonfigKcEntities.GetContext();
            cbPosir.ItemsSource = KonfigKcEntities.GetContext().Positions.ToList();
            cbDepar.ItemsSource = KonfigKcEntities.GetContext().Departments.ToList();
            UpdatePo();
            var lastRequestId = context.Requests.Max(r => (int?)r.RequestID) ?? 0;
            if (lastRequestId != null)
            {
                int newRequestId = lastRequestId + 1;
                tbInc.Text = newRequestId.ToString();
            }
            else
            {
                MessageBox.Show("Возникла ошибка создание заявки");
            }

        }
        private void UpdatePo()
        {
            if (cbPosir.SelectedItem != null)
            {
                Positions selectedPosition = cbPosir.SelectedItem as Positions;

                if (selectedPosition != null)
                {
                    var softwareForPosition = selectedPosition.SoftwarePosition
                        .Select(sp => sp.Software.SoftwareName)
                        .ToList();

                    if (softwareForPosition.Any())
                    {
                        StringBuilder numberedSoftwareList = new StringBuilder();
                        for (int i = 0; i < softwareForPosition.Count; i++)
                        {
                            numberedSoftwareList.AppendLine($"{i + 1}. {softwareForPosition[i]}");
                        }

                        tbPO.Text = numberedSoftwareList.ToString();
                    }
                    else
                    {
                        tbPO.Text = "Нет данных для отображения.";
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdatePo();
        }
        private void Create_Req_Click(object sender, RoutedEventArgs e)
        {

       /*  StringBuilder errors = new StringBuilder();
           
            if (string.IsNullOrWhiteSpace(_requests.Description))
                errors.AppendLine("Напишите описание!");
            if (_requests.DepartmentID == null)
                errors.AppendLine("Укажите категорию зверька!");
            if (_requests.PositionID == null)
                errors.AppendLine("Укажите категорию массу!");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }*/
           
            if (_requests.RequestID == 0)
            {
                var depar = cbDepar.SelectedItem as Departments;

                var posit = cbPosir.SelectedItem as Positions;
                //var animal=ZooBdEntities1.GetContext().TypeAnimals.Where(x=>x.NameType==tbAmimal.SelectedItem.ToString()).FirstOrDefault;
                var request = new Requests
                {
                    DepartmentID = depar.DepartmentID,
                    PositionID = posit.PositionID,
                    StatusID=1,
                    Description=tbDesc.Text,
                    RequestDate = DateTime.Now
                };
                KonfigKcEntities.GetContext().Requests.Add(request);
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

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
    

