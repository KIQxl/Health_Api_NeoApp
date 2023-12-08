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

        /// <summary>
        /// Obter todas as views Dto de pacientes da base de dados
        /// </summary>
        /// <returns>Lista de views Dto de pacientes</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Route("GetAllViews")]
        [Authorize(Roles = "Admin, Doctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Obter a view Dto de um paciente específico da base de dados
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>View Dto do paciente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Paciente não encontrado</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Doctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Criar um novo registro na tabela de pacientes
        /// </summary>
        /// <returns>View Dto do paciente</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPost]
        [Route("CreatePatient")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Alterar um registro na tabela de pacientes
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>View Dto do paciente</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("UpdatePatient/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Excluir um registro na tabela de pacientes
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>True para sucesso na exclusão, False para falha na exclusão</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpDelete]
        [Route("DeletePatient/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Alterar o Status do paciente para Ativo (Active)
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>View Dto do paciente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("ActivePatient/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Alterar o Status do paciente para Inativo (Inactive)
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>View Dto do paciente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("InactivePatient/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
