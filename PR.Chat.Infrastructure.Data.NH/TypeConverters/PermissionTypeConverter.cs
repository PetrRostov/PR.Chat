using System;
using System.Data;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using PR.Chat.Domain;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Data.NH
{
    //public class PermissionTypeConverter : IUserType
    //{
    //    #region Implementation of IUserType

    //    public new bool Equals(object x, object y)
    //    {
    //        bool returnvalue = false;
    //        if ((x != null) && (y != null))
    //        {
    //            returnvalue = x.Equals(y);
    //        }
    //        return returnvalue;
    //    }

    //    public int GetHashCode(object x)
    //    {
    //        return x.GetHashCode();
    //    }

    //    public object NullSafeGet(IDataReader rs, string[] names, object owner)
    //    {
    //        var permissionName = NHibernateUtil.String.NullSafeGet(rs, names) as string;

    //        return Domain.Permission.GetByName(permissionName);
    //    }

    //    public void NullSafeSet(IDbCommand cmd, object value, int index)
    //    {
    //        var permission = value as IPermission;
    //        var name = permission != null ? permission.Name : null;

    //        NHibernateUtil.String.NullSafeSet(cmd, name, index);
    //    }

    //    public object DeepCopy(object value)
    //    {
    //        return value;
    //    }

    //    public object Replace(object original, object target, object owner)
    //    {
    //        return original;
    //    }

    //    public object Assemble(object cached, object owner)
    //    {
    //        return DeepCopy(cached);
    //    }

    //    public object Disassemble(object value)
    //    {
    //        return DeepCopy(value);
    //    }

    //    public SqlType[] SqlTypes
    //    {
    //        get { return new[] { SqlTypeFactory.GetString(50) }; }
    //    }

    //    public Type ReturnedType
    //    {
    //        get { return typeof(IPermission); }
    //    }

    //    public bool IsMutable
    //    {
    //        get { return false; }
    //    }

    //    #endregion
    //}
}