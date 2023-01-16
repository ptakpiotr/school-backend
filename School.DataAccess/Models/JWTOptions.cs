namespace School.DataAccess.Models;

/// <summary>
/// Model zawierający opcje dla sekcji JWT w konfiguracji
/// </summary>
public class JWTOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}
