namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z encją nauczycieli
/// </summary>
public interface ITeacherService
{
    /// <summary>
    /// Metoda pobierająca listę wszystkich nauczycieli
    /// </summary>
    /// <returns></returns>
    Task<List<TeacherModel>> GetTeachers();

    /// <summary>
    /// Metoda pobierająca nauczyciela po jego/jej ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TeacherModel> GetTeacherById(int id);

    /// <summary>
    /// Metoda dodająca nauczyciela
    /// </summary>
    /// <param name="teacher"></param>
    /// <returns></returns>
    Task AddTeacher(TeacherDTO teacher);

    /// <summary>
    /// Metoda usuwająca nauczyciela
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteTeacher(int id);

    /// <summary>
    /// Metoda uaktualniająca dane nauczyciela
    /// </summary>
    /// <param name="id"></param>
    /// <param name="teacher"></param>
    /// <returns></returns>

    Task UpdateTeacher(int id, TeacherModel teacher);
}
