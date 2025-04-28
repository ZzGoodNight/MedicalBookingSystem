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
    public partial class UserSelectionForm : Form
    {
        public UserSelectionForm()
        {
            InitializeComponent();

            // Налаштування форми
            this.Text = "Оберіть тип користувача";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Налаштування кнопок
            btnPatient.Text = "Пацієнт";
            btnAdmin.Text = "Адміністратор";

            // Підписка на події
            btnPatient.Click += BtnPatient_Click;
            btnAdmin.Click += BtnAdmin_Click;
        }

        private void BtnPatient_Click(object sender, EventArgs e)
        {
            // Відкриваємо головну форму для пацієнта
            this.Hide();
            var mainForm = new MainForm();
            mainForm.ShowDialog();
            this.Close();
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            using (var loginForm = new AdminLoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Успішний вхід - відкриваємо адмін-панель
                    var adminForm = new AdminLoginForm();
                    adminForm.ShowDialog();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
