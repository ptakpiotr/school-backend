using Microsoft.Extensions.Options;
using Npgsql;
using School.DataAccess.Services.Contracts;
using System.Data;

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
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.InsertAsync("uczniowie", student);
            }

        }

        public async Task DeleteStudent(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.DeleteAsync("uczniowie", id);
            }
        }

        public async Task<StudentModel> GetStudentById(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectOneAsync<StudentModel>("uczniowie", id);
            }
        }

        public async Task<List<StudentModel>> GetStudents()
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                return await conn.SelectAllAsync<StudentModel>("v_all_students");
            }
        }
    }
}
