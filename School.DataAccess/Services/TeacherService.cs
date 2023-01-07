using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

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
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("nauczyciele", teacher);
            }
        }

        public async Task DeleteTeacher(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("nauczyciele", id);
            }
        }

        public async Task<TeacherModel> GetTeacherById(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<TeacherModel>("nauczyciele", id);
            }
        }

        public async Task<List<TeacherModel>> GetTeachers()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<TeacherModel>("nauczyciele");
            }
        }
    }
}
