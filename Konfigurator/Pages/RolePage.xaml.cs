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
            string newPositionTitle = tbPos.Text;
            var isDuplicate = KonfigKcEntities.GetContext().Positions.Any(sp => sp.PositionName==tbPos.Text);
            StringBuilder errors = new StringBuilder();
            if (tbPos.Text == null)
                errors.AppendLine("Введите должность");
            if (isDuplicate)
            {
                errors.AppendLine("Такая запись существует");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (!string.IsNullOrEmpty(newPositionTitle))
            {
                Positions newPosition = new Positions { PositionName = newPositionTitle };
                if (listview.SelectedItem != null)
                {
                    Positions selectedPosition = (Positions)listview.SelectedItem;
                    newPosition.PositionID = selectedPosition.PositionID;
                    UpdatePosition(newPosition);
                }
                else
                {
                    KonfigKcEntities.GetContext().Positions.Add(newPosition);
                }
                KonfigKcEntities.GetContext().SaveChanges();
                listview.SelectedItem = null;
                listview.ItemsSource = KonfigKcEntities.GetContext().Positions.ToList();
                tbPos.Clear();
            }
            else
            {
                MessageBox.Show("Введите название должности.");
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
    }
}
