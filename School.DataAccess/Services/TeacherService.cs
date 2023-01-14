namespace School.DataAccess.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly string _mainConn;

        public TeacherService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddTeacher(TeacherDTO teacher)
        {
            await CRUDHelper.Add<TeacherModel, TeacherDTO>(_mainConn, teacher);
        }

        public async Task DeleteTeacher(int id)
        {
            await CRUDHelper.Delete<TeacherModel>(_mainConn, id);
        }

        public async Task<TeacherModel> GetTeacherById(int id)
        {
            return await CRUDHelper.GetOne<TeacherModel>(_mainConn, id);
        }

        public async Task<List<TeacherModel>> GetTeachers()
        {
            return await CRUDHelper.GetAll<TeacherModel>(_mainConn);
        }

        public async Task UpdateTeacher(int id, TeacherModel teacher)
        {
            await CRUDHelper.Update(_mainConn, id, teacher);
        }
    }
}
