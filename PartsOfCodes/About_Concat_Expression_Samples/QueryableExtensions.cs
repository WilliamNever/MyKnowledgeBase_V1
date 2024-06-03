using System.Linq.Expressions;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// EF db Queryable OrderBy/OrderByDescending
    /// </summary>
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            PropertyInfo property = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            MemberExpression propertyAccess = Expression.MakeMemberAccess(param, property);

            LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, param);
            MethodCallExpression resultExp = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new Type[] { typeof(T), property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            var parameter = Expression.Parameter(typeof(T), "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            MethodCallExpression resultExp = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                new Type[] { typeof(T), property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
