using Entities.Dtos.AppointmentDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PatientDto
{
    public struct PatientView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public PersonStatus PatientStatus { get; set; }
        public List<AppointmentView> Appointments { get; set; }
    }
}
