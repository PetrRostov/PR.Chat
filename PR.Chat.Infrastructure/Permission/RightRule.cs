using System;
using System.Linq.Expressions;

namespace PR.Chat.Infrastructure
{
    public class RightRule : IEntity<RightRule, Guid>
    {
        public virtual Guid Id { get; protected set; }

        public virtual DateTime ExpiredAt { get; protected set; }

        public virtual Guid OwnerId { get; protected set; }

        public virtual IPermission Permission { get; protected set; }

        public virtual LambdaExpression CheckExpression { get; protected set; }

        protected RightRule()
        {
            //For NHibernate
        }

        public RightRule(Guid id, Guid ownerId, IPermission permission, LambdaExpression checkExpression, DateTime expiredAt)
        {
            Check.NotNull(permission, "permission");
            Check.NotNull(checkExpression, "checkExpression");

            ExpressionShouldBeLambdaWithRightArguments(checkExpression, permission);

            Id              = id;
            OwnerId         = ownerId;
            Permission      = permission;
            CheckExpression = checkExpression;
            ExpiredAt       = expiredAt;
        }

        private void ExpressionShouldBeLambdaWithRightArguments(LambdaExpression checkExpression, IPermission permission)
        {
            var checkExpressionType = permission.CheckExpressionType;


            if (checkExpressionType != checkExpression.GetType())
                throw new ArgumentException(string.Format(
                    "{0} should be lambda expression with type={1}. Permission: {2}.",
                    "checkExpression",
                    checkExpression.GetType(),
                    permission.Name
                ));
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