using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace KvantShared.Repos
{
    public static class RepoExtensions
    {
        public static IQueryable<T> AsTracking<T>(this IQueryable<T> set, bool tracking)
            where T : class =>
            tracking ? set : set.AsNoTracking();

        public static IQueryable<T> Include<T>(
            this IQueryable<T> set,
            params Expression<Func<T, object>>[] include)
            where T : class
        {
            if (include == null)
                return set;
            var result = include.Aggregate(set, (current, includeExpression) => current.Include(includeExpression));

            return result;
        }

        public static IOrderedQueryable<T> OrderBy<T, TKey>(
            this IQueryable<T> source,
            Expression<Func<T, TKey>> sortExpression,
            bool ascending)
            where T : class
        {
            return ascending ? source.OrderBy(sortExpression) : source.OrderByDescending(sortExpression);
        }
    }



}
