using Dapper;
using School.DataAccess.Models.DTOs;

namespace School.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly string _mainConn;

        public UserService(IOptions<ConnectionStringOptions> options)
        {
            _mainConn = options.Value.MainConn;
        }

        public async Task<bool> CheckUserExists(string email)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                var users = await conn.QueryAsync<UserModel>("SELECT email FROM users WHERE email = @email LIMIT 1", new { email = email });
                UserModel user = users.FirstOrDefault();

                return user is not null;
            }
        }

        public async Task<bool> ComparePassword(UserDTO user)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                var res = await conn.QueryAsync<UserModel>("SELECT * FROM users WHERE Email = @email LIMIT 1", user);

                UserModel usr = res.FirstOrDefault();

                if (usr is null)
                {
                    return false;
                }

                return BCrypt.Net.BCrypt.Verify(user.Password, usr.Password);
            }
        }

        public async Task CreateUser(UserModel user)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.ExecuteAsync("INSERT INTO users(Email,Password) VALUES(@email,@password)", user);
            }
        }

        public async Task DeleteUser(int id)
        {
            using (IDbConnection conn = new NpgsqlConnection(_mainConn))
            {
                await conn.ExecuteAsync("DELETE FROM users WHERE id = @id", new { id = id });
            }
        }
    }
}
