using Entities.Dtos.AppointmentDto;
using Entities.Models;

namespace Entities.Dtos.DoctorDto
{
    public struct DoctorView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CRM { get; set; }
        public string MedicalSpecialty { get; set; }
        public PersonStatus DoctorStatus { get; set; }
        public List<AppointmentView> Appointments { get; set; }
    }
}
