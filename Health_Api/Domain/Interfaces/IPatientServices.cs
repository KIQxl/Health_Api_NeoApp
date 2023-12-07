using Entities.Dtos.DoctorDto;
using Entities.Dtos.PatientDto;
using Entities.Models;

namespace Domain.Interfaces
{
    public interface IPatientServices
    {
        public Task<Patient> GetPatientById(int id);
        public Task<List<PatientView>> GetAllPatientsView();
        public Task<PatientView> GetPatientViewById(int id);
        public Task<PatientView> CreatePatient(CreatePatient request);
        public Task<PatientView> UpdatePatient(int id, UpdatePatient request);
        public Task<bool> DeletePatient(int id);
        public Task<PatientView> InactivePatient(int id);
        public Task<PatientView> ActivePatient(int id);
    }
}
