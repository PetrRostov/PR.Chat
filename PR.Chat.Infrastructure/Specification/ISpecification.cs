using System;
using System.Linq.Expressions;

namespace PR.Chat.Infrastructure
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy();
    }
}