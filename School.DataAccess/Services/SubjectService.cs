using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

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
                await conn.InsertAsync("przedmioty", subject);
            }
        }

        public async Task DeleteSubject(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("przedmioty", id);
            }
        }

        public async Task<List<SubjectModel>> GetAllSubjects()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<SubjectModel>("v_all_subjects");
            }
        }

        public async Task<SubjectModel> GetSubject(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<SubjectModel>("v_all_subjects", id);
            }
        }
    }
}
