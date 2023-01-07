namespace School.DataAccess.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly string _mainConn;

        public SubjectService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddSubject(SubjectDTO subject)
        {
            await CRUDHelper.Add<SubjectModel, SubjectDTO>(_mainConn, subject);
        }

        public async Task DeleteSubject(int id)
        {
            await CRUDHelper.Delete<SubjectModel>(_mainConn, id);
        }

        public async Task<List<SubjectModel>> GetAllSubjects()
        {
            return await CRUDHelper.GetAll<SubjectModel>(_mainConn);
        }

        public async Task<List<DetailedSubjectModel>> GetDetailedSubjects()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<DetailedSubjectModel>("v_all_subjects_detailed");
            }
        }

        public async Task<SubjectModel> GetSubject(int id)
        {
            return await CRUDHelper.GetOne<SubjectModel>(_mainConn, id);

        }
    }
}
