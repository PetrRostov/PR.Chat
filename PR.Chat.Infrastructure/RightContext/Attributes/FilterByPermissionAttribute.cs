using System;

namespace PR.Chat.Infrastructure.RightContext
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class FilterByPermissionAttribute : RequiredPermissionAttribute
    {
        public FilterByPermissionAttribute(string requiredPermission) 
            : base(requiredPermission)
        {
        }

        public FilterByPermissionAttribute(string requiredPermission, params string[] arguments) 
            : base(requiredPermission, arguments)
        {
        }

        public FilterByPermissionAttribute(
            string requiredPermission, 
            RuleHolder ruleHolder, 
            ArgumentPosition ruleHolderArgumentPosition, 
            params string[] arguments
        ) 
            : base(requiredPermission, ruleHolder, ruleHolderArgumentPosition, arguments)
        {
        }

    }
}