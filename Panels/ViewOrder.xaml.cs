using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED.Panels
{
    public partial class ViewOrder : Window
    {
        private List<Order> orders = new List<Order>();
        private Order selectedOrder = null;
        private MyDBContext context = new MyDBContext();

        public ViewOrder()
        {
            InitializeComponent();
            orders = context.Orders.ToList();
            lbOrders.ItemsSource = orders;

            cmbCars.ItemsSource = context.Cars.ToList();
        }

        private void lbOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            selectedOrder = lbOrders.SelectedItem as Order;

            if (selectedOrder != null)
            {
                txtOrderDetails.Text = selectedOrder.OrderDetails;

                if (selectedOrder.CarID != null)
                {
                    var associatedCar = context.Cars.FirstOrDefault(c => c.CarID == selectedOrder.CarID);

                    if (associatedCar != null)
                    {
                        cmbCars.SelectedItem = associatedCar;
                    }
                }

                txtError.Text = string.Empty;
            }
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;
            selectedOrder = null;

            txtOrderDetails.Text = string.Empty;
            cmbCars.SelectedItem = null;
            txtError.Text = string.Empty;
        }
        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string orderDetails = txtOrderDetails.Text;
                int carID;

                if (string.IsNullOrWhiteSpace(orderDetails))
                {
                    throw new ArgumentException("Order details cannot be empty.");
                }

                if (cmbCars.SelectedValue != null && int.TryParse(cmbCars.SelectedValue.ToString(), out carID))
                {
                    if (selectedOrder != null)
                    {
                        selectedOrder.OrderDetails = orderDetails;
                        selectedOrder.CarID = carID;
                    }
                    else
                    {
                        var newOrder = new Order
                        {
                            OrderDetails = orderDetails,
                            CarID = carID
                        };

                        context.Orders.Add(newOrder);
                    }

                    context.SaveChanges();

                    orders = context.Orders.ToList();
                    lbOrders.ItemsSource = orders;

                    createForm.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("Choose a car");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }


        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedOrder != null)
                {
                    context.Orders.Remove(selectedOrder);
                    context.SaveChanges();

                    orders = context.Orders.ToList();
                    lbOrders.ItemsSource = orders;

                    createForm.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("You must select an order.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }
    }
}
