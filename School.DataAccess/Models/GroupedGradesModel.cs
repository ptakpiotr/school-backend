namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający wartości zwróconej przez funkcję fn_get_grades
/// </summary>
public class GroupedGradesModel
{
    public int Ocena { get; set; }
    public int Uoid { get; set; }
    public string NazwaPracy { get; set; }
    public int UczenId { get; set; }
    public int KlasaId { get; set; }
}
