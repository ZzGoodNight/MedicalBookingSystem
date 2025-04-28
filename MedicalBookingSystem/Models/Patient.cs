using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalBookingSystem.Models
{
    public class Patient
    {
        private static readonly Random _random = new Random();

        public int Id { get; set; } = GeneratePatientId();
        public string Name { get; set; }
        public string Phone { get; set; }

        private static int GeneratePatientId()
        {
            return _random.Next(1000, 99999); // Генерує 4-5 цифр
        }

        public override string ToString() => $"{Name} (ID: {Id})";
    }
}
