using Domain.Interfaces;
using Entities.Dtos.AppointmentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWebApi.Controllers
{
    [ApiController]
    [Route("Health/v1/Appointments")]
    public class AppointmentController : ControllerBase
    {
        public readonly IAppointmentServices _appointmentServices;

        public AppointmentController(IAppointmentServices appointmentServices)
        {
            this._appointmentServices = appointmentServices;
        }


        /// <summary>
        /// Obter todas as views Dto de consultas médicas da base de dados
        /// </summary>
        /// <returns>Lista de views Dto de consultas médicas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Médico não encontrado</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllViews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAppointmentsView()
        {
            try
            {
                List<AppointmentView> appointments = await _appointmentServices.GetAllAppointmensView();

                return Ok(appointments);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Obter a view Dto de uma consulta médica específica da base de dados
        /// </summary>
        /// <param name="id">Identificador da consulta médica</param>
        /// <returns>View Dto da consulta médica</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Médico não encontrado</response>
        /// <response code="401">Não Autorizado</response>
        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Patient, Doctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAppointmentsView([FromRoute] int id)
        {
            try
            {
                AppointmentView appointment = await _appointmentServices.GetAppointmentViewById(id);

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Criar um novo registro na tabela de consultas médicas
        /// </summary>
        /// <returns>View Dto da consulta médica</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPost]
        [Route("CreateAppointment")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointment request)
        {
            try
            {
                AppointmentView appointment = await _appointmentServices.CreateAppointment(request);

                return Created($"Health/v1/Appointments/GetViewById/{appointment.Id}", appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        /// <summary>
        /// Alterar um registro na tabela de consultas médicas
        /// </summary>
        /// <param name="id">Identificador da consulta médica</param>
        /// <returns>View Dto da consulta médica</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpPut]
        [Route("UpdateAppointment/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAppointment([FromRoute] int id, [FromBody] UpdateAppointment request)
        {
            try
            {
                AppointmentView appointment = await _appointmentServices.UpdateAppointment(id, request);

                return Created($"Health/v1/Appointments/GetViewById/{appointment.Id}", appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Excluir um registro na tabela de consultas médicas
        /// </summary>
        /// <param name="id">Identificador da consulta médica</param>
        /// <returns>True para sucesso na exclusão, False para falha na exclusão</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        /// <response code="401">Não Autorizado</response>
        [HttpDelete]
        [Route("DeleteAppointment/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAppointment([FromRoute] int id)
        {
            try
            {
                bool result = await _appointmentServices.DeleteAppointment(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
