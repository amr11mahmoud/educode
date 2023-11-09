using AutoMapper;
using Educode.Core.Dtos.Users;
using Educode.Domain.Users.Models;

namespace Educode.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, ReadUserResponseDto>();
        }
    }
}
