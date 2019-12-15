using AutoMapper;
using SimpleCRUD.Entities.Entities;

namespace SimpleCRUD.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserPostDto>().ReverseMap();
        }
    }
}
