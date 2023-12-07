using Entities.Models;

namespace HealthWebApi.Interfaces
{
    public interface ITokenServices
    {
        public string GenerateToken(User user);
    }
}
