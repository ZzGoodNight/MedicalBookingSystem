using System;
using System.Linq;
using System.Windows.Forms;
using MedicalBookingSystem.Models;
using MedicalBookingSystem.Repositories;
using MedicalBookingSystem.Interfaces;


namespace MedicalBookingSystem
{
    public partial class MainForm : Form
    {
        private readonly IRepository<Appointment> _appointmentRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;

        public MainForm()
        {
            InitializeComponent();

            // Ініціалізація репозиторіїв
            _appointmentRepo = new AppointmentRepository(_doctorRepo, _patientRepo);
            _doctorRepo = new DoctorRepository();
            _patientRepo = new PatientRepository();

            // Завантаження тестових даних
            LoadSampleData();

            // Оновлення інтерфейсу
            RefreshDataGrid();

            // Налаштування DataGridView
            ConfigureDataGridView();
        }

        private void LoadSampleData()
        {
            if (!_doctorRepo.GetAll().Any())
            {
                _doctorRepo.Add(new Doctor { Id = 1, Name = "Доктор Іванов", Specialization = "Терапевт" });
                _doctorRepo.Add(new Doctor { Id = 2, Name = "Доктор Петрова", Specialization = "Хірург" });
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ID_Запису",
                HeaderText = "ID запису",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Лікар",
                HeaderText = "Лікар",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Пацієнт",
                HeaderText = "Пацієнт",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Телефон",
                HeaderText = "Телефон",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ID_Пацієнта",
                HeaderText = "ID пацієнта",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Дата_та_час",
                HeaderText = "Дата та час",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Статус",
                HeaderText = "Статус",
                Width = 100
            });
        }

        private void RefreshDataGrid()
        {
            try
            {
                dataGridView1.DataSource = _appointmentRepo.GetAll()
                    .OrderBy(a => a.Date)
                    .Select(a => new
                    {
                        ID_Запису = a.Id,
                        Лікар = a.Doctor?.Name ?? "Лікар не вказано",
                        Пацієнт = a.Patient?.Name ?? "Пацієнт не вказано",
                        Телефон = a.Patient?.Phone ?? "Н/Д",
                        ID_Пацієнта = a.Patient?.Id ?? 0,
                        Дата_та_час = a.Date.ToString("dd.MM.yyyy HH:mm"),
                        Статус = a.Date > DateTime.Now ? "Заплановано" : "Завершено"
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні даних: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddAppointmentForm(_doctorRepo, _patientRepo, _appointmentRepo);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                
                RefreshDataGrid();
                MessageBox.Show($"Запис створено! ID пацієнта: {addForm.NewAppointment.Id}",
                              "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /*private void btnFindById_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPatientId.Text, out int patientId))
            {
                var appointments = _appointmentRepo.Find(a => a.Id == patientId);

                if (appointments.Any())
                {
                    dataGridView1.DataSource = appointments
                        .OrderBy(a => a.AppointmentTime)
                        .Select(a => new
                        {
                            ID_Запису = a.Id,
                            Лікар = a.Doctor.Name,
                            Пацієнт = a.Patient.Name,
                            Дата_та_час = a.AppointmentTime.ToString("dd.MM.yyyy HH:mm"),
                            Статус = a.AppointmentTime > DateTime.Now ? "Заплановано" : "Завершено"
                        })
                        .ToList();
                }
                else
                {
                    MessageBox.Show("Записів для цього ID не знайдено", "Інформація",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний ID пацієнта (4-5 цифр)", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/


        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtPatientId.Clear();
            RefreshDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Будь ласка, оберіть запис для видалення", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedId = (int)dataGridView1.SelectedRows[0].Cells["ID_Запису"].Value;

            if (MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Підтвердження",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _appointmentRepo.Delete(selectedId);
                RefreshDataGrid();
                MessageBox.Show("Запис успішно видалено", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

      
    }
}
