
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
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("plan_zajec", schedule);
            }
        }

        public async Task DeleteSchedule(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("plan_zajec", id);
            }
        }

        public async Task<List<ScheduleModel>> GetFullSchedule()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<ScheduleModel>("v_schedule");
            }
        }

        public async Task<ScheduleModel> GetScheduleById(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<ScheduleModel>("v_schedule", id);
            }
        }
    }
}
