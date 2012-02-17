// ------------------------------------------------
// This code was taken from LinqSpecs project at
// http://linqspecs.codeplex.com/
// ------------------------------------------------
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Linq;
using PR.Chat.Infrastructure.ExpressionSerialization;
using PR.Chat.Infrastructure.LinqSpecs.ExpressionSerialization;

namespace PR.Chat.Infrastructure.LinqSpecs
{
    [Serializable]
    public class AdHocSpecification<T> : Specification<T>
    {
        [NonSerialized]
        private Expression<Func<T, bool>> specification;
        private readonly String serializedExpressionXml;

        public AdHocSpecification(Expression<Func<T, bool>> specification)
        {

            //var cleanedExpression = ExpressionUtility.Ensure(specification);

            this.specification = specification;
            //var serializer = new ExpressionSerializer();
            //var serializedExpression = serializer.Serialize(specification);
            //serializedExpressionXml = serializedExpression.ToString();
        }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            if (specification == null)
            {
                var serializer = new ExpressionSerializer();
                var serializedExpression = XElement.Parse(serializedExpressionXml);
                specification = serializer.Deserialize<Func<T, bool>>(serializedExpression);    
            }
            
            return specification;
        }
        
    }
}