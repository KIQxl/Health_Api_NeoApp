using Entities.Models;

namespace Entities.Dtos.DoctorDto
{
    public struct DoctorAppointmentView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CRM { get; set; }
        public string MedicalSpecialty { get; set; }
        public PersonStatus DoctorStatus { get; set; }
    }
}
