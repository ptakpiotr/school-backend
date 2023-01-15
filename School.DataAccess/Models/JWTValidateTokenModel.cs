namespace School.DataAccess.Models;

/// <summary>
/// Model, który odpowiada ciału zapytania przy walidacji tokenu JWT
/// </summary>
public class JWTValidateTokenModel
{
    public string Token { get; set; }
}
