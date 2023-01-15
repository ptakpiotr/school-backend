namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający tabeli oceny
/// </summary>
public class GradeModel
{
    public int Id { get; set; }
    public int Ocena { get; set; }
    public int Uczen_ocena_id { get; set; }
}
