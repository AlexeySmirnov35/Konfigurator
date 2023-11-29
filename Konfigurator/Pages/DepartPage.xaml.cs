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
    /// Логика взаимодействия для DepartPage.xaml
    /// </summary>
    public partial class DepartPage : Page
    {
        public DepartPage()
        {
            InitializeComponent();
            listview.ItemsSource=KonfigKcEntities.GetContext().Departments.ToList();
        }

        private void AddEditDepar_Click(object sender, RoutedEventArgs e)
        {
            string newDeparTitle = tbDep.Text;
            var isDuplicate = KonfigKcEntities.GetContext().Departments.Any(sp => sp.DepartmentName == tbDep.Text);
            StringBuilder errors = new StringBuilder();
            if (tbDep.Text == null)
                errors.AppendLine("Введите подразделение");
            if (isDuplicate)
            {
                errors.AppendLine("Такая запись существует");
                listview.SelectedItem = null;
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (!string.IsNullOrEmpty(newDeparTitle))
            {
                Departments newDep= new Departments { DepartmentName = newDeparTitle };
                if (listview.SelectedItem != null)
                {
                    Departments selectedDep = (Departments)listview.SelectedItem;
                    newDep.DepartmentID = selectedDep.DepartmentID;
                    UpdatePosition(newDep);
                }
                else
                {
                    KonfigKcEntities.GetContext().Departments.Add(newDep);
                }
                KonfigKcEntities.GetContext().SaveChanges();
                listview.SelectedItem = null;
                listview.ItemsSource = KonfigKcEntities.GetContext().Departments.ToList();
                tbDep.Clear();
            }
            else
            {
                MessageBox.Show("Введите название должности.");
            }
        }

        private void UpdatePosition(Departments departments)
        {
            var existingPosition = KonfigKcEntities.GetContext().Departments.Find(departments.DepartmentID);
            if (existingPosition != null)
            {
                existingPosition.DepartmentName = departments.DepartmentName;
            }
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
