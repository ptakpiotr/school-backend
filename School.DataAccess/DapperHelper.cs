using Dapper;

namespace School.DataAccess
{
    internal static class DapperHelper
    {
        internal static async Task DeleteAsync(this IDbConnection conn, string tableName, int id)
        {
            await conn.ExecuteAsync($"DELETE FROM {tableName} WHERE id = @id", new { id = id });
        }

        internal static async Task InsertAsync<T>(this IDbConnection conn, string tableName, T value)
        {
            string[] columnNames = typeof(T).GetProperties().Select(p => p.Name.ToLower()).Where(p => p != "Id").ToArray();
            string stmnt = $"INSERT INTO {tableName}({string.Join(",", columnNames)}) VALUES({string.Join(",", columnNames.Select(c => $"@{c}"))})";

            await conn.ExecuteAsync(stmnt, value);
        }

        internal static async Task<T> SelectOneAsync<T>(this IDbConnection conn, string tableName, int id)
        {
            var res = await conn.QueryAsync<T>($"SELECT * FROM {tableName} WHERE id = @id", new { id = id });

            return res.FirstOrDefault();
        }

        internal static async Task<List<T>> SelectAllAsync<T>(this IDbConnection conn, string tableName, params string[]? fieldsToSelect)
        {
            var res = await conn.QueryAsync<T>($"SELECT {(fieldsToSelect is null || fieldsToSelect.Length == 0 ? "*" : String.Join(',', fieldsToSelect))} FROM {tableName}");

            return res.ToList();
        }

        internal static async Task<List<T>> CallResultFunctionAsync<T, U>(this IDbConnection conn, string fnName, U value)
        {
            string[] columnNames = typeof(U).GetProperties().Select(p => $"@{p.Name.ToLower()}").Where(p => p != "Id").ToArray();

            var res = await conn.QueryAsync<T>($"SELECT * FROM {fnName}({string.Join(",", columnNames)})", param: value);

            return res.ToList();
        }
    }
}
