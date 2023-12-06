using AutoMapper;
using Domain.Interfaces;
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

        public async Task<List<PatientView>> GetAll()
        {
            List<Patient> patients = await _context.Patients.ToListAsync();

            return _mapper.Map<List<PatientView>>(patients);
        }

        public async Task<Patient> GetById(int id)
        {
            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            return patient;
        }

        public async Task<PatientView> GetViewById(int id)
        {
            Patient patient = await GetById(id);

            return _mapper.Map<PatientView>(patient);
        }

        public async Task<PatientView> Add(CreatePatient request)
        {
            Patient patient = _mapper.Map<Patient>(request);

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientView>(patient);
        }

        public async Task<PatientView> Update(int id, UpdatePatient request)
        {
            Patient patient = await GetById(id);

            patient.Name = request.Name;
            patient.DateOfBirth = request.DateOfBirth;
            patient.CPF = request.CPF;
            patient.Email = request.Email;
            patient.Phone = request.Phone;
            patient.Address = request.Address;

            await _context.SaveChangesAsync();

            return _mapper.Map<PatientView>(patient);
        }

        public async Task<bool> Delete(int id)
        {
            Patient patient = await GetById(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
