using School.DataAccess.Models.DTOs;

namespace School.DataAccess.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> CheckUserExists(string email);
        Task<bool> ComparePassword(UserDTO user);
        Task CreateUser(UserModel user);
        Task DeleteUser(int id);
    }
}
