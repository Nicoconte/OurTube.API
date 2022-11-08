using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OurTube.API.Extensions
{
    public static class LinqExtension
    {
        public async static Task<bool> CheckIf<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> expresion)
        {
            return await source.FirstOrDefaultAsync(expresion) != null;
        }
    }
}
