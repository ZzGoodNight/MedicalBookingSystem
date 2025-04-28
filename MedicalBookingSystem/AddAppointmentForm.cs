using System;
using System.Linq;
using System.Windows.Forms;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Interfaces;

namespace MedicalBookingSystem
{
    public partial class AddAppointmentForm : Form
    {
        public Appointment NewAppointment { get; private set; }
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IRepository<Appointment> _appointmentRepo;

        public AddAppointmentForm(IRepository<Doctor> doctorRepo,
                                IRepository<Patient> patientRepo,
                                IRepository<Appointment> appointmentRepo)
        {
            InitializeComponent();
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;

            LoadDoctors();
            SetupDateTimePicker();
            SetupControls();

            // Підписка на події
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
            lblGeneratedId.Text = "Ваш ID буде згенеровано автоматично";
            lblTimeStatus.Text = "Оберіть лікаря та час";
            lblTimeStatus.ForeColor = System.Drawing.Color.Blue;
        }

        private DateTime GetNextAvailableTime(DateTime preferredTime)
        {
            DateTime checkTime = new DateTime(preferredTime.Year, preferredTime.Month, preferredTime.Day,
                                            preferredTime.Hour, (preferredTime.Minute / 30) * 30, 0);

            if (comboDoctors.SelectedItem == null)
                return checkTime;

            var doctorId = ((dynamic)comboDoctors.SelectedItem).Id;

            while (!IsTimeSlotAvailable(checkTime, doctorId))
            {
                checkTime = checkTime.AddMinutes(30);

                if (checkTime.Hour >= 20)
                {
                    checkTime = checkTime.AddDays(1);
                    checkTime = new DateTime(checkTime.Year, checkTime.Month, checkTime.Day, 8, 0, 0);
                }
                else if (checkTime.Hour < 8)
                {
                    checkTime = new DateTime(checkTime.Year, checkTime.Month, checkTime.Day, 8, 0, 0);
                }
            }

            return checkTime;
        }

        private bool IsTimeSlotAvailable(DateTime time, int doctorId)
        {
            if (time < DateTime.Now)
                return false;

            if (time.Hour < 8 || time.Hour >= 20)
                return false;

            if (time.Minute != 0 && time.Minute != 30)
                return false;

            return !_appointmentRepo.GetAll()
                .Any(a => a.Doctor.Id == doctorId &&
                       a.AppointmentTime.Date == time.Date &&
                       Math.Abs((a.AppointmentTime - time).TotalMinutes) < 30);
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            var time = dateTimePicker.Value;
            if (time.Minute > 0 && time.Minute < 30)
            {
                dateTimePicker.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 30, 0);
            }
            else if (time.Minute > 30)
            {
                dateTimePicker.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour + 1, 0, 0);
            }

            UpdateTimeStatus();
        }

        private void UpdateTimeStatus()
        {
            if (comboDoctors.SelectedItem == null)
                return;

            var doctorId = ((dynamic)comboDoctors.SelectedItem).Id;
            var selectedTime = dateTimePicker.Value;

            if (selectedTime.Hour < 8 || selectedTime.Hour >= 20)
            {
                lblTimeStatus.Text = "Не робочий час (8:00-20:00)";
                lblTimeStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (!IsTimeSlotAvailable(selectedTime, doctorId))
            {
                lblTimeStatus.Text = "Час зайнятий! Найближчий вільний: " +
                                   GetNextAvailableTime(selectedTime).ToString("HH:mm");
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
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля пацієнта", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboDoctors.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть лікаря", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var doctorId = ((dynamic)comboDoctors.SelectedItem).Id;
            var selectedTime = dateTimePicker.Value;

            if (!IsTimeSlotAvailable(selectedTime, doctorId))
            {
                MessageBox.Show("Оберіть інший час - цей вже зайнятий", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Створення пацієнта (ID генерується автоматично)
            var patient = new Patient
            {
                Name = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };
            _patientRepo.Add(patient);

            // Створення запису
            NewAppointment = new Appointment
            {
                Doctor = _doctorRepo.GetById(doctorId),
                Patient = patient,
                AppointmentTime = selectedTime
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

      
    }
}