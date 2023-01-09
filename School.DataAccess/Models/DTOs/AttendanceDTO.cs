namespace School.DataAccess.Models.Dtos;

public class AttendanceDTO
{
    public int Id { get; set; }
    public int Uczen_id { get; set; }
    public bool Obecny { get; set; }
    public DateTime Data { get; set; }
}
