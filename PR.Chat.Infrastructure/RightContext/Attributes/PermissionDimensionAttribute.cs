using System;

namespace PR.Chat.Infrastructure.RightContext
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
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