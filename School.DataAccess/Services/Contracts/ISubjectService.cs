namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z przedmiotami
/// </summary>
public interface ISubjectService
{
    /// <summary>
    /// Metoda pobierająca listę wszystkich przedmiotów
    /// </summary>
    /// <returns></returns>
    Task<List<SubjectModel>> GetAllSubjects();

    /// <summary>
    /// Metoda pobierająca listę przedmiotów w bardziej szczegółowej formie
    /// </summary>
    /// <returns></returns>
    Task<List<DetailedSubjectModel>> GetDetailedSubjects();

    /// <summary>
    /// Metoda pobierająca przedmiot po ID przedmiotu
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SubjectModel> GetSubject(int id);

    /// <summary>
    /// Metoda dodająca przedmiot
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    Task AddSubject(SubjectDTO subject);

    /// <summary>
    /// Metoda usuwająca przedmiot
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteSubject(int id);

    /// <summary>
    /// Metoda zwracajaca liste przedmiotow-oddzialow
    /// </summary>
    /// <returns></returns>
    Task<List<SubjectClassModel>> GetSubjectClasses();

    /// <summary>
    /// Metoda dodajaca przedmiot-oddzial
    /// </summary>
    /// <param name="subjectClassDTO"></param>
    /// <returns></returns>
    Task AddSubjectClass(SubjectClassDTO subjectClassDTO);
}
