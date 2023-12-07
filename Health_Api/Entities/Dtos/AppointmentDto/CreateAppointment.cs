using Entities.Models;

namespace Entities.Dtos.AppointmentDto
{
    public struct CreateAppointment
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
