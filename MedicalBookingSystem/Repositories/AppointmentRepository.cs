using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;
using Newtonsoft.Json;

namespace MedicalBookingSystem.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private List<Appointment> _appointments = new List<Appointment>();
        private readonly string _filePath;
        private int _nextId = 1;

        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;

        public AppointmentRepository(IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo)
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;

            var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataDirectory);
            _filePath = Path.Combine(dataDirectory, "appointments.json");

            LoadFromFile();
        }

        public IEnumerable<Appointment> GetAll() => _appointments;

        public Appointment GetById(int id) => _appointments.FirstOrDefault(a => a.Id == id);

        public void Add(Appointment item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            // Явно встановлюємо DoctorId та PatientId, якщо вони не задані
            if (item.Doctor != null && item.DoctorId == 0)
                item.DoctorId = item.Doctor.Id;

            if (item.Patient != null && item.PatientId == 0)
                item.PatientId = item.Patient.Id;

            item.Id = _nextId++;
            _appointments.Add(item);
            SaveToFile();
        }

        public void Update(Appointment item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existing = GetById(item.Id);
            if (existing != null)
            {
                existing.Date = item.Date;
                existing.Patient = item.Patient;
                existing.PatientId = item.PatientId;  // Додано оновлення PatientId
                existing.PatientName = item.Patient?.Name;
                existing.Doctor = item.Doctor;
                existing.DoctorId = item.DoctorId;    // Додано оновлення DoctorId
                existing.Status = item.Status;
                existing.PatientPhone = item.PatientPhone;
                SaveToFile();
            }
        }

        public void Delete(int id)
        {
            _appointments.RemoveAll(a => a.Id == id);
            SaveToFile();
        }

        public Appointment Find(Func<Appointment, bool> predicate)
        {
            return _appointments.FirstOrDefault(predicate);
        }

        private void SaveToFile()
        {
            try
            {
                var data = _appointments.Select(a => new
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,  
                    PatientId = a.PatientId, 
                    PatientName = a.PatientName,
                    PatientPhone = a.PatientPhone,
                    Date = a.Date,
                    Status = a.Status
                }).ToList();

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromFile()
        {
            _appointments = new List<Appointment>();

            if (!File.Exists(_filePath))
            {
                _nextId = 1;
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                var data = JsonConvert.DeserializeObject<List<AppointmentJson>>(json);

                if (data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        var doctor = _doctorRepo?.GetById(item.DoctorId);
                        var patient = _patientRepo?.GetById(item.PatientId);

                        _appointments.Add(new Appointment
                        {
                            Id = item.Id,
                            Doctor = doctor,
                            DoctorId = item.DoctorId,  // Тепер зберігається
                            Patient = patient,
                            PatientId = item.PatientId,  // Тепер зберігається
                            PatientName = item.PatientName,
                            PatientPhone = item.PatientPhone,
                            Date = item.Date,
                            Status = item.Status
                        });
                    }
                    _nextId = _appointments.Max(a => a.Id) + 1;
                }
                else
                {
                    _nextId = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _nextId = 1;
            }
        }

        private class AppointmentJson
        {
            public int Id { get; set; }
            public int DoctorId { get; set; }
            public int PatientId { get; set; }
            public string PatientName { get; set; }
            public int PatientPhone { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
        }
    }
}


