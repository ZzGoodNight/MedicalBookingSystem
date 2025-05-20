using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Repositories;
using MedicalBookingSystem.Interfaces;

namespace MedicalBookingSystem
{
    public partial class DoctorAppointmentsForm : Form
    {
        private readonly Doctor _currentDoctor;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly AppointmentRepository _appointmentRepository;
        private List<Appointment> _appointments;
        private DataGridView dgvAppointments;


        public DoctorAppointmentsForm(Doctor doctor, IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo)
        {
            _currentDoctor = doctor;
            _doctorRepo = doctorRepo;
            _appointmentRepository = new AppointmentRepository(doctorRepo, patientRepo);

            InitializeComponent();
            LoadAppointments();
            SetupForm();
        }

        private void LoadAppointments()
        {
            _appointments = _appointmentRepository.GetAll()
                .Where(a => a.Doctor != null && a.Doctor.Id == _currentDoctor.Id)
                .ToList();
            foreach (var appt in _appointments)
            {
                if (string.IsNullOrWhiteSpace(appt.PatientName) && appt.Patient != null)
                {
                    appt.PatientName = appt.Patient.Name;
                }
            }
        }

        private void SetupForm()
        {
            this.Text = $"Прийоми лікаря {_currentDoctor.Name}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            dgvAppointments = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = false,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                DataSource = _appointments
            };

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                ReadOnly = true
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PatientName",
                HeaderText = "Пацієнт",
                ReadOnly = true
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "Дата",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });

            dgvAppointments.Columns.Add(new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Статус",
                DataSource = new string[] { "Очікується", "Завершено", "Скасовано" }
            });

            var btnSave = new Button
            {
                Text = "Зберегти зміни",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };

            btnSave.Click += SaveChanges;

            this.Controls.Add(dgvAppointments);
            this.Controls.Add(btnSave);
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            foreach (var appointment in _appointments)
            {
                _appointmentRepository.Update(appointment);
            }

            MessageBox.Show("Зміни збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


