using System.Diagnostics;

namespace PR.Chat.Infrastructure
{
    public static class ExecutionContext
    {
        public static object Executor 
        {
            [DebuggerStepThrough]
            get { return GetContext().Executor; }
        }

        private static IExecutionContext GetContext()
        {
            return IoC.Resolve<IExecutionContext>();
        }
    }
}