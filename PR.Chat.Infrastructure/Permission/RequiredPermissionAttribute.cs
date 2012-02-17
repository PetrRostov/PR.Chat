using System;

namespace PR.Chat.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RequiredPermissionAttribute : Attribute
    {
        private readonly IPermission _requiredPermission;
        public IPermission RequiredPermission
        {
            get { return _requiredPermission; }
        }

        public RequiredPermissionAttribute(IPermission requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public override bool Match(object obj)
        {
            if (ReferenceEquals(obj, this))
                return true;

            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var objAttribute = (RequiredPermissionAttribute) obj;

            return RequiredPermission.Equals(objAttribute.RequiredPermission);
        }
    }
}