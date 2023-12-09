using AutoMapper;
using Entities.Dtos.UserDot;
using Entities.Dtos.UserDto;
using Entities.Models;
using HealthWebApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthWebApi.Services
{
    public class UserServices : IUserServices
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly ITokenServices _tokenServices;
        public readonly IMapper _mapper;
        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenServices tokenServices) 
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenServices = tokenServices;
        }


        // Função para retornar um usuário da base de dados através do seu username retornando sua respectiva View Dto
        public async Task<UserView> GetUserViewByUserName(LoginRequest request)
        {
            User user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName.Equals(request.UserName.ToUpper()));

            UserView userView = _mapper.Map<UserView>(user);

            return userView;
        }


        // Função para retornar um usuário da base de dados através do seu username
        public async Task<User> GetUserByUserName(LoginRequest request)
        {
            User user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName.Equals(request.UserName.ToUpper()));

            return user;
        }


        // Função para criar/adicionar um usuário na base de dados retornando sua respectiva View Dto
        public async Task<IdentityResult> CreateUser(CreateUser request)
        {
            User user = _mapper.Map<User>(request);

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }


        // Função para realizar login de um usuário cadastrado na base de dados retornando sua View Dto e um Token de autenticação
        public async Task<string> Login(LoginRequest request)
        {
           try
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

                if (result.Succeeded)
                {
                    User user = await GetUserByUserName(request);

                    var token = _tokenServices.GenerateToken(user);

                    return token;
                }

                throw new Exception(result.ToString());
            } catch (Exception ex)
            {
                throw new Exception($"Failed to login {ex.Message}");
            }
        }
    }
}
