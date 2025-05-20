using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalBookingSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Ім'я обов’язкове")]
        
        public string Username { get; set; }

        [Required(ErrorMessage = "Ім'я обов’язкове")]
        public string Password { get; set; }

        // Для зручності відображення
       
    }
}

