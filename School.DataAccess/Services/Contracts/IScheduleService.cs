namespace School.DataAccess.Services.Contracts;

/// <summary>
/// Serwis do interakcji z planem zajęć
/// </summary>
public interface IScheduleService
{
    /// <summary>
    /// Metoda pobierająca pełny plan zajęć
    /// </summary>
    /// <returns></returns>
    Task<List<ScheduleModel>> GetFullSchedule();

    /// <summary>
    /// Metoda pobierająca plan zajęć po ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ScheduleModel> GetScheduleById(int id);

    /// <summary>
    /// Metoda dodająca wpis do planu zajęc
    /// </summary>
    /// <param name="schedule"></param>
    /// <returns></returns>
    Task AddSchedule(ScheduleDTO schedule);

    /// <summary>
    /// Metoda usuwająca wpis
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteSchedule(int id);
}