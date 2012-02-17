using System;

namespace PR.Chat.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PermissionDimensionAttribute : Attribute
    {
        public ArgumentPosition Position { get; private set; }
        public bool IsAttributeOwner { get; set; }
        public bool IsCurrentExecutor { get; set; }

        public PermissionDimensionAttribute(
            ArgumentPosition position
        )
        {
            Position = position;
            IsAttributeOwner   = false;
            IsCurrentExecutor  = false;
        }
    }
}