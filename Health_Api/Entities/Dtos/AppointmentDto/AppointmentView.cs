using Entities.Dtos.DoctorDto;
using Entities.Dtos.PatientDto;

namespace Entities.Dtos.AppointmentDto
{
    public struct AppointmentView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientAppointmentView Patient { get; set; }
        public int DoctorId { get; set; }
        public DoctorAppointmentView Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
