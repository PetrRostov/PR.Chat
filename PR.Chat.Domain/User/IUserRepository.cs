using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
   public interface IUserRepository : IRepository<User, Guid>
   {

   }
}