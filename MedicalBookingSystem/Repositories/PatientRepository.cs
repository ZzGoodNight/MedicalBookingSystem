using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Interfaces;

namespace MedicalBookingSystem.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly string _filePath = Path.Combine("Data", "patients.json");

        private List<Patient> _patients;
        private int _nextId = 1;

        public PatientRepository()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _patients = JsonSerializer.Deserialize<List<Patient>>(json) ?? new List<Patient>();
            }
            else
            {
                _patients = new List<Patient>();
            }

            _nextId = _patients.Any() ? _patients.Max(p => p.Id) + 1 : 1;
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_patients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Patient> GetAll() => _patients;

        public Patient GetById(int id) => _patients.FirstOrDefault(p => p.Id == id);

        public void Add(Patient item)
        {
            item.Id = _nextId++;
            _patients.Add(item);
            SaveToFile();
        }

        public void Update(Patient item)
        {
            var existing = GetById(item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.Phone = item.Phone;
                SaveToFile();
            }
        }

        public void Delete(int id)
        {
            _patients.RemoveAll(p => p.Id == id);
            SaveToFile();
        }

        public Patient Find(Func<Patient, bool> predicate)
        {
            return _patients.FirstOrDefault(predicate);
        }
    }
}

