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

                if (string.IsNullOrWhiteSpace(make))
                {
                    throw new ArgumentException("The make cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(model))
                {
                    throw new ArgumentException("The model cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(licensePlate))
                {
                    throw new ArgumentException("The licenseplate cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(chassisNumber))
                {
                    throw new ArgumentException("The chassisnumber cannot be empty.");
                }

                if (cmbCustomer.SelectedValue != null && int.TryParse(cmbCustomer.SelectedValue.ToString(), out customerID))
                {
                    if (context.Cars.Any(c => c.LicensePlate == licensePlate))
                    {
                        throw new ArgumentException("The license plate already exists in the database.");
                    }

                    if (context.Cars.Any(c => c.ChassisNumber == chassisNumber))
                    {
                        throw new ArgumentException("The chassis number already exists in the database.");
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
                else
                {
                    throw new ArgumentException("Select a customer.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }


        private void btnCreateCar_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;
            selectedCar = null;

            // limpiamos los campos
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtLicensePlate.Text = string.Empty;
            txtChassisNumber.Text = string.Empty;
            cmbCustomer.SelectedItem = null;
            txtError.Text = string.Empty;

        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedCar != null)
                {
                    // miramos si hay facturas asociadas a este coche

                    var invoicesWithCar = context.Invoices.Any(invoice => invoice.CarID == selectedCar.CarID);

                    if (invoicesWithCar)
                    {
                        MessageBox.Show("You cannot delete a car that has invoices. To delete this car, you" +
                            "will have to delete its invoices, but this will affect in the capital of the company", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        // no hay facturas asociadas, podemos eliminación el coche

                        var ordersToDelete = context.Orders.Where(order => order.CarID == selectedCar.CarID);
                        context.Orders.RemoveRange(ordersToDelete);

                        var appointmentsToDelete = context.Appointments.Where(appointment => appointment.CarID == selectedCar.CarID);
                        context.Appointments.RemoveRange(appointmentsToDelete);

                        context.Cars.Remove(selectedCar);
                        context.SaveChanges();

                        cars = context.Cars.ToList();
                        lbCars.ItemsSource = cars;

                        createForm.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    throw new ArgumentException("You must choose a car.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

    }
}
