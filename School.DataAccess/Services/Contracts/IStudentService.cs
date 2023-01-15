namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do obsługi uczniów
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Metoda pobierająca wszystkich uczniów
    /// </summary>
    /// <returns></returns>
    Task<List<StudentModel>> GetStudents();

    /// <summary>
    /// Metoda pobierająca ucznia po ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StudentModel> GetStudentById(int id);

    /// <summary>
    /// Metoda dodająca ucznia
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    Task AddStudent(StudentDTO student);

    /// <summary>
    /// Metoda usuwająca ucznia
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteStudent(int id);
}