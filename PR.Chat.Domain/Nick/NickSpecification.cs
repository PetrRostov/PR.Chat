using System;
using PR.Chat.Infrastructure.LinqSpecs;

namespace PR.Chat.Domain
{
    public static class NickSpecification
    {
         public static Specification<Nick> NameEquals(string nickName)
         {
             var nickNameLower = nickName.ToLowerInvariant();
             return new AdHocSpecification<Nick>(
                 n => n.Name.ToLowerInvariant().Equals(nickNameLower)
             );
         }
    }
}