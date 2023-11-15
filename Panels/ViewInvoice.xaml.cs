using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED.Panels
{
    public partial class ViewInvoice : Window
    {
        private List<Invoice> invoices = new List<Invoice>();
        private MyDBContext context = new MyDBContext();
        private Invoice selectedInvoice = null;

        public ViewInvoice()
        {
            InitializeComponent();
            invoices = context.Invoices.ToList();
            lbInvoices.ItemsSource = invoices;

            cmbCustomer.ItemsSource = context.Customers.ToList();
        }

        private void lbInvoices_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            selectedInvoice = lbInvoices.SelectedItem as Invoice;

            if (selectedInvoice != null)
            {
                dpInvoiceDate.SelectedDate = selectedInvoice.IssueDate;
                txtInvoiceAmount.Text = selectedInvoice.TotalAmount.ToString();
                txtInvoiceDetails.Text = selectedInvoice.Details;
                

                if (selectedInvoice.CustomerID != null)
                {
                    var associatedCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == selectedInvoice.CustomerID);

                    if (associatedCustomer != null)
                    {
                        cmbCustomer.SelectedItem = associatedCustomer;

                        LoadCarsForCustomer(associatedCustomer.CustomerId);

                        var associatedCar = context.Cars.FirstOrDefault(c => c.CarID == selectedInvoice.CarID);

                        cmbCar.SelectedItem = associatedCar;
                    }
                }

                txtError.Text = string.Empty;
            }
        }


        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;
            selectedInvoice = null;

            dpInvoiceDate.SelectedDate = DateTime.Now;
            txtInvoiceAmount.Text = string.Empty;
            txtInvoiceDetails.Text = string.Empty;
            cmbCustomer.SelectedItem = null;
            cmbCar.SelectedItem = null; // Limpiar ComboBox de coches
            txtError.Text = string.Empty;
        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime issueDate = dpInvoiceDate.SelectedDate ?? DateTime.Now;
                decimal totalAmount;

                if (!decimal.TryParse(txtInvoiceAmount.Text, out totalAmount))
                {
                    throw new ArgumentException("Amount must be a valid decimal value.");
                }

                string details = txtInvoiceDetails.Text;

                if (string.IsNullOrWhiteSpace(details))
                {
                    throw new ArgumentException("Details cannot be empty.");
                }

                int customerID;

                if (cmbCustomer.SelectedValue != null && int.TryParse(cmbCustomer.SelectedValue.ToString(), out customerID))
                {
                    int carID;
                    if (cmbCar.SelectedValue != null && int.TryParse(cmbCar.SelectedValue.ToString(), out carID))
                    {
                        if (selectedInvoice != null)
                        {
                            selectedInvoice.IssueDate = issueDate;
                            selectedInvoice.TotalAmount = totalAmount;
                            selectedInvoice.Details = details;
                            selectedInvoice.CustomerID = customerID;
                            selectedInvoice.CarID = carID;

                        }
                        else
                        {
                            var newInvoice = new Invoice
                            {
                                IssueDate = issueDate,
                                TotalAmount = totalAmount,
                                Details = details,
                                CustomerID = customerID,
                                CarID = carID
                            };

                            context.Invoices.Add(newInvoice);
                        }

                        context.SaveChanges();

                        invoices = context.Invoices.ToList();
                        lbInvoices.ItemsSource = invoices;

                        createForm.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        throw new ArgumentException("Choose a car.");
                    }
                }
                else
                {
                    throw new ArgumentException("Choose a customer.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedInvoice != null)
                {
                    context.Invoices.Remove(selectedInvoice);
                    context.SaveChanges();

                    invoices = context.Invoices.ToList();
                    lbInvoices.ItemsSource = invoices;

                    createForm.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("You must select an invoice.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void LoadCarsForCustomer(int customerId)
        {
            var carsForCustomer = context.Cars.Where(c => c.CustomerId == customerId).ToList();
            cmbCar.ItemsSource = carsForCustomer;
            cmbCar.DisplayMemberPath = "LicensePlate";
        }
        private void cmbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = cmbCustomer.SelectedItem as Customer;

            if (selectedCustomer != null)
            {
                var carsForCustomer = context.Cars.Where(c => c.CustomerId == selectedCustomer.CustomerId).ToList();

                cmbCar.ItemsSource = carsForCustomer;
                cmbCar.DisplayMemberPath = "LicensePlate";

                cmbCar.Visibility = Visibility.Visible;
            }
            else
            {
                cmbCar.Visibility = Visibility.Collapsed;
            }
        }

    }

}
