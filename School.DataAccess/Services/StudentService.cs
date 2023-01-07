namespace School.DataAccess.Services
{
    public class StudentService : IStudentService
    {
        private readonly string _mainConn;

        public StudentService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddStudent(StudentDTO student)
        {
            await CRUDHelper.Add<StudentModel, StudentDTO>(_mainConn, student);
        }

        public async Task DeleteStudent(int id)
        {
            await CRUDHelper.Delete<StudentModel>(_mainConn, id);
        }

        public async Task<StudentModel> GetStudentById(int id)
        {
            return await CRUDHelper.GetOne<StudentModel>(_mainConn, id);
        }

        public async Task<List<StudentModel>> GetStudents()
        {
            return await CRUDHelper.GetAll<StudentModel>(_mainConn);
        }
    }
}
