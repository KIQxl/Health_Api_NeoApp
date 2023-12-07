using Entities.Dtos.DoctorDto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDoctorServices
    {
        public Task<Doctor> GetDoctorById(int id);
        public Task<List<DoctorView>> GetAllDoctorsView();
        public Task<DoctorView> GetDoctorViewById(int id);
        public Task<DoctorView> CreateDoctor(CreateDoctor request);
        public Task<DoctorView> UpdateDoctor(int id, UpdateDoctor request);
        public Task<bool> DeleteDoctor(int id);
    }
}
