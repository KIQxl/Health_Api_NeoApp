using Entities.Dtos.AppointmentDto;
using Entities.Models;

namespace Domain.Interfaces
{
    public interface IAppointmentServices
    {
        public Task<Appointment> GetAppointmentById(int id);
        public Task<List<AppointmentView>> GetAllAppointmensView();
        public Task<AppointmentView> GetAppointmentViewById(int id);
        public Task<AppointmentView> CreateAppointment(CreateAppointment request);
        public Task<AppointmentView> UpdateAppointment(int id, UpdateAppointment request);
        public Task<bool> DeleteAppointment(int id);
    }
}
