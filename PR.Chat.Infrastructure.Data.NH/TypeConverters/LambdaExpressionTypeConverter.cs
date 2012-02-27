using System;
using System.Data;
using System.Linq.Expressions;
using System.Xml.Linq;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using PR.Chat.Domain;
using PR.Chat.Infrastructure.ExpressionSerialization;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class LambdaExpressionTypeConverter : IUserType
    {

        private static readonly ExpressionSerializer ExpressionSerializer = new ExpressionSerializer(
            new TypeResolver(new[] { typeof(User).Assembly, typeof(IRepository<,>).Assembly})
        );

        #region Implementation of IUserType

        public new bool Equals(object x, object y)
        {
            bool returnvalue = false;
            if ((x != null) && (y != null))
            {
                returnvalue = x.Equals(y);
            }
            return returnvalue;
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var serializableExpression = NHibernateUtil.String.NullSafeGet(rs, names) as string;

            if (string.IsNullOrEmpty(serializableExpression))
                return null;

            var xmlExpression = XElement.Parse(serializableExpression);
            var expression = ExpressionSerializer.Deserialize(xmlExpression) as LambdaExpression;

            return expression;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var expression = value as LambdaExpression;

            NHibernateUtil.String.NullSafeSet(
                cmd, 
                ExpressionSerializer.Serialize(expression).ToString(),
                index
            );
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.GetString(10000)}; }
        }

        public Type ReturnedType
        {
            get { return typeof (LabelExpression); }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        #endregion
    }
}