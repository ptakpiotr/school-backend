namespace School.DataAccess.Models;

/// <summary>
/// Klasa wewnętrzna (dostępna tylko w tym projekcie) zapewniająca typowanie dla argumentów funkcji SQL
/// </summary>
internal class FunctionModels
{
    internal class GroupedGradesModel
    {
        public int ClassId { get; set; }
        public string Np { get; set; }
    }

    internal class StudentGradesModel
    {
        public int StudentId { get; set; }
    }

    internal class AttendancePerClassModel
    {
        public int ClassId { get; set; }
    }
}
