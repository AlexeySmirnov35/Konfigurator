using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Konfigurator.Pages
{
    public partial class FilePage : Page
    {
        private byte[] fileContent = null;
        private Files selectedFile = null;

        public FilePage()
        {
            InitializeComponent();
            listview.ItemsSource = KonfigKcEntities.GetContext().Files.ToList();
            listview.SelectionChanged += Listview_SelectionChanged;
        }

        private void Listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // При изменении выделенной строки, заполните соответствующие элементы управления значениями выбранной строки.
            if (listview.SelectedItem != null)
            {
                selectedFile = (Files)listview.SelectedItem;
                tbContent.Text = selectedFile.FileName;
            }
        }

        private void Btn_AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();

            fileOpen.Title = "Выберите файл";
            fileOpen.Multiselect = false;
            fileOpen.Filter = "Файлы PDF|*.pdf";

            if (fileOpen.ShowDialog() == true)
            {
                fileContent = File.ReadAllBytes(fileOpen.FileName);
                tbContent.Text = fileOpen.SafeFileName;
            }
        }

        private void Btn_SaveFile_Click(object sender, RoutedEventArgs e)
        {
            string fileName = tbContent.Text;
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(fileName))
                errors.AppendLine("Выберите файл.");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (selectedFile != null) // Если строка была выбрана, то обновите данные в базе данных
            {
                selectedFile.FileName = fileName;
                selectedFile.FileContent = fileContent;
            }
            else // Иначе, добавьте новый файл в базу данных
            {
                Files newFile = new Files { FileName = fileName, FileContent = fileContent };
                KonfigKcEntities.GetContext().Files.Add(newFile);
            }

            KonfigKcEntities.GetContext().SaveChanges();
            listview.ItemsSource = KonfigKcEntities.GetContext().Files.ToList();
            tbContent.Clear();
            selectedFile = null; // Сброс выбранного файла после сохранения
        }
    }
}
