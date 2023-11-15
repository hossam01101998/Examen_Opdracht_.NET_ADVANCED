using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED.Panels
{
    public partial class ViewAppointment : Window
    {
        private List<Appointment> appointments = new List<Appointment>();
        private Appointment selectedAppointment = null;

        private MyDBContext context = new MyDBContext();
        public List<string> StatusList { get; } = new List<string> { "Pending", "Confirmed", "Cancelled" };

        public ViewAppointment()
        {
            InitializeComponent();
            appointments = context.Appointments.ToList();
            cmbStatus.ItemsSource = StatusList;

            // establece la propiedad DataContext de la ventana en esta instancia de ViewAppointment

            DataContext = this;

            lbAppointments.ItemsSource = appointments;
            cmbCars.ItemsSource = context.Cars.ToList();
        }

        private void lbAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;

            selectedAppointment = lbAppointments.SelectedItem as Appointment;

            if (selectedAppointment != null)
            {
                dpAppointmentDate.SelectedDate = selectedAppointment.AppointmentDate;
                txtRequiredService.Text = selectedAppointment.RequiredService;
                cmbStatus.SelectedItem = selectedAppointment.Status;

                if (selectedAppointment.CarID != null)
                {
                    var associatedCar = context.Cars.FirstOrDefault(c => c.CarID == selectedAppointment.CarID);
                    if (associatedCar != null)
                    {
                        cmbCars.SelectedItem = associatedCar;
                    }
                }

                txtError.Text = string.Empty;
            }
        }

        private void btnCreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            createForm.Visibility = Visibility.Visible;
            selectedAppointment = null;

            dpAppointmentDate.SelectedDate = DateTime.Now;
            txtRequiredService.Text = string.Empty;
            cmbStatus.SelectedItem = null;
            cmbCars.SelectedItem = null;  
            txtError.Text = string.Empty;
        }

        private void btnSaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime appointmentDate = dpAppointmentDate.SelectedDate ?? DateTime.Now;
                string requiredService = txtRequiredService.Text;
                string status = cmbStatus.SelectedItem as string;
                int carID;

                if (string.IsNullOrWhiteSpace(requiredService))
                {
                    throw new ArgumentException("The required service cannot be empty.");
                }

                if (cmbStatus.SelectedItem == null)
                {
                    throw new ArgumentException("Select a status for the appointment.");
                }

                if (cmbCars.SelectedValue != null && int.TryParse(cmbCars.SelectedValue.ToString(), out carID))
                {
                    
                }
                else
                {
                    throw new ArgumentException("Choose a car.");
                }

                if (selectedAppointment != null)
                {
                    selectedAppointment.AppointmentDate = appointmentDate;
                    selectedAppointment.RequiredService = requiredService;
                    selectedAppointment.Status = status;
                    selectedAppointment.CarID = carID;
                }
                else
                {
                    var newAppointment = new Appointment
                    {
                        AppointmentDate = appointmentDate,
                        RequiredService = requiredService,
                        Status = status,
                        CarID = carID
                    };

                    context.Appointments.Add(newAppointment);
                }

                context.SaveChanges();

                appointments = context.Appointments.ToList();
                lbAppointments.ItemsSource = appointments;

                createForm.Visibility = Visibility.Collapsed;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void btnDeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedAppointment != null)
                {
                    context.Appointments.Remove(selectedAppointment);
                    context.SaveChanges();

                    appointments = context.Appointments.ToList();
                    lbAppointments.ItemsSource = appointments;

                    createForm.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("You must select an appointment.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }
    }
}
