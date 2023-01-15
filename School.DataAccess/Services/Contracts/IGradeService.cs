namespace School.DataAccess.Services.Contracts;


/// <summary>
/// Serwis do interakcji z systemem ocen
/// </summary>
public interface IGradeService
{
    /// <summary>
    /// Metoda pobierajaca wszystkie oceny
    /// </summary>
    /// <returns></returns>
    Task<List<GradeModel>> GetGrades();

    /// <summary>
    /// Metoda pobierajaca ocene o okreslonym id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GradeModel> GetGrade(int id);

    /// <summary>
    /// Metoda dodajaca ocene
    /// </summary>
    /// <param name="grade"></param>
    /// <returns></returns>
    Task AddGrade(GradeModel grade);

    /// <summary>
    /// Metoda usuwajaca ocene
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteGrade(int id);

    /// <summary>
    /// Metoda zwracajaca pogrupowane oceny według parametrów przekazanych w wywołaniu metody
    /// </summary>
    /// <param name="classId"></param>
    /// <param name="np"></param>
    /// <returns></returns>
    Task<List<GroupedGradesModel>> GetGroupedGrades(int classId, string np);

    /// <summary>
    /// Metoda zwracająca oceny studenta
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<List<StudentGradesModel>> GetStudentGrades(int studentId);

}
