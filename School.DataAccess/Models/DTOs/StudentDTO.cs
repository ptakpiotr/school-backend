namespace School.DataAccess.Models.Dtos;

/// <summary>
/// DTO dla modelu StudentModel
/// </summary>
public class StudentDTO
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public long Klasa_id { get; set; }
}
