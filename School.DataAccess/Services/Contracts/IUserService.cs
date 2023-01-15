using School.DataAccess.Models.DTOs;

namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z systemem użytkowników
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Metoda sprawdzająca czy użytkownik o podanym adresie email istnieje
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<bool> CheckUserExists(string email);

    /// <summary>
    /// Metoda porównująca czy hasło dostarczone przez użytkownika jest poprawne
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<bool> ComparePassword(UserDTO user);

    /// <summary>
    /// Metoda tworząca nowego użytkownika
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task CreateUser(UserModel user);

    /// <summary>
    /// Metoda usuwająca użytkownika
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteUser(int id);
}
