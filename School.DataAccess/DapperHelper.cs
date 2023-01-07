using Dapper;

namespace School.DataAccess
{
    internal static class DapperHelper
    {
        public static async Task DeleteAsync(this IDbConnection conn, string tableName, int id)
        {
            await conn.ExecuteAsync($"DELETE FROM {tableName} WHERE id = @id", new { id = id });
        }

        public static async Task InsertAsync<T>(this IDbConnection conn, string tableName, T value)
        {
            string[] columnNames = typeof(T).GetProperties().Select(p => p.Name.ToLower()).Where(p => p != "Id").ToArray();
            string stmnt = $"INSERT INTO {tableName}({string.Join(",", columnNames)}) VALUES({string.Join(",", columnNames.Select(c => $"@{c}"))})";

            await conn.ExecuteAsync(stmnt, value);
        }

        public static async Task<T> SelectOneAsync<T>(this IDbConnection conn, string tableName, int id)
        {
            var res = await conn.QueryAsync<T>($"SELECT * FROM {tableName} WHERE id = @id", new { id = id });

            return res.FirstOrDefault();
        }

        public static async Task<List<T>> SelectAllAsync<T>(this IDbConnection conn, string tableName, params string[]? fieldsToSelect)
        {
            var res = await conn.QueryAsync<T>($"SELECT {(fieldsToSelect is null || fieldsToSelect.Length == 0 ? "*" : String.Join(',', fieldsToSelect))} FROM {tableName}");

            return res.ToList();
        }
    }
}
