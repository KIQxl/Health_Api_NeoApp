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

        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Patient, Doctor")]
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

        [HttpPost]
        [Route("CreateAppointment")]
        [Authorize(Roles = "Admin")]
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

        [HttpPut]
        [Route("UpdateAppointment/{id}")]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Route("DeleteAppointment/{id}")]
        [Authorize(Roles = "Admin")]
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
