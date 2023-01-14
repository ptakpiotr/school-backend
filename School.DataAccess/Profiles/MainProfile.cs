using AutoMapper;
using School.DataAccess.Models.DTOs;

namespace School.DataAccess.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<UserDTO, UserModel>();
            CreateMap<UserLoginDTO, UserDTO>();
            CreateMap<TeacherDTO, TeacherModel>().ReverseMap();
        }
    }
}
