using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace MedicalBookingSystem.Services
{
    public class AppointmentManager : ICRUDOperations<Appointment>, IFilterable<Appointment>
    {
        private List<Appointment> _appointments = new List<Appointment>();

        public void Add(Appointment appointment)
        {
            appointment.Id = _appointments.Count + 1; // Автогенерація ID
            _appointments.Add(appointment);
        }

        public void Update(Appointment appointment)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existing != null)
            {
                existing.Doctor = appointment.Doctor;
                existing.Patient = appointment.Patient;
                existing.AppointmentTime = appointment.AppointmentTime;
            }
        }

        public void Delete(int id)
        {
            _appointments.RemoveAll(a => a.Id == id);
        }

        public Appointment GetById(int id)
        {
            return _appointments.FirstOrDefault(a => a.Id == id);
        }

        public List<Appointment> GetAll()
        {
            return _appointments;
        }

        // Реалізація IFilterable
        public List<Appointment> Filter(string keyword)
        {
            return _appointments
                .Where(a => a.Doctor.Name.Contains(keyword) ||
                             a.Patient.Name.Contains(keyword))
                .ToList();
        }
    }
}
