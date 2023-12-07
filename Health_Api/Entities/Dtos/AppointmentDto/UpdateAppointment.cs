using Entities.Models;

namespace Entities.Dtos.AppointmentDto
{
    public struct UpdateAppointment
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
