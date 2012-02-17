using System;
using System.Diagnostics;
using System.Reflection;

namespace PR.Chat.Infrastructure.Castle
{
    public class CastleInvocationAdapter: IInvocation
    {
        private readonly global::Castle.DynamicProxy.IInvocation _innerInvocation;

        public CastleInvocationAdapter(global::Castle.DynamicProxy.IInvocation innerInvocation)
        {
            _innerInvocation = innerInvocation;
        }

        #region Implementation of IInvocation

        public object[] Arguments
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.Arguments; }
        }

        public Type[] GenericArguments
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.GenericArguments; }
        }

        public object InvocationTarget
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.InvocationTarget; }
        }

        public MethodInfo Method
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.Method; }
        }

        public MethodInfo MethodInvocationTarget
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.MethodInvocationTarget; }
        }

        public object Proxy
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.Proxy; }
        }

        public object ReturnValue
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.ReturnValue; }
            [DebuggerStepThrough]
            set { _innerInvocation.ReturnValue = value; }
        }

        public Type TargetType
        {
            [DebuggerStepThrough]
            get { return _innerInvocation.TargetType; }
        }

        [DebuggerStepThrough]
        public object GetArgumentValue(int index)
        {
            return _innerInvocation.GetArgumentValue(index);
        }

        [DebuggerStepThrough]
        public MethodInfo GetConcreteMethod()
        {
            return _innerInvocation.GetConcreteMethod();
        }

        [DebuggerStepThrough]
        public MethodInfo GetConcreteMethodInvocationTarget()
        {
            return _innerInvocation.GetConcreteMethodInvocationTarget();
        }

        [DebuggerStepThrough]
        public void Proceed()
        {
            _innerInvocation.Proceed();
        }

        [DebuggerStepThrough]
        public void SetArgumentValue(int index, object value)
        {
            _innerInvocation.SetArgumentValue(index, value);
        }

        #endregion
    }
}