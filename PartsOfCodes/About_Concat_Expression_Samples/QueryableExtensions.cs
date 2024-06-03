using System.Linq;
using System;
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

        /// <summary>
        /// 未测试，untested
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(IQueryable<T> source, string propertyName, object value)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            MemberExpression member = Expression.PropertyOrField(param, propertyName);
            ConstantExpression constant = Expression.Constant(value);
            BinaryExpression binary = Expression.Equal(member, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(binary, param);

            return source.Where(lambda);
        }
    }
}
