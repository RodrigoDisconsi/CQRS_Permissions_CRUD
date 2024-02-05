using Dapper;
using System.Data;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.Dapper;
public class DapperBaseRepository
{
    private readonly IDbConnection _connection;

    public DapperBaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public List<T> Query<T>(string query, object parameters = null)
    {
        try
        {
            return _connection.Query<T>(query, parameters).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<T>();
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
    {
        try
        {
            return await _connection.QueryAsync<T>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<T>();
        }
    }

    public T QueryFirst<T>(string query, object parameters = null)
    {
        try
        {
            return _connection.QueryFirst<T>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }

    public T QueryFirstOrDefault<T>(string query, object parameters = null)
    {
        try
        {
            return _connection.QueryFirstOrDefault<T>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }

    public T QuerySingle<T>(string query, object parameters = null)
    {
        try
        {
            return _connection.QuerySingle<T>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }

    public T QuerySingleOrDefault<T>(string query, object parameters = null)
    {
        try
        {
            return _connection.QuerySingleOrDefault<T>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }
}
