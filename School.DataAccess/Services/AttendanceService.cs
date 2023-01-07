namespace School.DataAccess.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly string _mainConn;

        public AttendanceService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddAttendance(AttendanceModel attendance)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("obecnosci", attendance);
            }
        }

        public async Task DeleteAttendance(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("obecnosci", id);
            }
        }

        public async Task<List<AttendanceModel>> GetAllAttendance()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<AttendanceModel>("obecnosci");
            }
        }
    }
}
