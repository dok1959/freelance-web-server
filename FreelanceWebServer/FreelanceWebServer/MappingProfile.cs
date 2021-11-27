using AutoMapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ProfileDTO>();
            CreateMap<RegistrationDTO, User>();
        }
    }
}