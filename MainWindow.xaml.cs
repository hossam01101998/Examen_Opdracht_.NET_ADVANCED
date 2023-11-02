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
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

/*public partial class MainWindow : Window
{
    List<Customer> customers = new List<Customer>();
    List<Order> orders = new List<Order>();

    public MainWindow()
    {
        InitializeComponent();
        // Binding customers to customerDataGrid
        customerDataGrid.ItemsSource = customers;
    }

    private void AddCustomer_Click(object sender, RoutedEventArgs e)
    {
        // Handle adding a new customer
        Customer newCustomer = new Customer();
        customers.Add(newCustomer);
        customerDataGrid.Items.Refresh();
    }

    private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
    {
        // Handle deleting a selected customer
        if (customerDataGrid.SelectedItem != null)
        {
            Customer selectedCustomer = (Customer)customerDataGrid.SelectedItem;
            customers.Remove(selectedCustomer);
            customerDataGrid.Items.Refresh();
        }
    }

    private void AddOrder_Click(object sender, RoutedEventArgs e)
    {
        // Handle adding a new order
        if (customerDataGrid.SelectedItem != null)
        {
            Order newOrder = new Order();
            Customer selectedCustomer = (Customer)customerDataGrid.SelectedItem;
            selectedCustomer.Orders.Add(newOrder);
            orderDataGrid.Items.Refresh();
        }
    }

    private void DeleteOrder_Click(object sender, RoutedEventArgs e)
    {
        // Handle deleting a selected order
        if (orderDataGrid.SelectedItem != null)
        {
            Order selectedOrder = (Order)orderDataGrid.SelectedItem;
            Customer selectedCustomer = (Customer)customerDataGrid.SelectedItem;
            selectedCustomer.Orders.Remove(selectedOrder);
            orderDataGrid.Items.Refresh();
        }
    }
}

private void CreateAppointmentButton_Click(object sender, RoutedEventArgs e)
{
    using (var context = new MyDBContext())
    {
        var customer = context.Customers.FirstOrDefault(c => c.Name == "Nombre del Cliente");
        var car = context.Cars.FirstOrDefault(); // Obtener una instancia de coche

        var newAppointment = new Appointment(DateTime.Now, "Servicio de prueba", "Programado");

        // Asigna el cliente y el coche utilizando las propiedades de navegación
        newAppointment.Customer = customer;
        newAppointment.Car = car;

        context.Appointments.Add(newAppointment);
        context.SaveChanges();
    }
}
private void CrearFacturaButton_Click(object sender, RoutedEventArgs e)
{
using (var context = new MyDBContext())
{
    var customer = context.Customers.FirstOrDefault(c => c.Name == "Nombre del Cliente");
    var car = context.Cars.FirstOrDefault(c => c.LicensePlate == "Matrícula del Coche");

    if (car != null)
    {
        var newInvoice = new Invoice(car, customer, DateTime.Now, 100.00m, "Detalles de la factura");

        context.Invoices.Add(newInvoice);
        context.SaveChanges();
    }
    else
    {
        MessageBox.Show("El coche no se encontró en la base de datos."); // Mostrar un mensaje de error
    }
}
}
 using (var context = new MyDBContext())
{
    var customer = context.Customers.FirstOrDefault(c => c.Name == "Nombre del Cliente");
    var car = context.Cars.FirstOrDefault(c => c.LicensePlate == "Matrícula del Coche");

    if (customer != null && car != null)
    {
        var newInvoice = new Invoice
        {
            Customer = customer,
            Car = car,
            IssueDate = DateTime.Now,
            TotalAmount = 100.00m,
            Details = "Detalles de la factura"
        };

        context.Invoices.Add(newInvoice);
        context.SaveChanges();
    }
    else
    {
        // Manejar el caso en el que no se encuentra el cliente o el coche
        // Por ejemplo, lanzar una excepción, mostrar un mensaje de error, etc.
    }
}
*/
}