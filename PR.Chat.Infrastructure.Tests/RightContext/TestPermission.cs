using System;
using System.Linq.Expressions;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    public class TestPermission : IPermission
    {
        public static readonly IPermission Current = new TestPermission();

        private TestPermission()
        {
            
        }

        #region Implementation of IPermission

        public string Name
        {
            get { return "TestName"; }
        }

        public Type CheckExpressionType
        {
            get { return typeof (Expression<Func<int, string, ObjectOneForRightContext, bool>>); }
        }

        #endregion
    }
}