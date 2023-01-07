namespace School.DataAccess.Services
{
    public class GradeService : IGradeService
    {
        private readonly string _mainConn;

        public GradeService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddGrade(GradeModel grade)
        {
            await CRUDHelper.Add<GradeModel, GradeModel>(_mainConn, grade);

        }

        public async Task DeleteGrade(int id)
        {
            await CRUDHelper.Delete<GradeModel>(_mainConn, id);
        }

        public async Task<GradeModel> GetGrade(int id)
        {
            return await CRUDHelper.GetOne<GradeModel>(_mainConn, id);
        }

        public async Task<List<GradeModel>> GetGrades()
        {
            return await CRUDHelper.GetAll<GradeModel>(_mainConn);
        }
    }
}
