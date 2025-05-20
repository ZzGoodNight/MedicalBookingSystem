using System;
using System.Drawing;
using System.Windows.Forms;
using MedicalBookingSystem.Repositories;
using MedicalBookingSystem.Models;


namespace MedicalBookingSystem
{
    public partial class DoctorLoginForm : Form
    {
        private DoctorRepository _doctorRepo = new DoctorRepository();
        private PatientRepository _patientRepo = new PatientRepository();


        public DoctorLoginForm()
        {

            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Вхід для лікаря";
            this.ClientSize = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblUsername = new Label { Text = "Логін:", Location = new Point(50, 80) };
            var txtUsername = new TextBox { Location = new Point(150, 80), Width = 120 };

            var lblPassword = new Label { Text = "Пароль:", Location = new Point(50, 120) };
            var txtPassword = new TextBox { Location = new Point(150, 120), Width = 120, PasswordChar = '*' };

            var btnLogin = new Button
            {
                Text = "Увійти",
                Location = new Point(120, 170),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };

            btnLogin.Click += (s, e) =>
            {
                // Перевірка на пусті поля
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Будь ласка, введіть логін.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Будь ласка, введіть пароль.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Якщо все добре — перевіряємо логін і пароль
                var doctor = _doctorRepo.FindByCredentials(txtUsername.Text, txtPassword.Text);
                if (doctor != null)
                {
                    this.Hide();
                    new DoctorAppointmentsForm(doctor, _doctorRepo, _patientRepo).ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Невірний логін або пароль", "Помилка входу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };


            this.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, btnLogin });
        }
    }
}



