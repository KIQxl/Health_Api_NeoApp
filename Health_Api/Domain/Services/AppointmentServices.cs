using AutoMapper;
using Dapper;
using Domain.Interfaces;
using Entities.Dtos.AppointmentDto;
using Entities.Dtos.DoctorDto;
using Entities.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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


        // Função para consultar todas as consultas médicas na base de dados retornando suas respectivas Views Dto
        public async Task<List<AppointmentView>> GetAllAppointmensView()
        {
            List<Appointment> appointments = await _context.Appointments.ToListAsync(); 

            return _mapper.Map<List<AppointmentView>>(appointments);
        }


        // Função para consultar uma consulta médica atráves do seu Id
        public async Task<Appointment> GetAppointmentById(int id)
        {
            Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id.Equals(id));

            return appointment;
        }


        // Função para consultar uma consulta médica atráves do seu Id na base de dados retornando sua respectiva View Dto
        public async Task<AppointmentView> GetAppointmentViewById(int id)
        {
            Appointment appointment = await GetAppointmentById(id);

            return _mapper.Map<AppointmentView>(appointment);
        }


        // Função para inserir uma consulta médica na base de dados retornando sua respectiva View Dto
        public async Task<AppointmentView> CreateAppointment(CreateAppointment request)
        {
            try
            {
                Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id.Equals(request.DoctorId));
                Patient patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id.Equals(request.PatientId));

                if (patient == null || doctor == null)
                {
                    throw new Exception("Médico e/ou paciente não encontrados");
                }

                IEnumerable<DoctorView> doctorsAvailabe = await GetAvailabeDoctorValidation(new MedicalAvailabilityQueryBySpecialty()
                {
                    AppointmentDate = request.AppointmentDate,
                    MedicalSpecialty = doctor.MedicalSpecialty
                });

                bool validationDoctorAvailabe = doctorsAvailabe.Any(x => x.Name == doctor.Name);
                bool validationDate = DateTime.Now < request.AppointmentDate;

                if (!validationDate || !validationDoctorAvailabe)
                {
                    throw new Exception("Horário inválido ou Médico indisponíveis");
                }

                if (patient != null && doctor != null)
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

                throw new Exception("Não foi possível confirmar a consulta, verifique se os dados informados estão corretos");
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Função para Alterar ou Atualizar uma consulta médica na base de dados retornando sua respectiva View Dto
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


        // Função para Deletar uma consulta médica na base de dados retornando um boolean
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


        // Função utilizando Dapper para retorno de médicos disponíveis a partir de sua especialidade e horários para validar a criação de um nova consulta onde os horários estejam livres
        public async Task<IEnumerable<DoctorView>> GetAvailabeDoctorValidation(MedicalAvailabilityQueryBySpecialty request)
        {

            using (var con = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                string formattedDate = request.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

                string sql =
                    @"SELECT *
                    FROM doctors d
                    WHERE 
                        d.id NOT IN (
                            SELECT a.doctorId
                            FROM Appointments a
                            WHERE a.AppointmentDate = @formattedDate AND a.Status = 1
                        )
                        AND d.MedicalSpecialty = @MedicalSpecialty ";

                var parameters = new { formattedDate, request.MedicalSpecialty };

                IEnumerable<DoctorView> doctors = await con.QueryAsync<DoctorView>(sql: sql, param: parameters);

                return doctors;
            }
        }
    }
}
