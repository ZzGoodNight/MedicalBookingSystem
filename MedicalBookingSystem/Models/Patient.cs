using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MedicalBookingSystem.Models
{
    public class Patient
    {
        private static readonly Random _random = new Random();

        public int Id { get; set; } = GeneratePatientId();

        [Required(ErrorMessage = "Ім'я пацієнта обов'язкове")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ім'я пацієнта має бути від 2 до 100 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Телефон обов'язковий")]
        [Phone(ErrorMessage = "Некоректний формат телефону")]
        public string Phone { get; set; }

        private static int GeneratePatientId()
        {
            return _random.Next(1000, 99999); // Генерує 4-5 цифр
        }

        public override string ToString() => $"{Name} (ID: {Id})";
    }
}
