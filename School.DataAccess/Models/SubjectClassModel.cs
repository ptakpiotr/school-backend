namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_subject_class
/// </summary>
public class SubjectClassModel
{
    public int Rok { get; set; }
    public string Nauczyciel { get; set; }
    public string NazwaPrzedmiotu { get; set; }
}
