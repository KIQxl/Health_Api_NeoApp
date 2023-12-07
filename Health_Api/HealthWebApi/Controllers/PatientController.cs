using Domain.Interfaces;
using Domain.Services;
using Entities.Dtos.DoctorDto;
using Entities.Dtos.PatientDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWebApi.Controllers
{
    [ApiController]
    [Route("Health/v1/Patients")]
    public class PatientController : ControllerBase
    {
        public readonly IPatientServices _patientServices;

        public PatientController(IPatientServices patientServices)
        {
            this._patientServices = patientServices;
        }

        [HttpGet]
        [Route("GetAllViews")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<PatientView> patients = await _patientServices.GetAllPatientsView();

                return Ok(patients);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetViewById([FromRoute] int id)
        {
            try
            {
                PatientView patient = await _patientServices.GetPatientViewById(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePatient")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatient request)
        {
            try
            {
                PatientView patientView = await _patientServices.CreatePatient(request);

                return Created($"Health/v1/Doctors/GetViewById/{patientView.Id}", patientView);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePatient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePatient request)
        {
            try
            {
                PatientView patientView = await _patientServices.UpdatePatient(id, request);

                return Created($"Health/v1/Doctors/GetViewById/{patientView.Id}", patientView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePatient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            try
            {
                bool result = await _patientServices.DeletePatient(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActivePatient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivePatient([FromRoute] int id)
        {
            try
            {
                PatientView patient = await _patientServices.ActivePatient(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("InactivePatient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InactivePatient([FromRoute] int id)
        {
            try
            {
                PatientView patient = await _patientServices.InactivePatient(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
