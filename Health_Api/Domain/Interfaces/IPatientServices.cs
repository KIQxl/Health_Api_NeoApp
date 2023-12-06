using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPatientServices
    {
        public Task<PatientView> Add(CreatePatient request);
        public Task<List<PatientView>> GetAll();
        public Task<PatientView> Update(int id, UpdatePatient request);
        public Task<bool> Delete(int id);
        public Task<PatientView> GetViewById(int id);
    }
}
