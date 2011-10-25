using System;
using PR.Chat.Infrastructure.UnitOfWork;

namespace PR.Chat.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabase _database;
        private bool _isDisposed;

        public UnitOfWork(IDatabase database)
        {
            Check.NotNull(database, "database");
            _database = database;
            _database.BeginTransaction();
        }

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            Check.NotNull(databaseFactory, "databaseFactory");
            _database = databaseFactory.Create();
        }

        public void Dispose()
        {
            if (!_isDisposed)
                _isDisposed = true;
        }

        public void Commit()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            _database.Submit();

            Dispose();
        }
    }
}