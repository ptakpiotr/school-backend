namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający rezultatowi zwracanemu przez funkcję fn_frekwencja_per_klasa
/// </summary>
public class AttendancePerClassModel
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public int Rok { get; set; }
    public bool Obecny { get; set; }
    public DateTime Data { get; set; }
}
