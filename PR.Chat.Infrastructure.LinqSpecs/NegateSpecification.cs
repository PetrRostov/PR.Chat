// ------------------------------------------------
// This code was taken from LinqSpecs project at
// http://linqspecs.codeplex.com/
// ------------------------------------------------
using System;
using System.Linq.Expressions;

namespace PR.Chat.Infrastructure.LinqSpecs
{
	[Serializable]
	public class NegateSpecification<T> : Specification<T>
	{
		private readonly Specification<T> spec;

		public NegateSpecification(Specification<T> spec)
		{
			this.spec = spec;
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			var isSatisfiedBy = spec.IsSatisfiedBy();
			return Expression.Lambda<Func<T, bool>>(
				Expression.Not(isSatisfiedBy.Body), isSatisfiedBy.Parameters);
		}

		protected override object[] Parameters
		{
			get
			{
				return new[] {spec};
			}
		}
	}
}