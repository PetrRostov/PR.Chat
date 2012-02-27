using System;

namespace PR.Chat.Infrastructure.RightContext
{
    public interface IPermission
    {
        string Name { get; }

        Type CheckExpressionType { get; }
    }
}