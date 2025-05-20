using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Helpers;

namespace MedicalBookingSystem.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly string _filePath = Path.Combine("Data", "doctors.json");

        private List<Doctor> _doctors;
        private int _nextId = 1;

        public DoctorRepository()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _doctors = JsonSerializer.Deserialize<List<Doctor>>(json) ?? new List<Doctor>();
            }
            else
            {
                _doctors = new List<Doctor>(); // ← важливо!
            }

            _nextId = _doctors.Any() ? _doctors.Max(d => d.Id) + 1 : 1;
        }

        private void LoadFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _doctors = JsonSerializer.Deserialize<List<Doctor>>(json) ?? new List<Doctor>();
            }
            else
            {
                _doctors = new List<Doctor>();
            }
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_doctors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Doctor> GetAll() => _doctors;

        public Doctor GetById(int id) => _doctors.FirstOrDefault(d => d.Id == id);

        public void Add(Doctor item)
        {
            item.Id = _nextId++;
            _doctors.Add(item);
            SaveToFile();
        }

        public void Update(Doctor item)
        {
            var existing = GetById(item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.Specialization = item.Specialization;
                existing.Username = item.Username;
                existing.Password = item.Password;
                SaveToFile();
            }
        }

        public void Delete(int id)
        {
            _doctors.RemoveAll(d => d.Id == id);
            SaveToFile();
        }

        public Doctor Find(Func<Doctor, bool> predicate)
        {
            return _doctors.FirstOrDefault(predicate);
        }

        public Doctor FindByCredentials(string username, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);

            return _doctors.FirstOrDefault(d =>
                d.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                d.Password == hashedPassword);
        }
    }
}


