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
            string newDeparTitle = tbDep.Text.Trim();
            if (string.IsNullOrEmpty(newDeparTitle))
            {
                MessageBox.Show("Введите подразделение");
                return;
            }

            var dbContext = KonfigKcEntities.GetContext();
            var isDuplicate = dbContext.Departments.Any(sp => sp.DepartmentName == newDeparTitle);

            if (isDuplicate)
            {
                MessageBox.Show("Такая запись существует");
                listview.SelectedItem = null;
                return;
            }

            Departments newDep = new Departments { DepartmentName = newDeparTitle };

            if (listview.SelectedItem != null)
            {
                Departments selectedDep = (Departments)listview.SelectedItem;
                newDep.DepartmentID = selectedDep.DepartmentID;
                UpdatePosition(newDep);
            }
            else
            {
                dbContext.Departments.Add(newDep);
            }

            dbContext.SaveChanges();

            listview.SelectedItem = null;
            listview.ItemsSource = dbContext.Departments.ToList();
            tbDep.Clear();
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

        private void DelDepar_Click(object sender, RoutedEventArgs e)
        {
            var deparDel = listview.SelectedItems.Cast<Departments>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить эти {deparDel.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var dbContext = KonfigKcEntities.GetContext();

                foreach (var depart in deparDel)
                {
                    if (!dbContext.Requests.Any(item => item.DepartmentID == depart.DepartmentID))
                    {
                        dbContext.Departments.Remove(depart);
                        MessageBox.Show($"{depart.DepartmentName} успешно удален.");
                    }
                    else
                    {
                        MessageBox.Show($"{depart.DepartmentName} используется в других таблицах и не может быть удален.");
                    }
                }

                dbContext.SaveChanges();
                MessageBox.Show("Удаление прошло успешно");
                listview.ItemsSource = dbContext.Departments.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении файла: {ex.Message}");
            }
        }


    }
}
