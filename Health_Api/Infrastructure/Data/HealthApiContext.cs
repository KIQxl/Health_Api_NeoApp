using Entities.Models;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class HealthApiContext : DbContext
    {
        public HealthApiContext(DbContextOptions<HealthApiContext> opts): base(opts)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseLazyLoadingProxies().UseMySql(GetConnectionString(), ServerVersion.AutoDetect(GetConnectionString()));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        }

        public string GetConnectionString()
        {
            return "server=localhost; database=HealthApi;user=root;password=password";
        }
    }
}
