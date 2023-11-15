using System;
using System.Windows;
using System.Collections.Generic;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Linq;
using System.Text.RegularExpressions;
using Examen_Opdracht_.NET_ADVANCED.Panels;
using Examen_Opdracht_.NET_ADVANCED.Data;
using Examen_Opdracht_.NET_ADVANCED.Users;

namespace Examen_Opdracht_.NET_ADVANCED
{
    public partial class MainWindow : Window
    {
        private MyDBContext context;

        //private MyDBContext context = new MyDBContext();
        

        public MainWindow()
        {
            //Initializer.DbSetInitializer(context);

            InitializeComponent();


        }
        private void Car_Click(object sender, RoutedEventArgs e)
        {
            ViewCar viewCarWindow = new ViewCar();

            viewCarWindow.Show();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            ViewCustomer viewCustomerWindow = new ViewCustomer();

            viewCustomerWindow.Show();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            ViewOrder viewOrderWindow = new ViewOrder();

            viewOrderWindow.Show();
        }

        private void Capital_Click(object sender, RoutedEventArgs e)
        {
            ViewCapital viewCapitalWindow = new ViewCapital();

            viewCapitalWindow.Show();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            ViewInvoice viewInvoiceWindow = new ViewInvoice();

            viewInvoiceWindow.Show();
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            ViewAppointment viewAppointmentWindow = new ViewAppointment();

            viewAppointmentWindow.Show();
        }
       /* private void Administrators_Click(object sender, RoutedEventArgs e)
        {
            AdministratorsWindow administratorsWindow = new AdministratorsWindow();

            administratorsWindow.Show();
        }*/




    }
}