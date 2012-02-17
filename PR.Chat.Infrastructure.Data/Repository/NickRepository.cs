using System;
using System.Linq;
using PR.Chat.Domain;

namespace PR.Chat.Infrastructure.Data
{
    public class NickRepository : BasePersistenceRepository<Nick, Guid>, INickRepository
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
                .Where(NickSpecifications.NameMustEquals(nickName))
                .FirstOrDefault();

            if (nick == null)
                ExceptionHelper.EntityNotFound<Nick>("Name", nickName);

            return nick;
        }

        public bool ExistsWithName(string nickName)
        {
            return  GetSource()
                .Where(NickSpecifications.NameMustEquals(nickName))
                .Any();
        }
    }
}