using Entities.Models;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataSeeding
{
    public static class UserDataSeeder
    {

        public static async Task SeedUser(UserManager<User> userManager)
        {
            var user = new User()
            {
                Name = "Default User",
                UserName = "DefaultUser",
                PhoneNumber = "12345678910",
                Email = "defaultuser@example.com",
                Role = "Admin"
            };

            userManager.CreateAsync(user, "Default@123").Wait();
        }

        public static async Task SeedPatient(HealthApiContext _context)
        {
            var patient = new Patient()
            {
                Name = "Default Patient",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                CPF = "xxxxxxxxxxx",
                Email = "defaultPatient@example.com",
                Phone = "xxxxxxxxxxx",
                Address = "xxx xxxxxxxx xxxxx",
                PatientStatus = PersonStatus.Active
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public static async Task SeedDoctor(HealthApiContext _context)
        {
            var doctor = new Doctor()
            {
                Name = "Default Doctor",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                CPF = "xxxxxxxxxxx",
                Email = "defaultPatient@example.com",
                Phone = "xxxxxxxxxxx",
                Address = "xxx xxxxxxxx xxxxx",
                CRM = "xxxxxxxxxxxx",
                MedicalSpecialty = "Default Specialty",
                DoctorStatus = PersonStatus.Active
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
