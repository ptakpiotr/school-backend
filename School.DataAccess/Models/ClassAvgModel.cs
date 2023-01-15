namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_class_avg
/// </summary>
public class ClassAvgModel
{
    public int Id { get; set; }
    public int Rok { get; set; }
    public string NazwaPracy { get; set; }
    public double SredniaOcena { get; set; }
}
