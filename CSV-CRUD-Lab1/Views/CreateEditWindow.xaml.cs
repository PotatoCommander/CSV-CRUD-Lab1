using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CSV_CRUD_Lab1.Model;
using CSV_CRUD_Lab1.Repository;

namespace CSV_CRUD_Lab1.Views
{
    /// <summary>
    /// Interaction logic for AddCreateWindow.xaml
    /// </summary>
    public partial class CreateEditWindow : Window
    {
        private bool isCreateAction;
        private CSVRepository repo;
        public CreateEditWindow(CSVRepository repository)
        {
            InitializeComponent();
            EventsInit();

            repo = repository;
            CreateEditButton.Content = "Создать";
            isCreateAction = true;
            ConditionComboBox.ItemsSource = Enum.GetValues(typeof(CarsCondition));
            BodyTypeCombobox.ItemsSource = Enum.GetValues(typeof(BodyTypes));
        }

        public CreateEditWindow(Car car)
        {
            InitializeComponent();
            EventsInit();

            BrandTextBox.Text = car.brand;
            ModelTextBox.Text = car.model;
            PriceTextBox.Text = car.price.ToString(CultureInfo.InvariantCulture);
            TypeOfFuelTextBox.Text = car.engine.typeOfFuel;
            YearTextBox.Text = car.yearOfProduction.ToString();
            numberOfCylindersTextBox.Text = car.engine.numberOfCylinders.ToString();
            numberOfValvesTextBox.Text = car.engine.numberOfValves.ToString();
            volumeTextBox.Text = car.engine.volumeSm.ToString();
            CreateEditButton.Content = "Редактировать";

            ConditionComboBox.ItemsSource = Enum.GetValues(typeof(CarsCondition));
            BodyTypeCombobox.ItemsSource = Enum.GetValues(typeof(BodyTypes));

            isCreateAction = false;
        }

        private void EventsInit()
        {
            PriceTextBox.PreviewTextInput += new TextCompositionEventHandler((s, e) => NumberValidationTextBox(s, e, 10));
            YearTextBox.PreviewTextInput += new TextCompositionEventHandler((s, e) => NumberValidationTextBox(s, e, 4));
            numberOfCylindersTextBox.PreviewTextInput += new TextCompositionEventHandler((s, e) => NumberValidationTextBox(s, e, 2));
            numberOfValvesTextBox.PreviewTextInput += new TextCompositionEventHandler((s, e) => NumberValidationTextBox(s, e, 3));
            volumeTextBox.PreviewTextInput += new TextCompositionEventHandler((s, e) => NumberValidationTextBox(s, e, 3));
        }

        private void CreateEditButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Create()
        {
            repo.AddCar(new Car()
            {
                price = Convert.ToInt32(PriceTextBox.Text),
                condition = (CarsCondition)ConditionComboBox.SelectedItem,
                bodyType = (BodyTypes)BodyTypeCombobox.SelectedItem,
                brand = BrandTextBox.Text,
                model = ModelTextBox.Text,
                yearOfProduction = Convert.ToInt32(YearTextBox.Text),
                engine = new Engine()
                {
                    numberOfCylinders = Convert.ToByte(numberOfCylindersTextBox.Text),
                    numberOfValves = Convert.ToByte(numberOfValvesTextBox.Text),
                    typeOfFuel = TypeOfFuelTextBox.Text,
                    volumeSm = Convert.ToInt32(volumeTextBox.Text)
                }
            });
        }
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e, int n)
        {
            var regex = new Regex(@"^\d+");
            var textBox = sender as TextBox;
            if (textBox.Text.Length < n && regex.IsMatch(e.Text))
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
}
}
