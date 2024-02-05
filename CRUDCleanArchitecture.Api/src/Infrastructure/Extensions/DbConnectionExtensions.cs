using Dapper;
using CRUDCleanArchitecture.Application.Common.Models;
using System.Data;

namespace CRUDCleanArchitecture.Infrastructure.Extensions;
public static class DbConnectionExtensions
{
    public static async Task<PaginatedList<T>?> PaginatedListAsync<T>(this IDbConnection conn,
        string query,
        DynamicParameters parameters,
        int pageIndex,
        int pageSize) where T : class
    {
        var indiceOrder = query.ToUpper().IndexOf("ORDER BY");
        var queryCount = query;
        if (indiceOrder > 0)
            queryCount = queryCount.Replace(query.Substring(indiceOrder, query.Length - indiceOrder), "");
        queryCount = queryCount.Replace(query.Substring(0, query.ToUpper().IndexOf("FROM ")), "SELECT COUNT(*) ");
        var count = await conn.ExecuteScalarAsync<int>(queryCount, parameters);
        var pageQuery = $"{query} OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        var items = await conn.QueryAsync<T>(pageQuery, parameters);

        return new PaginatedList<T>(items?.ToList(), count, pageIndex, pageSize);
    }

}
