using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CRUDCleanArchitecture.Domain.Attributes;
using CRUDCleanArchitecture.Domain.Exceptions;

namespace CRUDCleanArchitecture.Infrastructure.Extensions;
public static class SercheableExtensions
{
    /// <summary>
    /// Concatena todas las propiedades del tipo objType con sintaxis sql
    /// que permite ser incluido en una clausula where (el operador logico utilizado es "OR" si la entidad tiene mas de una propiedad)
    /// </summary>
    /// <param name="objType">Tipo a partir del que se obtendran todas las propiedades</param>
    /// <param name="varBuscador">Nombre de la variable SQL a utilizar</param>
    /// <returns>String con sintaxis sql compatible, contiene todas las propiedades del tipo especificado en objType concatenadas</returns>
    public static string SearchCriteria(this Type objType, string varBuscador)
    {
        ///lista con todas las propiedades del tipo objType
        List<PropertyInfo> _properties = objType?.GetTypeInfo().DeclaredProperties.ToList();
        if (_properties != null && _properties.Count > 0)
        {
            StringBuilder query = new StringBuilder();

            List<string> strPropLike = new List<string>();

            _properties.Where(p => p.GetCustomAttribute(typeof(BuscadorAttribute)) != null).ToList().ForEach(value =>
            {
                if (value.PropertyType.Equals(typeof(DateTime)) || value.PropertyType.Equals(typeof(DateTime?)))
                {
                    strPropLike.Add($" convert(varchar, {value.Name}, 3)" + " like " + varBuscador);
                    strPropLike.Add($" [dbo].[fn_ConvertDate] ({value.Name},'spanish')" + " like " + varBuscador);
                }
                else
                    strPropLike.Add(value.Name + " like " + varBuscador);
            });

            query.AppendJoin(" or ", strPropLike);
            return query.ToString();
        }

        ///la siguiente excepcion se produce por falta de propiedades en la entidad del tipo especificado o porque el object Type es null
        throw new ArgumentExpectedException("objType no es un tipo valido, null o no contiene propiedades");
    }
}
