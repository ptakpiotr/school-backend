namespace School.DataAccess.Models;

/// <summary>
/// Klasa statyczna zapewniająca mapowania pomiędzy modelem, tabelą oraz (opcjonalnie) widokiem
/// </summary>
internal static class ModelDTODatabaseMappings
{
    public static Dictionary<Type, TableNames> mappings { get; set; } = new();
    static ModelDTODatabaseMappings()
    {
        mappings.Add(typeof(AttendanceModel), new() { Original = "obecnosci", View = "v_frekwencja" });
        mappings.Add(typeof(ClassModel), new() { Original = "klasy", View = "v_wszystkie_klasy" });
        mappings.Add(typeof(GradeModel), new() { Original = "oceny", View = "oceny" });
        mappings.Add(typeof(PaymentModel), new() { Original = "oplaty", View = "v_wszystkie_platnosci" });
        mappings.Add(typeof(ScheduleModel), new() { Original = "plan_zajec", View = "v_plan_zajec" });
        mappings.Add(typeof(StudentModel), new() { Original = "uczniowie", View = "v_wszyscy_studenci" });
        mappings.Add(typeof(SubjectModel), new() { Original = "przedmioty", View = "v_wszystkie_przedmioty" });
        mappings.Add(typeof(TeacherModel), new() { Original = "nauczyciele", View = "nauczyciele" });
        mappings.Add(typeof(ClassAvgModel), new() { Original = "v_srednia_klas", View = "v_srednia_klas" });
        mappings.Add(typeof(UserGradeModel), new() { Original = "uczen_oceny", View = "v_prace_ucznia" });
        mappings.Add(typeof(SubjectClassModel), new() { Original = "przedmiot_oddzial", View = "v_przedmiot_w_oddzialach" });
    }
}

/// <summary>
/// Klasa reprezentująca nazwy tabel (oryginalną oraz widok)
/// </summary>
class TableNames
{
    public string Original { get; set; }
    public string? View { get; set; }
}
