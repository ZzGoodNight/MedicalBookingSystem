using System;
using System.Linq;
using System.Windows.Forms;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Services;
using MedicalBookingSystem.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MedicalBookingSystem
{
    public partial class AddAppointmentForm : Form
    {
        public Appointment NewAppointment { get; private set; }
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<Appointment> _appointmentRepo;
        private readonly AppointmentService _appointmentService;

        public AddAppointmentForm(IRepository<Doctor> doctorRepo,
                                  IRepository<Patient> patientRepo,
                                  IRepository<Appointment> appointmentRepo)
        {
            InitializeComponent();
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;
            _appointmentService = new AppointmentService(
                (AppointmentRepository)_appointmentRepo,
                (DoctorRepository)_doctorRepo,
                (PatientRepository)_patientRepo
            );

            LoadDoctors();
            SetupDateTimePicker();
            SetupControls();

            comboDoctors.SelectedIndexChanged += (s, e) => UpdateTimeStatus();
            dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
        }

        private void LoadDoctors()
        {
            comboDoctors.DataSource = _doctorRepo.GetAll()
                .Select(d => new
                {
                    d.Id,
                    DisplayText = $"{d.Name} ({d.Specialization})"
                })
                .ToList();

            comboDoctors.DisplayMember = "DisplayText";
            comboDoctors.ValueMember = "Id";
        }

        private void SetupDateTimePicker()
        {
            dateTimePicker.MinDate = DateTime.Now;
            dateTimePicker.MaxDate = DateTime.Now.AddMonths(3);
            dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.Value = GetNextAvailableTime(DateTime.Now);
        }

        private void SetupControls()
        {
            lblGeneratedId.Text = "ID згенерується автоматично";
            lblTimeStatus.Text = "Оберіть лікаря та час";
            lblTimeStatus.ForeColor = System.Drawing.Color.Blue;
        }

        private DateTime GetNextAvailableTime(DateTime preferredTime)
        {
            if (comboDoctors.SelectedItem == null)
                return preferredTime;

            int doctorId = ((dynamic)comboDoctors.SelectedItem).Id;
            return _appointmentService.GetNextAvailableTime(preferredTime, doctorId);
        }

        private bool IsTimeSlotAvailable(DateTime time, int doctorId)
        {
            return _appointmentService.IsTimeSlotAvailable(time, doctorId);
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            var time = dateTimePicker.Value;

            if (time.Minute > 0 && time.Minute < 30)
                dateTimePicker.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 30, 0);
            else if (time.Minute > 30)
                dateTimePicker.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour + 1, 0, 0);

            UpdateTimeStatus();
        }

        private void UpdateTimeStatus()
        {
            if (comboDoctors.SelectedItem == null) return;

            var doctorId = ((dynamic)comboDoctors.SelectedItem).Id;
            var selectedTime = dateTimePicker.Value;

            if (selectedTime.Hour < 8 || selectedTime.Hour >= 20)
            {
                lblTimeStatus.Text = "Неробочий час (8:00–20:00)";
                lblTimeStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (!IsTimeSlotAvailable(selectedTime, doctorId))
            {
                lblTimeStatus.Text = $"Час зайнятий! Найближчий: {GetNextAvailableTime(selectedTime):HH:mm}";
                lblTimeStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblTimeStatus.Text = "Час вільний!";
                lblTimeStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboDoctors.SelectedItem == null)
            {
                MessageBox.Show("Оберіть лікаря", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var doctorId = ((dynamic)comboDoctors.SelectedItem).Id;
            var selectedTime = dateTimePicker.Value;

            try
            {
                if (!_appointmentService.IsTimeSlotAvailable(selectedTime, doctorId))
                {
                    MessageBox.Show("Цей час зайнятий. Оберіть інший.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var patient = _appointmentService.CreatePatient(txtName.Text.Trim(), txtPhone.Text.Trim());
                NewAppointment = _appointmentService.CreateAppointment(doctorId, patient, selectedTime);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show($"Помилка валідації:\n{vex.Message}", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}


