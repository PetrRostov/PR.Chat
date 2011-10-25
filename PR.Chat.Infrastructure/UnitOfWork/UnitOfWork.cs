using System.Diagnostics;

namespace PR.Chat.Infrastructure.UnitOfWork
{
    public class UnitOfWork
    {
        [DebuggerStepThrough]
        public IUnitOfWork Start()
        {
            return DependencyResolver.Resolve<IUnitOfWork>();
        }
    }
}