using AutoMapper;
using Entities.Dtos.PatientDto;
using Entities.Models;

namespace Infrastructure.Mappings
{
    public class PatientMapping : Profile
    {
        public PatientMapping()
        {
            CreateMap<CreatePatient, Patient>();
            CreateMap<Patient, PatientView>();
        }
    }
}
