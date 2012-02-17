using System;
using System.Linq.Expressions;

namespace PR.Chat.Infrastructure.Data.NH.Tests
{
    public class ExpressionTest
    {
        private readonly LambdaExpression _lambdaExpression;

        private Guid _id;
        public virtual Guid Id
        {
            get { return _id; }
        }

        public virtual LambdaExpression LambdaExpression
        {
            get { return _lambdaExpression; }
        }

        protected ExpressionTest()
        {
            //For NHibernate
        }

        public ExpressionTest(LambdaExpression lambdaExpression)
        {
            _lambdaExpression = lambdaExpression;
        }
    }
}