using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MedicalBookingSystem.Repositories;
using MedicalBookingSystem.Services;
using MedicalBookingSystem.Interfaces;
using MedicalBookingSystem.Models;

namespace MedicalBookingSystem
{
    internal static class Program
    {


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var doctorRepo = new DoctorRepository();
            var patientRepo = new PatientRepository();
            var appointmentRepo = new AppointmentRepository(doctorRepo, patientRepo);


            // Налаштування Dependency Injection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Створення провайдера сервісів
            var serviceProvider = services.BuildServiceProvider();

            // Запуск головної форми
            Application.Run(serviceProvider.GetRequiredService<UserSelectionForm>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Реєстрація сервісів
             // JSON-сервіс для збереження даних

            // Реєстрація репозиторіїв
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Patient>, PatientRepository>();
            services.AddScoped<IRepository<Appointment>, AppointmentRepository>();

            // Реєстрація форм
            services.AddTransient<UserSelectionForm>();
            services.AddTransient<AddAppointmentForm>();

            services.AddTransient<DoctorLoginForm>();
            services.AddTransient<MainForm>();
           

        }
    }
}
