namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_all_class
/// </summary>
public class ClassModel
{
    public int Id { get; set; }
    public int Rok { get; set; }
    public string ImieNazwiskoNauczyciela { get; set; }
}
