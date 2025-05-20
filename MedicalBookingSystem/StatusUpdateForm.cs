using System;
using System.Windows.Forms;

namespace MedicalBookingSystem
{
    public partial class StatusUpdateForm : Form
    {
        public string SelectedStatus { get; private set; }

        public StatusUpdateForm(string currentStatus)
        {
            InitializeComponent();
            SetupForm(currentStatus);
        }

        private ComboBox comboStatus;

        private void SetupForm(string currentStatus)
        {
            this.Text = "Оновлення статусу";
            this.Size = new System.Drawing.Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;

            comboStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new System.Drawing.Point(30, 20),
                Width = 220
            };
            comboStatus.Items.AddRange(new string[] { "Очікується", "Завершено", "Скасовано" });
            comboStatus.SelectedItem = currentStatus;
            this.Controls.Add(comboStatus);

            var btnOK = new Button
            {
                Text = "OK",
                Location = new System.Drawing.Point(90, 60),
                Width = 100
            };
            btnOK.Click += (s, e) =>
            {
                SelectedStatus = comboStatus.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnOK);
        }
    }
}
