namespace School.DataAccess.Models;

/// <summary>
/// Model reprezentujacy pojedynczego uzytkownika
/// </summary>
public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
