using System;
using PR.Chat.Domain;

namespace PR.Chat.Infrastructure.Data
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public UserRepository(IDatabase database) : base(database)
        {
        }
    }
}