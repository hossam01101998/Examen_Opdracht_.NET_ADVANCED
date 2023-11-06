using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Examen_Opdracht_.NET_ADVANCED.Model;


namespace Examen_Opdracht_.NET_ADVANCED.Panels
{
    /// <summary>
    /// Lógica de interacción para ViewCustomer.xaml
    /// </summary>
    /// 
    
    public partial class ViewCustomer : Window
    {
        private List<Customer> customers = new List<Customer>();
        private Customer selectedCustomer = null;
        private MyDBContext context = new MyDBContext();
        public ViewCustomer()
        {
            InitializeComponent();
            customers = context.Customers.ToList();
            lbCustomers.ItemsSource = customers;
        }





        private void lbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            selectedCustomer = lbCustomers.SelectedItem as Customer;


            if (selectedCustomer != null)
            {


                txtCustomerName.Text = selectedCustomer.Name;
                txtEmail.Text = selectedCustomer.Email;
                txtAddress.Text = selectedCustomer.Adress;
                txtError.Text = string.Empty;

            }

        }

        private void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            txtCustomerName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            //spAddress.Visibility = Visibility.Hidden;
            txtError.Text = string.Empty;


        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string customerName = txtCustomerName.Text;
                string customerEmail = txtEmail.Text;
                string customerAddress = txtAddress.Text;

                if (string.IsNullOrWhiteSpace(customerName))
                {
                    throw new ArgumentException("The name cannot be empty.");
                }

                if (!string.IsNullOrWhiteSpace(customerEmail))
                {
                    if (!IsValidEmail(customerEmail))
                    {
                        throw new ArgumentException("The format of email is not correct.");
                    }
                }
                else
                {
                    throw new ArgumentException("The email cannot be empty.");
                }


                if (string.IsNullOrWhiteSpace(customerAddress))
                {
                    throw new ArgumentException("The direction cannot be empty.");
                }

                var newCustomer = new Customer
                {
                    Name = customerName,
                    Email = customerEmail,
                    Adress = customerAddress
                };

                context.Customers.Add(newCustomer);
                context.SaveChanges();

                customers = context.Customers.ToList();
                lbCustomers.ItemsSource = customers;

                createForm.Visibility = Visibility.Collapsed;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;

                //spAddress.Visibility = Visibility.Visible;
                txtError.Text = ex.Message;

            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }
    }
}