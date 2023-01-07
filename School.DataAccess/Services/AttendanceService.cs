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
            await CRUDHelper.Add<AttendanceModel, AttendanceModel>(_mainConn, attendance);
        }

        public async Task DeleteAttendance(int id)
        {
            await CRUDHelper.Delete<AttendanceModel>(_mainConn, id);
        }

        public async Task<List<AttendanceModel>> GetAllAttendance()
        {
            return await CRUDHelper.GetAll<AttendanceModel>(_mainConn);
        }
    }
}
