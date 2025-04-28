using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalBookingSystem.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly List<Appointment> _appointments = new List<Appointment>();

        public void Add(Appointment entity) => _appointments.Add(entity);

        public void Update(Appointment entity)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == entity.Id);
            if (existing != null)
            {
                existing.Doctor = entity.Doctor;
                existing.Patient = entity.Patient;
                existing.AppointmentTime = entity.AppointmentTime;  
            }
        }

        public void Delete(int id) => _appointments.RemoveAll(a => a.Id == id);

        public Appointment GetById(int id) => _appointments.FirstOrDefault(a => a.Id == id);

        public IEnumerable<Appointment> GetAll() => _appointments.OrderBy(a => a.AppointmentTime);

        public IEnumerable<Appointment> Find(Func<Appointment, bool> predicate) => _appointments.Where(predicate);
    }
}
