using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED.Panels
{
    public partial class ViewCar : Window
    {
        private List<Car> cars = new List<Car>();
        private Car selectedCar = null;
        private MyDBContext context = new MyDBContext();

        public ViewCar()
        {
            InitializeComponent();
            cars = context.Cars.ToList();
            lbCars.ItemsSource = cars;
            cmbCustomer.ItemsSource = context.Customers.ToList();
        }

        private void lbCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            selectedCar = lbCars.SelectedItem as Car;

            if (selectedCar != null)
            {
                txtMake.Text = selectedCar.Make;
                txtModel.Text = selectedCar.Model;
                txtLicensePlate.Text = selectedCar.LicensePlate;
                txtChassisNumber.Text = selectedCar.ChassisNumber;
                if (selectedCar.CustomerId != null)
                {
                    // la expresion landa   
                    var selectedCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == selectedCar.CustomerId);
                    if (selectedCustomer != null)
                    {
                        cmbCustomer.SelectedItem = selectedCustomer;
                    }
                }
                txtError.Text = string.Empty;
            }
        }

        private void btnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string make = txtMake.Text;
                string model = txtModel.Text;
                string licensePlate = txtLicensePlate.Text;
                string chassisNumber = txtChassisNumber.Text;
                int customerID;

                if (cmbCustomer.SelectedValue != null && int.TryParse(cmbCustomer.SelectedValue.ToString(), out customerID))
                {
                    // Usar customerID para el ID del cliente seleccionado
                }
                else
                {
                    throw new ArgumentException("Selecciona un cliente.");
                }

                var newCar = new Car
                {
                    Make = make,
                    Model = model,
                    LicensePlate = licensePlate,
                    ChassisNumber = chassisNumber,
                    CustomerId = customerID
                };

                context.Cars.Add(newCar);
                context.SaveChanges();

                cars = context.Cars.ToList();
                lbCars.ItemsSource = cars;

                createForm.Visibility = Visibility.Collapsed;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }
        private void btnCreateCar_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;
        }

    }
}
