namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający rezultatowi zwróconemu przez funkcję fn_pobierz_oceny_ucznia
/// </summary>
public class StudentGradesModel
{
    public int Ocena { get; set; }
    public string NazwaPracy { get; set; }
    public int UczenId { get; set; }
    public int PrzedmiotOddzialId { get; set; }
}
