namespace MedicalBookingSystem
{
    partial class AddAppointmentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAppointmentForm));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGeneratedId = new System.Windows.Forms.Label();
            this.lblTimeStatus = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.comboDoctors = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // panelMain
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelMain.Controls.Add(this.panelForm);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(500, 400);
            this.panelMain.TabIndex = 0;

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(500, 60);
            this.panelHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(194, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Новий запис до лікаря";

            // panelForm
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.Controls.Add(this.label2);
            this.panelForm.Controls.Add(this.label1);
            this.panelForm.Controls.Add(this.btnCancel);
            this.panelForm.Controls.Add(this.lblGeneratedId);
            this.panelForm.Controls.Add(this.lblTimeStatus);
            this.panelForm.Controls.Add(this.txtPhone);
            this.panelForm.Controls.Add(this.txtName);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.dateTimePicker);
            this.panelForm.Controls.Add(this.comboDoctors);
            this.panelForm.Location = new System.Drawing.Point(20, 80);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(460, 300);
            this.panelForm.TabIndex = 1;

            // comboDoctors
            this.comboDoctors.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboDoctors.FormattingEnabled = true;
            this.comboDoctors.Location = new System.Drawing.Point(30, 40);
            this.comboDoctors.Name = "comboDoctors";
            this.comboDoctors.Size = new System.Drawing.Size(400, 25);
            this.comboDoctors.TabIndex = 1;

            // dateTimePicker
            this.dateTimePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateTimePicker.Location = new System.Drawing.Point(30, 80);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(400, 25);
            this.dateTimePicker.TabIndex = 2;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(30, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Записатись";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // txtName
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(30, 120);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(400, 25);
            this.txtName.TabIndex = 4;

            // txtPhone
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(30, 160);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(400, 25);
            this.txtPhone.TabIndex = 5;

            // lblTimeStatus
            this.lblTimeStatus.AutoSize = true;
            this.lblTimeStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeStatus.Location = new System.Drawing.Point(30, 200);
            this.lblTimeStatus.Name = "lblTimeStatus";
            this.lblTimeStatus.Size = new System.Drawing.Size(34, 15);
            this.lblTimeStatus.TabIndex = 6;
            this.lblTimeStatus.Text = "Час:";

            // lblGeneratedId
            this.lblGeneratedId.AutoSize = true;
            this.lblGeneratedId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGeneratedId.Location = new System.Drawing.Point(30, 220);
            this.lblGeneratedId.Name = "lblGeneratedId";
            this.lblGeneratedId.Size = new System.Drawing.Size(21, 15);
            this.lblGeneratedId.TabIndex = 7;
            this.lblGeneratedId.Text = "ID:";

            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(250, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 35);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(30, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ім'я пацієнта";

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(30, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Номер телефону";

            // AddAppointmentForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAppointmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запис до лікаря";
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.ComboBox comboDoctors;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblTimeStatus;
        private System.Windows.Forms.Label lblGeneratedId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}