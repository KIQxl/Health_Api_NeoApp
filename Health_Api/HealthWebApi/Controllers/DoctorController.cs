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

        [HttpGet]
        [Authorize(Roles = "Admin, Patient")]
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

        [HttpGet]
        [Route("GetViewById/{id}")]
        [Authorize(Roles = "Admin, Patient")]
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

        [HttpPost]
        [Route("CreateDoctor")]
        [Authorize(Roles = "Admin")]
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

        [HttpPut]
        [Route("UpdateDoctor/{id}")]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Route("DeleteDoctor/{id}")]
        [Authorize(Roles = "Admin")]
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

    }
}
