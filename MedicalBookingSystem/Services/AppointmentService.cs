using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Repositories;

namespace MedicalBookingSystem.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepo;
        private readonly DoctorRepository _doctorRepo;
        private readonly PatientRepository _patientRepo;

        public AppointmentService(AppointmentRepository appointmentRepo,
                                  DoctorRepository doctorRepo,
                                  PatientRepository patientRepo)
        {
            _appointmentRepo = appointmentRepo;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }
        public void ValidateEntity<T>(T entity)
        {
            var context = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, context, results, true);
            if (!isValid)
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
        public bool IsTimeSlotAvailable(DateTime time, int doctorId)
        {
            if (time < DateTime.Now) return false;
            if (time.Hour < 8 || time.Hour >= 20) return false;
            if (time.Minute != 0 && time.Minute != 30) return false;

            return !_appointmentRepo.GetAll().Any(a =>
                a.Doctor?.Id == doctorId &&
                a.Date.Date == time.Date &&
                Math.Abs((a.Date - time).TotalMinutes) < 30);
        }

        public DateTime GetNextAvailableTime(DateTime preferredTime, int doctorId)
        {
            DateTime checkTime = new DateTime(preferredTime.Year, preferredTime.Month, preferredTime.Day,
                                              preferredTime.Hour, (preferredTime.Minute / 30) * 30, 0);

            while (!IsTimeSlotAvailable(checkTime, doctorId))
            {
                checkTime = checkTime.AddMinutes(30);
                if (checkTime.Hour >= 20)
                    checkTime = checkTime.AddDays(1).Date.AddHours(8);
                else if (checkTime.Hour < 8)
                    checkTime = checkTime.Date.AddHours(8);
            }

            return checkTime;
        }

        public Patient CreatePatient(string name, string phone)
        {
            var patient = new Patient { Name = name.Trim(), Phone = phone.Trim() };
            ValidateEntity(patient);
            _patientRepo.Add(patient);
            return patient;
        }

        public Appointment CreateAppointment(int doctorId, Patient patient, DateTime time)
        {
            if (!IsTimeSlotAvailable(time, doctorId))
                throw new InvalidOperationException("Обраний час недоступний.");

            var doctor = _doctorRepo.GetById(doctorId);
            if (doctor == null)
                throw new Exception("Лікаря не знайдено");

            var appointment = new Appointment
            {
                Doctor = doctor,
                Patient = patient,
                PatientName = patient.Name,
                Date = time
            };

            _appointmentRepo.Add(appointment);
            return appointment;
        }
       
    }
}

