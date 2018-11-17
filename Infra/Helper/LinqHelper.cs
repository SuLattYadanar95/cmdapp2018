using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public static class LinqHelper<T>
    {
        public static IQueryable<T> Sort(IQueryable<T> source, string orderbyfield, string Direction = "ASC")
        {
            var type = typeof(T);
            var property = type.GetProperty(orderbyfield);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            if (Direction == "ASC")
            {
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                return source.Provider.CreateQuery<T>(resultExp);
            }
            else
            {
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                return source.Provider.CreateQuery<T>(resultExp);
            }
        }
    }
}
