namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_srednia_klas
/// </summary>
public class ClassAvgModel
{
    public int Id { get; set; }
    public int Rok { get; set; }
    public string NazwaPracy { get; set; }
    public double SredniaOcena { get; set; }
}
