﻿using Domain.Interfaces;
using Entities.Dtos.PatientDto;
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<PatientView> patients = await _patientServices.GetAll();

                return Ok(patients);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetViewById/{id}")]
        public async Task<IActionResult> GetViewById([FromRoute] int id)
        {
            try
            {
                PatientView patient = await _patientServices.GetViewById(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePatient")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatient request)
        {
            try
            {
                PatientView patientView = await _patientServices.Add(request);

                return Ok(patientView);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePatient request)
        {
            try
            {
                PatientView patientView = await _patientServices.Update(id, request);

                return Ok(patientView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            try
            {
                bool result = await _patientServices.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}