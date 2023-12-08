using Entities.Dtos.UserDot;
using Entities.Dtos.UserDto;
using HealthWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthWebApi.Controllers
{
    [ApiController]
    [Route("Health/v1/Users")]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            this._userServices = userServices;
        }


        /// <summary>
        /// Realizar login
        /// </summary>
        /// <returns>Token de autenticação e View do Usuário</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _userServices.Login(request);
                UserView user = await _userServices.GetUserViewByUserName(request);

                return Ok(new
                {
                    token = token,
                    user = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Criar um novo registro na tabela de usuários
        /// </summary>
        /// <returns>Resultado da operação (True ou False)</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Falha na requisição</response>
        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser request)
        {
            try
            {
                IdentityResult result = await _userServices.CreateUser(request);

                if (!result.Succeeded)
                {
                    return BadRequest("Falha ao criar usuário");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
