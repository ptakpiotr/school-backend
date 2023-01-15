namespace School.DataAccess.Models.DTOs;

/// <summary>
/// DTO służące do logowania przez użytkownika, w dalszej części mapowane do UserDTO
/// </summary>
public class UserLoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}
