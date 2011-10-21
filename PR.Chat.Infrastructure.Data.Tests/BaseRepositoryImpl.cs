using System;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    public class BaseRepositoryImpl : BaseRepository<TestEntity, Guid>
    {
        public BaseRepositoryImpl(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public BaseRepositoryImpl(IDatabase database) : base(database)
        {
        }
    }
}