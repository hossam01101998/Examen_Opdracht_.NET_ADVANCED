using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;


namespace Examen_Opdracht_.NET_ADVANCED.Model
{
    internal class Appointment
    {
        public int AppointmentId { get; set; }


        //[ForeignKey("Car")]
        public int CarID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string RequiredService { get; set; }
        public string Status { get; set; }
        
        public Car Car { get; set; }


        public Appointment()
        {
        }

        public Appointment(DateTime appointmentDate, Car car, string requiredService, string status)
        {
            AppointmentId = GenerateUniqueId();
            AppointmentDate = appointmentDate;
            Car = car;
            RequiredService = requiredService;
            Status = status;
        }


        private static int nextAppointmentId = 1;

        private int GenerateUniqueId()
        {
            return nextAppointmentId++;
        }
    }
}
