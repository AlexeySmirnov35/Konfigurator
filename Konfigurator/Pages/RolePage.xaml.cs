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
    /// Логика взаимодействия для RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        public RolePage()
        {
            InitializeComponent();
            listview.ItemsSource=KonfigKcEntities.GetContext().Positions.ToList();
        }

        private void AddEditRole_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbPos.Text))
            {
                errors.AppendLine("Введите корректное название должности");
            }
            else
            {
                var dbContext = KonfigKcEntities.GetContext();
                var isDuplicate = dbContext.Positions.Any(sp => sp.PositionName == tbPos.Text);

                if (isDuplicate)
                {
                    errors.AppendLine("Такая запись существует");
                }

                if (errors.Length == 0)
                {
                    Positions newPosition = new Positions { PositionName = tbPos.Text };

                    if (listview.SelectedItem != null)
                    {
                        Positions selectedPosition = (Positions)listview.SelectedItem;
                        newPosition.PositionID = selectedPosition.PositionID;
                        UpdatePosition(newPosition);
                    }
                    else
                    {
                        dbContext.Positions.Add(newPosition);
                    }

                    dbContext.SaveChanges();
                    listview.SelectedItem = null;
                    listview.ItemsSource = dbContext.Positions.ToList();
                    tbPos.Clear();
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
            }
        }




        private void UpdatePosition(Positions newPosition)
        {
            
            var existingPosition = KonfigKcEntities.GetContext().Positions.Find(newPosition.PositionID);
             if (existingPosition != null)
            {
                existingPosition.PositionName = newPosition.PositionName;
            }
        }

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DelRole_Click(object sender, RoutedEventArgs e)
        {
            var positionsToDelete = listview.SelectedItems.Cast<Positions>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить эти {positionsToDelete.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var dbContext = KonfigKcEntities.GetContext();

                foreach (var position in positionsToDelete)
                {
                    if (!IsPositionUsedInOtherTables(position))
                    {
                        dbContext.Positions.Remove(position);
                    }
                    else
                    {
                        MessageBox.Show($"Должность {position.PositionName} используется в других таблицах и не может быть удалена.");
                    }
                }

                dbContext.SaveChanges();
                MessageBox.Show("Удаление прошло успешно");
                listview.ItemsSource = dbContext.Positions.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении должности: {ex.Message}");
            }
        }

        private bool IsPositionUsedInOtherTables(Positions position)
        {
            var dbContext = KonfigKcEntities.GetContext();
            return dbContext.SoftwarePosition.Any(item => item.PositionID == position.PositionID)
                || dbContext.Requests.Any(item => item.PositionID == position.PositionID);
        }



    }
}

