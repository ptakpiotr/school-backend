namespace School.DataAccess
{
    internal static class CRUDHelper
    {
        internal static async Task Add<T, U>(string connString, U value)
        {
            bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
            if (success && tableNames is not null)
            {
                using (IDbConnection conn = new NpgsqlConnection(connString))
                {
                    await conn.InsertAsync(tableNames.Original, value);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        internal static async Task Delete<T>(string connString, int id)
        {
            bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
            if (success && tableNames is not null)
            {
                using (IDbConnection conn = new NpgsqlConnection(connString))
                {
                    await conn.DeleteAsync(tableNames.Original, id);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        internal static async Task<List<T>> GetAll<T>(string connString)
        {
            bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
            if (success && tableNames is not null)
            {
                using (IDbConnection conn = new NpgsqlConnection(connString))
                {
                    return await conn.SelectAllAsync<T>(tableNames.View);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        internal static async Task<T> GetOne<T>(string connString, int id)
        {
            bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
            if (success && tableNames is not null)
            {
                using (IDbConnection conn = new NpgsqlConnection(connString))
                {
                    return await conn.SelectOneAsync<T>(tableNames.View, id);
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
