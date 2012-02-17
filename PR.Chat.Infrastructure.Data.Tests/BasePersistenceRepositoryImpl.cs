using System;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    public class BasePersistenceRepositoryImpl : BasePersistenceRepository<TestEntity, Guid>
    {
        public BasePersistenceRepositoryImpl(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public BasePersistenceRepositoryImpl(IDatabase database) : base(database)
        {
        }
    }
}