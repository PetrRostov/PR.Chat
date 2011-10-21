using System;
using System.Linq;

namespace PR.Chat.Infrastructure.Data
{
    public interface IDatabase : IDisposable
    {
        IQueryable<T> GetSource<T, TKey>() where T : class, IEntity<T, TKey>;

        void UpdateOnSubmit<T, TKey>(IEntity<T, TKey> entity);

        void DeleteOnSubmit<T, TKey>(IEntity<T, TKey> entity);

        TKey AddOnSubmit<T, TKey>(IEntity<T, TKey> entity);

        void Submit();
        void BeginTransaction();
    }
}