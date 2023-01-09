namespace School.DataAccess.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly string _mainConn;

        public AttendanceService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddAttendance(AttendanceDTO attendance)
        {
            await CRUDHelper.Add<AttendanceModel, AttendanceDTO>(_mainConn, attendance);
        }

        public async Task DeleteAttendance(int id)
        {
            await CRUDHelper.Delete<AttendanceModel>(_mainConn, id);
        }

        public async Task<List<AttendanceModel>> GetAllAttendance()
        {
            return await CRUDHelper.GetAll<AttendanceModel>(_mainConn);
        }

        public async Task<List<AttendancePerClassModel>> GetAttendancePerClass(int classId, string expr)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.CallResultFunctionAsync<AttendancePerClassModel, FunctionModels.AttendancePerClassModel>("public.fn_get_attendance_per_class", new FunctionModels.AttendancePerClassModel() { ClassId = classId }, expr);
            }
        }
    }
}
