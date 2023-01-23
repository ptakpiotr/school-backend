namespace School.DataAccess.Models.Dtos;

/// <summary>
/// DTO dla modelu SubjectClassModel
/// </summary>
public class SubjectClassDTO
{
    public int Klasa_id { get; set; }
    public int Nauczyciel_id { get; set; }
    public int Przedmiot_id { get; set; }
}
