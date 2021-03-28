using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CSV_CRUD_Lab1.Model;
using CSV_CRUD_Lab1.Repository;
using Microsoft.Win32;

namespace CSV_CRUD_Lab1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CSVRepository _repo;

        public MainWindow()
        {
            InitializeComponent();
            _repo = new CSVRepository();
            CarsGrid.SelectionMode = DataGridSelectionMode.Single;
            CarsGrid.SelectedItem = null;
            PathLabelShow();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_repo.FilePath))
            {
                MessageBox.Show("Не задан путь");
                return;
            }

            var addWindow = new CreateEditWindow(_repo);
            addWindow.RefreshCallbackDelegate += this.ShowGrid;
            addWindow.Show();
            DropSelection();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_repo.FilePath))
            {
                MessageBox.Show("Не задан путь");
                return;
            }

            if (!IsAnySelected()) return;
            var addWindow = new CreateEditWindow(GetCurrent(), _repo);
            addWindow.RefreshCallbackDelegate += ShowGrid;
            addWindow.GetCurrentCallbackDelegate += GetCurrent;
            addWindow.DropDelegate += DropSelection;
            addWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_repo.FilePath))
            {
                MessageBox.Show("Не задан путь");
                return;
            }

            if (!IsAnySelected()) return;
            _repo.DeleteCar(CarsGrid.SelectedItem as Car); 
            ShowGrid();
        }

        public bool IsAnySelected()
        {
            if (CarsGrid.SelectedItems.Count != 0) return true;
            MessageBox.Show("Выберите элемент");
            return false;
        }


        private Car GetCurrent()
        {
            return CarsGrid.SelectedItem as Car;
        }

        private void DropSelection()
        {
            CarsGrid.SelectedItem = null;
        }

        private void FileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv*)|*.csv*",
                FilterIndex = 1
            };

            if (fbd.ShowDialog() != true) return;
            _repo.FilePath = fbd.FileName;
            ShowGrid();
            PathLabelShow(_repo.FilePath);
        }

        private void PathLabelShow(string path = "УКАЖИТЕ ПУТЬ К ФАЙЛУ")
        {
            FilePathLabel.Content = $"Путь к файлу: {path}";
        }

        private void ShowGrid()
        {
            CarsGrid.ItemsSource = _repo.GetCars();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new SaveFileDialog
            {
                DefaultExt = "csv", 
                Filter = "CSV Files (*.csv*)|*.csv*", 
                FilterIndex = 1
            };

            if (fbd.ShowDialog() != true) return;
            _repo.FilePath = fbd.FileName; 
            var fs = File.Create(fbd.FileName);
            fs.Close();
            MessageBox.Show("Файл создан");
            ShowGrid();
            PathLabelShow(_repo.FilePath);
        }
    }
}