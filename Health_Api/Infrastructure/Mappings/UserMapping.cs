using AutoMapper;
using Entities.Dtos.UserDot;
using Entities.Models;

namespace Infrastructure.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<CreateUser, User>();
            CreateMap<User, UserView>();
        }

    }
}
