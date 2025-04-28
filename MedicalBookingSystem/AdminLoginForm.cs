using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalBookingSystem
{
    public partial class AdminLoginForm : Form
    {
        // Правильні облікові дані (можна підключити до БД пізніше)
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123";

        public AdminLoginForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Налаштування основної форми
            this.Text = "Вхід для адміністратора";
            this.ClientSize = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // Заголовок
            var lblTitle = new Label
            {
                Text = "Вхід в систему",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Поле логіна
            var lblUsername = new Label
            {
                Text = "Логін:",
                Location = new Point(50, 80),
                Size = new Size(100, 20)
            };

            var txtUsername = new TextBox
            {
                Location = new Point(150, 80),
                Size = new Size(150, 25),
                TabIndex = 0
            };

            // Поле пароля
            var lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 120),
                Size = new Size(100, 20)
            };

            var txtPassword = new TextBox
            {
                Location = new Point(150, 120),
                Size = new Size(150, 25),
                PasswordChar = '*',
                TabIndex = 1
            };

            // Кнопка входу
            var btnLogin = new Button
            {
                Text = "Увійти",
                Location = new Point(120, 170),
                Size = new Size(100, 35),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                TabIndex = 2
            };

            btnLogin.Click += (sender, e) => AttemptLogin(txtUsername.Text, txtPassword.Text);

            // Додавання елементів на форму
            this.Controls.AddRange(new Control[] {
                lblTitle, lblUsername, txtUsername,
                lblPassword, txtPassword, btnLogin
            });

            // Обробка натискання Enter
            this.AcceptButton = btnLogin;
        }

        private void AttemptLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username == ADMIN_USERNAME && password == ADMIN_PASSWORD)
            {
                // Ховаємо цю форму (або закриваємо, якщо потрібно)
                this.Hide();

                // Відкриваємо форму адміністратора
                var adminForm = new AdminForm();
                adminForm.ShowDialog();

                // Після закриття adminForm — закриваємо і цю
                this.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль", "Помилка входу",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

