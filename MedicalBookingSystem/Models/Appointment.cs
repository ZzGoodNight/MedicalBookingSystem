using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MedicalBookingSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }  // Тепер це не ігнорується

        [JsonIgnore]  // Доктор не серіалізується, але його ID зберігається
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }  // Додано явне поле PatientId
        public Patient Patient { get; set; }

        public int PatientPhone { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
