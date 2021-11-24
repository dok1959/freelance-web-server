using AutoMapper;
using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO;
using FreelanceWebServer.Models.DTO.Account;

namespace FreelanceWebServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<User, ProfileDTO>();
            CreateMap<RegistrationDTO, User>();
        }
    }
}