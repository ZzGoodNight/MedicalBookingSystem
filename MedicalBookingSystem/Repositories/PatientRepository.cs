using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalBookingSystem.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly List<Patient> _patients = new List<Patient>();

        public void Add(Patient entity) => _patients.Add(entity);

        public void Update(Patient entity)
        {
            var existing = _patients.FirstOrDefault(p => p.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Phone = entity.Phone;
            }
        }

        public void Delete(int id) => _patients.RemoveAll(p => p.Id == id);

        public Patient GetById(int id) => _patients.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Patient> GetAll() => _patients.OrderBy(p => p.Id);

        public IEnumerable<Patient> Find(Func<Patient, bool> predicate) => _patients.Where(predicate);
    }
}