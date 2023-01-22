namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_attendance
/// </summary>
public class UserGradeModel
{
    public int Id { get; set; }
    public string ImieNazwiskoUcznia { get; set; }
    public string NazwaPrzedmiotu { get; set; }
    public string Nazwa_Pracy { get; set; }
}