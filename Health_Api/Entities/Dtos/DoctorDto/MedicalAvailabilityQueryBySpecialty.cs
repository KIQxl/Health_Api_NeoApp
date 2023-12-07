using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.DoctorDto
{
    public struct MedicalAvailabilityQueryBySpecialty
    {
        public DateTime AppointmentDate { get; set; }
        public string MedicalSpecialty { get; set; }
    }
}
