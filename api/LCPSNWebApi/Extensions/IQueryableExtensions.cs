using System.Linq.Expressions;
using LCPSNWebApi.Classes;

namespace LCPSNWebApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool desc)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            return desc ? Queryable.OrderByDescending(source, (dynamic)lambda) : Queryable.OrderBy(source, (dynamic)lambda);
        }
    }
}