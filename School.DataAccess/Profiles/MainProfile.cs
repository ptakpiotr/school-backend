using AutoMapper;
using School.DataAccess.Models.DTOs;

namespace School.DataAccess.Profiles
{
    /// <summary>
    /// Główny profil dla AutoMappera
    /// </summary>
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<UserDTO, UserModel>();
            CreateMap<UserLoginDTO, UserDTO>();
            CreateMap<TeacherDTO, TeacherModel>().ReverseMap();
            CreateMap<RoomDTO, RoomModel>().ReverseMap();
            CreateMap<PaymentTypeDTO, PaymentTypeModel>().ReverseMap();
        }
    }
}
