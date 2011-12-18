using System;
using System.Linq;
using PR.Chat.Domain;

namespace PR.Chat.Infrastructure.Data
{
    public class NickRepository : BaseRepository<Nick, Guid>, INickRepository
    {
        public NickRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public NickRepository(IDatabase database) : base(database)
        {
        }

        public Nick GetByName(string nickName)
        {
            var nick = GetSource()
                .Where(NickSpecification.NameEquals(nickName).IsSatisfiedBy())
                .FirstOrDefault();

            if (nick == null)
                ExceptionHelper.EntityNotFound<Nick>("Name", nickName);

            return nick;
        }

        public bool ExistsWithName(string nickName)
        {
            var nick = GetSource()
                .Where(NickSpecification.NameEquals(nickName).IsSatisfiedBy())
                .FirstOrDefault();

            return nick != null;
        }
    }
}