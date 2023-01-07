using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

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
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("oceny", grade);
            }
        }

        public async Task DeleteGrade(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("oceny", id);
            }
        }

        public async Task<GradeModel> GetGrade(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<GradeModel>("oceny", id);
            }
        }

        public async Task<List<GradeModel>> GetGrades()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<GradeModel>("oceny");
            }
        }
    }
}
