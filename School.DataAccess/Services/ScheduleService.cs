
namespace School.DataAccess.Services
{
    public class ScheduleService : IScheduleService
    {

        private readonly string _mainConn;

        public ScheduleService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }


        public async Task AddSchedule(ScheduleDTO schedule)
        {
            await CRUDHelper.Add<ScheduleModel, ScheduleDTO>(_mainConn, schedule);
        }

        public async Task DeleteSchedule(int id)
        {
            await CRUDHelper.Delete<ScheduleModel>(_mainConn, id);
        }

        public async Task<List<ScheduleModel>> GetFullSchedule()
        {
            return await CRUDHelper.GetAll<ScheduleModel>(_mainConn);
        }

        public async Task<ScheduleModel> GetScheduleById(int id)
        {
            return await CRUDHelper.GetOne<ScheduleModel>(_mainConn, id);
        }
    }
}
