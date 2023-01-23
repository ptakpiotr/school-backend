using Dapper;
using School.DataAccess.Exceptions;

namespace School.DataAccess;

/// <summary>
/// Zbiór tzw. extension methods ułatwiających pracę z SQL
/// </summary>
internal static class DapperHelper
{
    internal static async Task DeleteAsync(this IDbConnection conn, string tableName, int id, string expr = "")
    {
        string sql = $"DELETE FROM {tableName} WHERE id = @id {(!string.IsNullOrEmpty(expr) ? $"AND {expr}" : "")}";
        await conn.ExecuteAsync(sql, new { id = id });
    }

    internal static async Task InsertAsync<T>(this IDbConnection conn, string tableName, T value)
    {
        string[] columnNames = typeof(T).GetProperties().Select(p => p.Name.ToLower()).Where(p => p.ToLower() != "id").ToArray();
        string stmnt = $"INSERT INTO {tableName}({string.Join(",", columnNames)}) VALUES({string.Join(",", columnNames.Select(c => $"@{c}"))})";
        try
        {
            await conn.ExecuteAsync(stmnt, value);
        }
        catch (Exception ex)
        {
            throw new InvalidDatabaseOperationException(ex.Message);
        }
    }

    internal static async Task<T> SelectOneAsync<T>(this IDbConnection conn, string tableName, int id, string expr = "")
    {
        string sql = $"SELECT * FROM {tableName} WHERE id = @id {(!string.IsNullOrEmpty(expr) ? $"AND {expr}" : "")}";
        var res = await conn.QueryAsync<T>(sql, new { id = id });

        return res.FirstOrDefault();
    }

    internal static async Task<List<T>> SelectAllAsync<T>(this IDbConnection conn, string tableName, string expr = "", bool sortById = true, params string[]? fieldsToSelect)
    {
        string sql = $"SELECT {(fieldsToSelect is null || fieldsToSelect.Length == 0 ? "*" : String.Join(',', fieldsToSelect))} FROM {tableName} {(!string.IsNullOrEmpty(expr) ? $"WHERE {expr}" : "")} {(sortById ? "ORDER BY id ASC" : string.Empty)}";
        var res = await conn.QueryAsync<T>(sql);

        return res.ToList();
    }

    internal static async Task<List<T>> CallResultFunctionAsync<T, U>(this IDbConnection conn, string fnName, U value, string expr = "")
    {
        string[] columnNames = typeof(U).GetProperties().Select(p => $"@{p.Name.ToLower()}").Where(p => p.ToLower() != "id").ToArray();
        string sql = $"SELECT * FROM {fnName}({string.Join(",", columnNames)}) {(!string.IsNullOrEmpty(expr) ? $"WHERE {expr}" : "")}";
        var res = await conn.QueryAsync<T>(sql, param: value);

        return res.ToList();
    }

    internal static async Task CallExecuteFunctionAsync<T>(this IDbConnection conn, string fnName, T value)
    {
        await conn.QueryAsync(fnName, param: value, commandType: CommandType.StoredProcedure, commandTimeout: 900);
    }

    internal static async Task UpdateAsync<T>(this IDbConnection conn, string tableName, int id, T value)
    {
        string[] colsToUpdate = typeof(T).GetProperties().Select(p => $"{p.Name.ToLower()} = @{p.Name.ToLower()}").Where(p => !p.StartsWith("id")).ToArray();
        string sql = $"UPDATE {tableName} SET {string.Join(",", colsToUpdate)} WHERE id = {id}";

        await conn.ExecuteAsync(sql, value);
    }
}
