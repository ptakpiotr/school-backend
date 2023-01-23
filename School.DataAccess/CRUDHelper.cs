using School.DataAccess.Exceptions;

namespace School.DataAccess;

/// <summary>
/// Klasa wewnętrzna zapewniająca opakowaną funkcjonalność CRUD
/// </summary>
internal static class CRUDHelper
{
    /// <summary>
    /// Metoda dodająca nowy rekord do tabeli
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="connString"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDatabaseOperationException"></exception>
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
            throw new InvalidDatabaseOperationException();
        }
    }

    /// <summary>
    /// Metoda usuwająca rekord z tabeli po ID
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connString"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDatabaseOperationException"></exception>
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
            throw new InvalidDatabaseOperationException();
        }
    }

    /// <summary>
    /// Metoda pobierająca listę wszystkich wartości
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connString"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDatabaseOperationException"></exception>
    internal static async Task<List<T>> GetAll<T>(string connString, bool sortById = true)
    {
        bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
        if (success && tableNames is not null)
        {
            using (IDbConnection conn = new NpgsqlConnection(connString))
            {
                return await conn.SelectAllAsync<T>(tableNames.View, sortById: sortById);
            }
        }
        else
        {
            throw new InvalidDatabaseOperationException();
        }
    }

    /// <summary>
    /// Metoda pobierająca jeden rekord (bazując na ID)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connString"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDatabaseOperationException"></exception>
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
            throw new InvalidDatabaseOperationException();
        }
    }

    /// <summary>
    /// Metoda uaktualniająca
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connString"></param>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDatabaseOperationException"></exception>
    internal static async Task Update<T>(string connString, int id, T value)
    {
        bool success = ModelDTODatabaseMappings.mappings.TryGetValue(typeof(T), out TableNames tableNames);
        if (success && tableNames is not null)
        {
            using (IDbConnection conn = new NpgsqlConnection(connString))
            {
                await conn.UpdateAsync(tableNames.Original, id, value);
            }
        }
        else
        {
            throw new InvalidDatabaseOperationException();
        }
    }
}
