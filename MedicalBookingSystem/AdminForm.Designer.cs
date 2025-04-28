namespace MedicalBookingSystem
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnEditAppointment = new System.Windows.Forms.Button();
            this.btnEditDoctor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "button1";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 102);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "button2";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnEditAppointment
            // 
            this.btnEditAppointment.Location = new System.Drawing.Point(12, 151);
            this.btnEditAppointment.Name = "btnEditAppointment";
            this.btnEditAppointment.Size = new System.Drawing.Size(75, 23);
            this.btnEditAppointment.TabIndex = 3;
            this.btnEditAppointment.Text = "button3";
            this.btnEditAppointment.UseVisualStyleBackColor = true;
            // 
            // btnEditDoctor
            // 
            this.btnEditDoctor.Location = new System.Drawing.Point(12, 198);
            this.btnEditDoctor.Name = "btnEditDoctor";
            this.btnEditDoctor.Size = new System.Drawing.Size(75, 23);
            this.btnEditDoctor.TabIndex = 4;
            this.btnEditDoctor.Text = "button4";
            this.btnEditDoctor.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEditDoctor);
            this.Controls.Add(this.btnEditAppointment);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Name = "AdminForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnEditAppointment;
        private System.Windows.Forms.Button btnEditDoctor;
    }
}