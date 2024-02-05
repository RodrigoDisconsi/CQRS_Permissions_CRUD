using Dapper;
using CRUDCleanArchitecture.Infrastructure.Extensions;

namespace CRUDCleanArchitecture.Infrastructure.Repositories;
public class BaseRepository
{
    protected string AplicarBusqueda(string textoBusqueda, DynamicParameters parameters, Type t)
    {
        string queryCondicion = "";

        if (!string.IsNullOrWhiteSpace(textoBusqueda) && textoBusqueda.Length >= 3)
        {
          
            parameters.Add("@buscador", '%' + textoBusqueda + '%');
            queryCondicion += " WHERE (" + t.SearchCriteria("@buscador") + ")";
        }

        return queryCondicion + Environment.NewLine;
    }
}
