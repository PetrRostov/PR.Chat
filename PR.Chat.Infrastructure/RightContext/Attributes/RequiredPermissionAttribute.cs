using System;

namespace PR.Chat.Infrastructure.RightContext
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RequiredPermissionAttribute : Attribute
    {
        protected readonly ArgumentPosition _ruleHolderArgumentPosition;
        protected readonly string[] _arguments;
        protected RuleHolder _ruleHolder;
        protected readonly string _requiredPermission;

        public string[] Arguments
        {
            get { return _arguments; }
        }

        public ArgumentPosition RuleHolderArgumentPosition
        {
            get { return _ruleHolderArgumentPosition; }
        }

        
        public RuleHolder RuleHolder
        {
            get { return _ruleHolder; }
        }

        
        public string RequiredPermission
        {
            get { return _requiredPermission; }
        }

        public RequiredPermissionAttribute(string requiredPermission)
            : this(requiredPermission, RuleHolder.MethodOwner, ArgumentPosition.WithoutPosition)
        {
            
        }

        public RequiredPermissionAttribute(string requiredPermission, params string[] arguments)
            : this(requiredPermission, RuleHolder.MethodOwner, ArgumentPosition.WithoutPosition, arguments)
        {

        }

        public RequiredPermissionAttribute(
            string requiredPermission, 
            RuleHolder ruleHolder, 
            ArgumentPosition ruleHolderArgumentPosition,
            params string[] arguments
        )
        {
            _ruleHolderArgumentPosition     = ruleHolderArgumentPosition;
            _arguments                      = arguments;
            _ruleHolder                     = ruleHolder;
            _requiredPermission             = requiredPermission;
        }
    }
}