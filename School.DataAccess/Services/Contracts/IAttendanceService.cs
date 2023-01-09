namespace School.DataAccess.Services.Contracts
{
    public interface IAttendanceService
    {
        Task<List<AttendanceModel>> GetAllAttendance();
        Task AddAttendance(AttendanceDTO attendance);
        Task DeleteAttendance(int id);
        Task<List<AttendancePerClassModel>> GetAttendancePerClass(int classId, string expr);
    }
}
