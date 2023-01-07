namespace School.DataAccess.Services
{
    public class ClassService : IClassService
    {
        private readonly string _mainConn;

        public ClassService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task AddClass(ClassDTO classToAdd)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("klasy", classToAdd);
            }
        }

        public async Task DeleteClass(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("klasy", id);
            }
        }

        public async Task<List<ClassModel>> GetAllClasses()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<ClassModel>("v_all_class");
            }
        }

        public async Task<ClassModel> GetClass(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<ClassModel>("v_all_class", id);
            }
        }
    }
}
