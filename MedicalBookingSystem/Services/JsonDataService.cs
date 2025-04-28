using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using MedicalBookingSystem.Models;
using Newtonsoft.Json;

namespace MedicalBookingSystem.Services
{
    public class JsonDataService
    {
        private readonly string _filePath = "medical_data.json";

        public (List<Doctor> Doctors, List<Patient> Patients, List<Appointment> Appointments) LoadData()
        {
            if (!File.Exists(_filePath))
                return (new List<Doctor>(), new List<Patient>(), new List<Appointment>());

            string json = File.ReadAllText(_filePath);
            var data = JsonConvert.DeserializeObject<MedicalData>(json);

            return (data.Doctors, data.Patients, data.Appointments);
        }

        public void SaveData(List<Doctor> doctors, List<Patient> patients, List<Appointment> appointments)
        {
            var data = new MedicalData
            {
                Doctors = doctors,
                Patients = patients,
                Appointments = appointments
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        private class MedicalData
        {
            public List<Doctor> Doctors { get; set; }
            public List<Patient> Patients { get; set; }
            public List<Appointment> Appointments { get; set; }
        }
    }
}
