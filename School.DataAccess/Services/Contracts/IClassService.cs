namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z funkcjonalnością klas
/// </summary>
public interface IClassService
{
    /// <summary>
    /// Metoda pobierająca listę wszystkich klas
    /// </summary>
    /// <returns></returns>
    Task<List<ClassModel>> GetAllClasses();

    /// <summary>
    /// Metoda pobierająca klasę o określonym ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ClassModel> GetClass(int id);

    /// <summary>
    /// Metoda dodająca klasę
    /// </summary>
    /// <param name="classToAdd"></param>
    /// <returns></returns>
    Task AddClass(ClassDTO classToAdd);

    /// <summary>
    /// Metoda usuwająca klasę
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteClass(int id);

    /// <summary>
    /// Metoda pobierająca klasy wraz z ich średnimi
    /// </summary>
    /// <returns></returns>
    Task<List<ClassAvgModel>> GetClassAverages();
}
