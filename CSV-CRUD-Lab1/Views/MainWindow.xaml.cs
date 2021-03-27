using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CSV_CRUD_Lab1.Model;
using CSV_CRUD_Lab1.Repository;
namespace CSV_CRUD_Lab1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CSVRepository repo;
        public MainWindow()
        {
            InitializeComponent();
            repo = new CSVRepository();
            CarsGrid.ItemsSource = repo.GetCars();
            CarsGrid.SelectionMode = DataGridSelectionMode.Single;
            CarsGrid.SelectedItem = null;
        }


        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new CreateEditWindow(repo);
            addWindow.RefreshCallbackDelegate += this.Refresh;
            addWindow.Show();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isAnySelected()) return;

            var addWindow = new CreateEditWindow(GetCurrent(), repo);
            addWindow.RefreshCallbackDelegate += this.Refresh;
            addWindow.GetCurrentCallbackDelegate += this.GetCurrent;
            addWindow.DropDelegate += this.DropSelection;
            addWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnySelected()) return;
            repo.DeleteCar(CarsGrid.SelectedItem as Car);
            this.Refresh();
        }

        public bool isAnySelected()
        {
            if (CarsGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите элемент");
                return false;
            }

            return true;
        }
        public void Refresh()
        {
            CarsGrid.ItemsSource = repo.GetCars();
        }

        private Car GetCurrent()
        {
            return CarsGrid.SelectedItem as Car;
        }

        private void DropSelection()
        {
            CarsGrid.SelectedItem = null;
        }
    }
}