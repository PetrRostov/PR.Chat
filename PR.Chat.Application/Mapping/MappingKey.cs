using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Application
{
    internal class MappingKey
    {
        public MappingKey(Type from, Type to)
        {
            Require.NotNull(from, "from");
            Require.NotNull(to, "to");

            From    = from;
            To      = to;
        }

        internal Type From;
        internal Type To;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var mapping = obj as MappingKey;

            return mapping.From.Equals(From) && mapping.To.Equals(To);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + From.GetHashCode();
            hash = (hash * 7) + To.GetHashCode();

            return hash;
        }
    }
}