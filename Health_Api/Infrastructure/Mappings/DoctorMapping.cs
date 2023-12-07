using AutoMapper;
using Entities.Dtos.DoctorDto;
using Entities.Dtos.PatientDto;
using Entities.Models;

namespace Infrastructure.Mappings
{
    public class DoctorMappingc : Profile
    {
        public DoctorMappingc() 
        {
            CreateMap<CreateDoctor, Doctor>();
            CreateMap<Doctor, DoctorView>();
            CreateMap<UpdateDoctor, Doctor>();
            CreateMap<Doctor, DoctorAppointmentView>();
        }
    }
}
