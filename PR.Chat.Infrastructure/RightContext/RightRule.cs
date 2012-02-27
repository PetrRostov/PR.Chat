using System;
using System.Linq.Expressions;

namespace PR.Chat.Infrastructure.RightContext
{
    public class RightRule : IEntity<RightRule, Guid>
    {
        public virtual Guid Id { get; protected set; }

        public virtual DateTime ExpiredAt { get; protected set; }

        public virtual Guid OwnerId { get; protected set; }

        public virtual string Permission { get; protected set; }

        public virtual LambdaExpression CheckExpression { get; protected set; }

        protected RightRule()
        {
            //For NHibernate
        }

        public RightRule(Guid id, Guid ownerId, string permission, LambdaExpression checkExpression, DateTime expiredAt)
        {
            Require.NotNullOrEmpty(permission, "permission");
            Require.NotNull(checkExpression, "checkExpression");

            Id              = id;
            OwnerId         = ownerId;
            Permission      = permission;
            CheckExpression = checkExpression;
            ExpiredAt       = expiredAt;
        }

        public virtual bool SameIdentityAs(RightRule other)
        {
            return other != null && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
                return true;

            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return SameIdentityAs((RightRule)obj);
        }
    }
}