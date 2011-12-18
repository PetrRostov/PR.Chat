using System;

namespace PR.Chat.Application
{
    public class MappingNotExistsException : Exception
    {
        public MappingNotExistsException(Type from, Type to)
            : base(string.Format("Mapping from class {0} to {1} not exists", from.FullName, to.FullName))
        {
            
        }
    }
}