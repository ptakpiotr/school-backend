namespace School.DataAccess.Services.Contracts
{
    public interface IScheduleService
    {
        Task<List<ScheduleModel>> GetFullSchedule();
        Task<ScheduleModel> GetScheduleById(int id);
        Task AddSchedule(ScheduleDTO schedule);
        Task DeleteSchedule(int id);
    }
}