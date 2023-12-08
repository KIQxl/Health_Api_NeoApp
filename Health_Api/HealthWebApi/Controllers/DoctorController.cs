using Domain.Interfaces;
using Entities.Dtos.DoctorDto;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWebApi.Controllers
{
    [ApiController]
    [Route("Health/v1/Doctors")]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            this._doctorServices = doctorServices;
        }


        /// <summary>
        /// Obter todas as views Dto de médicos da base de dados
        /// </summary>
        /// <returns>Lista de views Dto de médicos</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Médico não encontrado</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Authorize(Roles = "Admin, Patient")]
        [Route("GetAllViews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllDoctorsView()
        {
            try
            {
                List<DoctorView> doctors = await _doctorServices.GetAllDoctorsView();

                return Ok(doctors);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obter a view Dto de um médico específico da base de dados
        /// </summary>
        /// <param name="id">Identificador do paciente</param>
        /// <returns>View Dto do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Médico não encontrado</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Patient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetViewById([FromRoute] int id)
        {
            try
            {
                DoctorView doctor = await _doctorServices.GetDoctorViewById(id);

                return Ok(doctor);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Criar um novo registro na tabela de médicos
        /// </summary>
        /// <returns>View Dto do médico</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPost]
        [Route("CreateDoctor")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctor request)
        {
            try
            {
                DoctorView doctor = await _doctorServices.CreateDoctor(request);

                return Created($"Health/v1/Doctors/GetViewById/{doctor.Id}", doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Alterar um registro na tabela de médicos
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>View Dto do médico</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("UpdateDoctor/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateDoctor([FromRoute] int id, [FromBody] UpdateDoctor request)
        {
            try
            {
                DoctorView doctor = await _doctorServices.UpdateDoctor(id, request);

                return Created($"Health/v1/Doctors/GetViewById/{doctor.Id}", doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Excluir um registro na tabela de médicos
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>True para sucesso na exclusão, False para falha na exclusão</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpDelete]
        [Route("DeleteDoctor/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            try
            {
                bool result = await _doctorServices.DeleteDoctor(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Alterar o Status do médico para Ativo (Active)
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>View Dto do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("ActiveDoctor/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ActiveDoctor([FromRoute] int id)
        {
            try
            {
                DoctorView doctor = await _doctorServices.ActiveDoctor(id);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Alterar o Status do médico para Inativo (Inactive)
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>View Dto do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("InactiveDoctor/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> InactiveDoctor([FromRoute] int id)
        {
            try
            {
                DoctorView doctor = await _doctorServices.InactiveDoctor(id);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Buscar médicos por especialidade disponíveis em um horário específico
        /// </summary>
        /// <param name="id">Identificador do médico</param>
        /// <returns>View Dto do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Route("GetAvailabeDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAvailabeDoctor([FromBody] MedicalAvailabilityQueryBySpecialty request)
        {
            try
            {
                var doctor = await _doctorServices.GetAvailabeDoctor(request);
                return Ok(doctor);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
