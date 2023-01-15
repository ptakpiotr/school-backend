namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z funkcjonalnością frekwencji
/// </summary>
public interface IAttendanceService
{
    /// <summary>
    /// Metoda pobierająca pełną listę frekwencji
    /// </summary>
    /// <returns></returns>
    Task<List<AttendanceModel>> GetAllAttendance();

    /// <summary>
    /// Metoda dodająca wpis o frekwencji
    /// </summary>
    /// <param name="attendance"></param>
    /// <returns></returns>
    Task AddAttendance(AttendanceDTO attendance);

    /// <summary>
    /// Metoda usuwająca wpis o obecności
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAttendance(int id);

    /// <summary>
    /// Metoda pobierająca frekwencję per klasa (w zależności od wartości parametrów)
    /// </summary>
    /// <param name="classId"></param>
    /// <param name="expr"></param>
    /// <returns></returns>
    Task<List<AttendancePerClassModel>> GetAttendancePerClass(int classId, string expr);
}
