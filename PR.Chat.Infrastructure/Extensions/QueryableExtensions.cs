using System.Linq;

namespace PR.Chat.Infrastructure
{
    public static class QueryableExtensions
    {
         public static IQueryable<T> Where<T>(this IQueryable<T> source, ISpecification<T> specification)
         {
             return source.Where(specification.IsSatisfiedBy());
         }
    }
}