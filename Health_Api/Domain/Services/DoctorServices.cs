using AutoMapper;
using Castle.Core.Configuration;
using Dapper;
using Domain.Interfaces;
using Entities.Dtos.DoctorDto;
using Entities.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Domain.Services
{
    public class DoctorServices : IDoctorServices
    {
        public readonly HealthApiContext _context;
        public readonly IMapper _mapper;
       
        public DoctorServices(HealthApiContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        // Função utilizando Dapper onde é consultado se um médico de determinada especialidade está disponível para atendimento em determinado horário
        public async Task<IEnumerable<DoctorViewByDapper>> GetAvailabeDoctor(MedicalAvailabilityQueryBySpecialty request)
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

                IEnumerable<DoctorViewByDapper> doctors = await con.QueryAsync<DoctorViewByDapper>(sql: sql, param: parameters);

                return doctors;
            }
        }


        // Função para consultar um médico atráves do seu Id
        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }


        // Função para consultar um médico atráves do seu Id na base de dados retornando sua respectiva View Dto
        public async Task<DoctorView> GetDoctorViewById(int id)
        {
            Doctor doctor = await GetDoctorById(id);

            return _mapper.Map<DoctorView>(doctor);
        }


        // Função para consultar todos os médicos na base de dados retornando suas respectivas Views Dto
        public async Task<List<DoctorView>> GetAllDoctorsView()
        {
            List<Doctor> doctors = await _context.Doctors.ToListAsync();

            return _mapper.Map<List<DoctorView>>(doctors);
        }


        // Função para inserir um médico na base de dados retornando sua respectiva View Dto
        public async Task<DoctorView> CreateDoctor(CreateDoctor request)
        {
            Doctor doctor = _mapper.Map<Doctor>(request);
            doctor.DoctorStatus = PersonStatus.Active;

            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorView>(doctor);
        }


        // Função para Alterar ou Atualizar um médico na base de dados retornando sua respectiva View Dto
        public async Task<DoctorView> UpdateDoctor(int id, UpdateDoctor request)
        {
            Doctor doctor = await GetDoctorById(id);

            if (doctor != null)
            {
                doctor.Name = request.Name;
                doctor.DateOfBirth = request.DateOfBirth;
                doctor.CPF = request.CPF;
                doctor.Email = request.Email;
                doctor.Phone = request.Phone;
                doctor.Address = request.Address;
                doctor.CRM = request.CRM;
                doctor.MedicalSpecialty = request.MedicalSpecialty;

                await _context.SaveChangesAsync();

                return _mapper.Map<DoctorView>(doctor);
            }

            throw new Exception("Doutor não encontrado");
        }


        // Função para Deletar um médico na base de dados retornando um boolean
        public async Task<bool> DeleteDoctor(int id)
        {
            Doctor doctor = await GetDoctorById(id);

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return true;
        }


        // Função para Alterar o status de um médico para inativo retornando sua View Dto
        public async Task<DoctorView> InactiveDoctor(int id)
        {
            Doctor doctor = await GetDoctorById(id);

            doctor.DoctorStatus = PersonStatus.Inactive;

            await _context.SaveChangesAsync();
            return _mapper.Map<DoctorView>(doctor);
        }


        // Função para Alterar o status de um médico para ativo retornando sua View Dto
        public async Task<DoctorView> ActiveDoctor(int id)
        {
            Doctor doctor = await GetDoctorById(id);

            doctor.DoctorStatus = PersonStatus.Active;

            await _context.SaveChangesAsync();
            return _mapper.Map<DoctorView>(doctor);
        }
    }
}
