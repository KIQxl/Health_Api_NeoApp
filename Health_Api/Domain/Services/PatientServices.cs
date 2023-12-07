using AutoMapper;
using Domain.Interfaces;
using Entities.Dtos.DoctorDto;
using Entities.Dtos.PatientDto;
using Entities.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class PatientServices : IPatientServices
    {
        public readonly HealthApiContext _context;
        public readonly IMapper _mapper;
        public PatientServices(HealthApiContext context, IMapper mapper)
        {
           this._context = context;
            this._mapper = mapper;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            return patient;
        }

        public async Task<PatientView> GetPatientViewById(int id)
        {
            Patient patient = await GetPatientById(id);

            return _mapper.Map<PatientView>(patient);
        }

        public async Task<List<PatientView>> GetAllPatientsView()
        {
            List<Patient> patients = await _context.Patients.ToListAsync();

            return _mapper.Map<List<PatientView>>(patients);
        }

        public async Task<PatientView> CreatePatient(CreatePatient request)
        {
            Patient patient = _mapper.Map<Patient>(request);
            patient.PatientStatus = PersonStatus.Active;

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientView>(patient);
        }

        public async Task<PatientView> UpdatePatient(int id, UpdatePatient request)
        {
            Patient patient = await GetPatientById(id);

            if(patient != null)
            {
                patient.Name = request.Name;
                patient.DateOfBirth = request.DateOfBirth;
                patient.CPF = request.CPF;
                patient.Email = request.Email;
                patient.Phone = request.Phone;
                patient.Address = request.Address;

                await _context.SaveChangesAsync();

                return _mapper.Map<PatientView>(patient);
            }

            throw new Exception("Paciente não encontrado");
        }

        public async Task<bool> DeletePatient(int id)
        {
            Patient patient = await GetPatientById(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<PatientView> InactivePatient(int id)
        {
            Patient patient = await GetPatientById(id);

            patient.PatientStatus = PersonStatus.Inactive;

            await _context.SaveChangesAsync();
            return _mapper.Map<PatientView>(patient);
        }


        public async Task<PatientView> ActivePatient(int id)
        {
            Patient patient = await GetPatientById(id);

            patient.PatientStatus = PersonStatus.Active;

            await _context.SaveChangesAsync();
            return _mapper.Map<PatientView>(patient);
        }
    }
}
