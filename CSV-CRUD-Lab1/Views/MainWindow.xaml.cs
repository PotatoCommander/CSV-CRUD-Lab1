using System;
using System.Collections.Generic;
using System.Windows;
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
        }


        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            CarsGrid.ItemsSource = repo.GetCars();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new CreateEditWindow(new Car() {engine = new Engine()});
            addWindow.Show();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}