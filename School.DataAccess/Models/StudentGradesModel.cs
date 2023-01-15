namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający rezultatowi zwróconemu przez funkcję fn_get_student_grades
/// </summary>
public class StudentGradesModel
{
    public int Ocena { get; set; }
    public string NazwaPracy { get; set; }
    public int UczenId { get; set; }
    public int PrzedmiotOddzialId { get; set; }
}
