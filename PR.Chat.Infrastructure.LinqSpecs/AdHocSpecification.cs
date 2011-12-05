// ------------------------------------------------
// This code was taken from LinqSpecs project at
// http://linqspecs.codeplex.com/
// ------------------------------------------------
using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using PR.Chat.Infrastructure.LinqSpecs.ExpressionSerialization;

namespace PR.Chat.Infrastructure.LinqSpecs
{
	[Serializable]
	public class AdHocSpecification<T> : Specification<T>
	{
		private readonly Expression<Func<T, bool>> specification;
	    private readonly String serializedExpressionXml;

		public AdHocSpecification(Expression<Func<T, bool>> specification)
		{

		    this.specification = specification;
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
		    return specification;
		}
	}
}