using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CSV_CRUD_Lab1.Model;
using CSV_CRUD_Lab1.Repository;

namespace CSV_CRUD_Lab1.Views
{
    /// <summary>
    /// Interaction logic for AddCreateWindow.xaml
    /// </summary>
    public partial class CreateEditWindow : Window
    {
        private bool _isCreateAction;
        private CSVRepository _repo;

        public delegate void RefreshGridDelegate();
        public RefreshGridDelegate RefreshCallbackDelegate;

        public delegate Car GetCurrentDelegate();
        public GetCurrentDelegate GetCurrentCallbackDelegate;

        public delegate void DropSelectionDelegate();
        public DropSelectionDelegate DropDelegate;

        public CreateEditWindow(CSVRepository repository)
        {
            InitializeComponent();
            EventsInit();
            InitEnums();

            _repo = repository;
            CreateEditButton.Content = "Создать";
            _isCreateAction = true;

            ConditionComboBox.SelectedItem = CarsCondition.Good;
            BodyTypeCombobox.SelectedItem = BodyTypes.Wagon;
        }

        public CreateEditWindow(Car car, CSVRepository repository)
        {
            InitializeComponent();
            EventsInit();
            InitEnums();

            _repo = repository;
            //textbox init
            TextBoxEditInit(car);
            //textbox init

            ConditionComboBox.SelectedItem = car.condition;
            BodyTypeCombobox.SelectedItem = car.bodyType;

            _isCreateAction = false;
        }

        private void TextBoxEditInit(Car car)
        {
            BrandTextBox.Text = car.brand;
            ModelTextBox.Text = car.model;
            PriceTextBox.Text = car.price.ToString(CultureInfo.InvariantCulture);
            TypeOfFuelTextBox.Text = car.engine.typeOfFuel;
            YearTextBox.Text = car.yearOfProduction.ToString();
            numberOfCylindersTextBox.Text = car.engine.numberOfCylinders.ToString();
            numberOfValvesTextBox.Text = car.engine.numberOfValves.ToString();
            volumeTextBox.Text = car.engine.volumeSm.ToString();
            CreateEditButton.Content = "Редактировать";
        }

        private void EventsInit()
        {
            PriceTextBox.PreviewTextInput += (s, e) => NumberValidationTextBox(s, e, 10);
            YearTextBox.PreviewTextInput += (s, e) => NumberValidationTextBox(s, e, 4);
            numberOfCylindersTextBox.PreviewTextInput += (s, e) => NumberValidationTextBox(s, e, 2);
            numberOfValvesTextBox.PreviewTextInput += (s, e) => NumberValidationTextBox(s, e, 2);
            volumeTextBox.PreviewTextInput += (s, e) => NumberValidationTextBox(s, e, 6);
        }

        private void InitEnums()
        {
            ConditionComboBox.ItemsSource = Enum.GetValues(typeof(CarsCondition));
            BodyTypeCombobox.ItemsSource = Enum.GetValues(typeof(BodyTypes));
        }

        private void CreateEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isCreateAction)
                {
                    Create();
                }
                else
                {
                    Edit(GetCurrentCallbackDelegate());
                }

                RefreshCallbackDelegate();
                DropDelegate?.Invoke();
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод");
            }
        }

        private void Create()
        {
            _repo.AddCar(new Car
                {
                    price = Convert.ToInt32(PriceTextBox.Text),
                    condition = (CarsCondition) ConditionComboBox.SelectedItem,
                    bodyType = (BodyTypes) BodyTypeCombobox.SelectedItem,
                    brand = BrandTextBox.Text,
                    model = ModelTextBox.Text,
                    yearOfProduction = Convert.ToInt32(YearTextBox.Text),
                    engine = new Engine
                    {
                        numberOfCylinders = Convert.ToByte(numberOfCylindersTextBox.Text),
                        numberOfValves = Convert.ToByte(numberOfValvesTextBox.Text),
                        typeOfFuel = TypeOfFuelTextBox.Text,
                        volumeSm = Convert.ToInt32(volumeTextBox.Text)
                    }
                });
        }

        private void Edit(Car item)
        {
            _repo.UpdateCar(new Car
            {
                id = item.id,
                price = Convert.ToInt32(PriceTextBox.Text),
                condition = (CarsCondition)ConditionComboBox.SelectedItem,
                bodyType = (BodyTypes)BodyTypeCombobox.SelectedItem,
                brand = BrandTextBox.Text,
                model = ModelTextBox.Text,
                yearOfProduction = Convert.ToInt32(YearTextBox.Text),
                engine = new Engine
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
            if (textBox != null && textBox.Text.Length < n && regex.IsMatch(e.Text))
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }
}
}
