using System;

namespace PR.Chat.Infrastructure
{
    public interface IPermission
    {
        string Name { get; }

        Type CheckExpressionType { get; }
    }
}