namespace School.DataAccess.Models.Dtos;
//Property naming violates C# conventions --> applied here for easier mapping between models and database entities

/// <summary>
/// DTO dla modelu AttendanceModel
/// </summary>
public class AttendanceDTO
{
    public int Id { get; set; }
    public int Uczen_id { get; set; }
    public bool Obecny { get; set; }
    public DateTime Data { get; set; }
}
