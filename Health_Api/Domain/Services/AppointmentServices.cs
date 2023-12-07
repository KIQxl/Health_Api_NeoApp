using AutoMapper;
using Domain.Interfaces;
using Entities.Dtos.AppointmentDto;
using Entities.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        public readonly HealthApiContext _context;
        public readonly IMapper _mapper;

        public AppointmentServices(HealthApiContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        public async Task<List<AppointmentView>> GetAllAppointmensView()
        {
            List<Appointment> appointments = await _context.Appointments.ToListAsync(); 

            return _mapper.Map<List<AppointmentView>>(appointments);
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id.Equals(id));

            return appointment;
        }

        public async Task<AppointmentView> GetAppointmentViewById(int id)
        {
            Appointment appointment = await GetAppointmentById(id);

            return _mapper.Map<AppointmentView>(appointment);
        }

        public async Task<AppointmentView> CreateAppointment(CreateAppointment request)
        {
            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id.Equals(request.DoctorId));
            Patient patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id.Equals(request.PatientId));

            if(patient != null && doctor != null)
            {
                Appointment appointment = new Appointment()
                {
                    DoctorId = doctor.Id,
                    PatientId = patient.Id,
                    AppointmentDate = request.AppointmentDate,
                    Status = AppointmentStatus.Scheduled
                };

                await _context.AddAsync(appointment);
                await _context.SaveChangesAsync();

                return _mapper.Map<AppointmentView>(appointment);
            }

            throw new Exception("Não foi possível confirmar a consulta, médico e/ou paciente não encontrados");
        }

        public async Task<AppointmentView> UpdateAppointment(int id, UpdateAppointment request)
        {
            Appointment appointment = await GetAppointmentById(id);

            if(appointment != null)
            {
                appointment.AppointmentDate = request.AppointmentDate;
                appointment.DoctorId = request.DoctorId;
                appointment.Status = request.Status;

                await _context.SaveChangesAsync();

                return _mapper.Map<AppointmentView>(appointment);
            }

            throw new Exception("Consulta não encontrada");
        }
        public async Task<bool> DeleteAppointment(int id)
        {
            Appointment appointment = await GetAppointmentById(id);

            if(appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
