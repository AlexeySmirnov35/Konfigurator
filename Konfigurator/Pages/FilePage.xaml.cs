using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string fileName = tbContent.Text.Trim();
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(fileName))
            {
                errors.AppendLine("Выберите файл.");
            }
            else
            {
                var dbContext = KonfigKcEntities.GetContext();

                var isDuplicate = dbContext.Files.Any(f => f.FileName == fileName);

                if (isDuplicate)
                {
                    errors.AppendLine("Такой файл уже существует.");
                }

                if (errors.Length == 0)
                {
                    if (selectedFile != null)
                    {
                        selectedFile.FileName = fileName;
                        selectedFile.FileContent = fileContent;
                    }
                    else
                    {
                        Files newFile = new Files { FileName = fileName, FileContent = fileContent };
                        dbContext.Files.Add(newFile);
                    }

                    dbContext.SaveChanges();
                    listview.ItemsSource = dbContext.Files.ToList();
                    tbContent.Clear();
                    selectedFile = null;
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
            }
        }
        private void Btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Btn_DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            var filesToDelete = listview.SelectedItems.Cast<Files>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить эти {filesToDelete.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                var dbContext = KonfigKcEntities.GetContext();

                foreach (var file in filesToDelete)
                {
                    if (!dbContext.Software.Any(item => item.FileID == file.FileID))
                    {
                        dbContext.Files.Remove(file);
                    }
                    else
                    {
                        MessageBox.Show($"Файл {file.FileName} используется в других таблицах и не может быть удален.");
                    }
                }

                dbContext.SaveChanges();
                MessageBox.Show("Удаление прошло успешно");
                listview.ItemsSource = dbContext.Files.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении файла: {ex.Message}");
            }
        }


        private void Btn_OpenFile(object sender, RoutedEventArgs e)
        {
            if (selectedFile != null)
            {
                try
                {
                    string tempFilePath = Path.GetTempFileName();
                    File.WriteAllBytes(tempFilePath, selectedFile.FileContent);
                    System.Diagnostics.Process.Start(tempFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите файл для открытия.");
            }
        }
    

    }
}

