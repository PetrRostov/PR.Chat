using System.Diagnostics;

namespace PR.Chat.Infrastructure.UnitOfWork
{
    public static class UnitOfWork
    {
        [DebuggerStepThrough]
        public static IUnitOfWork Start()
        {
            return IoC.Resolve<IUnitOfWork>();
        }

        [DebuggerStepThrough]
        public static IUnitOfWork StartAsync()
        {
            return IoC.Resolve<IUnitOfWork>("SingletonUnitOfWork");
        }
    }
}