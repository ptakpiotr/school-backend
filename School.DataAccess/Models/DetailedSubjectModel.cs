namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_wszystkie_przedmioty
/// </summary>
public class DetailedSubjectModel
{
    public int Id { get; set; }
    public int Rok { get; set; }
    public string ImieNazwiskoNauczyciela { get; set; }
    public int NauczycielId { get; set; }
    public string NazwaPrzedmiotu { get; set; }
}
