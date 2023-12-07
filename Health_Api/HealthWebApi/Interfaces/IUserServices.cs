using Entities.Dtos.UserDot;
using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace HealthWebApi.Interfaces
{
    public interface IUserServices
    {
        public Task<User> GetUserByUserName(LoginRequest request);
        public Task<UserView> GetUserViewByUserName(LoginRequest request);
        public Task<IdentityResult> CreateUser(CreateUser request);
        public Task<string> Login(LoginRequest request);
    }
}
