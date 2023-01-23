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
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.CallExecuteFunctionAsync("public.fn_wstaw_przedmiot", new { np = subject.Nazwa_przedmiotu, ns = subject.Numer_sali });
            }
        }

        public async Task AddSubjectClass(SubjectClassDTO subjectClassDTO)
        {
            await CRUDHelper.Add<SubjectClassModel, SubjectClassDTO>(_mainConn, subjectClassDTO);
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
                return await conn.SelectAllAsync<DetailedSubjectModel>("v_wszystkie_przedmioty_dokladnie");
            }
        }

        public async Task<SubjectModel> GetSubject(int id)
        {
            return await CRUDHelper.GetOne<SubjectModel>(_mainConn, id);

        }

        public async Task<List<SubjectClassModel>> GetSubjectClasses()
        {
            return await CRUDHelper.GetAll<SubjectClassModel>(_mainConn, false);
        }
    }
}
