using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalBookingSystem.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        public void Add(Doctor entity) => _doctors.Add(entity);

        public void Update(Doctor entity)
        {
            var existing = _doctors.FirstOrDefault(d => d.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Specialization = entity.Specialization;
            }
        }

        public void Delete(int id) => _doctors.RemoveAll(d => d.Id == id);

        public Doctor GetById(int id) => _doctors.FirstOrDefault(d => d.Id == id);

        public IEnumerable<Doctor> GetAll() => _doctors.OrderBy(d => d.Id);

        public IEnumerable<Doctor> Find(Func<Doctor, bool> predicate) => _doctors.Where(predicate);
    }
}

