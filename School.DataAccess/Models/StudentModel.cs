namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_wszyscy_studenci
/// </summary>
public class StudentModel
{
    public int Id { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public int Rok { get; set; }
}
