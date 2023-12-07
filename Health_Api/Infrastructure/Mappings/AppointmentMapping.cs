using AutoMapper;
using Entities.Dtos.AppointmentDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class AppointmentMapping : Profile
    {
        public AppointmentMapping() 
        {
            CreateMap<CreateAppointment, Appointment>();
            CreateMap<UpdateAppointment, Appointment>();
            CreateMap<Appointment, AppointmentView>();
        }

    }
}
