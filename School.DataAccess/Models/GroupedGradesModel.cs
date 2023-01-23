namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający wartości zwróconej przez funkcję fn_pobierz_oceny
/// </summary>
public class GroupedGradesModel
{
    public int Ocena { get; set; }
    public int Uoid { get; set; }
    public string NazwaPracy { get; set; }
    public int UczenId { get; set; }
    public int KlasaId { get; set; }
}
