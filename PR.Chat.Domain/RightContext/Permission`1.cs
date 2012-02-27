using System;
using System.Linq.Expressions;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.Enumeration;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Domain
{
    public class Permission<TExpression> : IPermission
        where TExpression : LambdaExpression
    {
        private readonly string _name;

        public string Name 
        { 
            get { return _name; }
        }

        public Type CheckExpressionType
        {
            get { return typeof (TExpression); }
        }

        internal Permission(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var objPermission = obj as IPermission;

            if (objPermission == null)
                return false;

            return _name.Equals(objPermission.Name);
        }
    }
}