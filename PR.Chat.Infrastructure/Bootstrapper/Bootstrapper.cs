using System.Linq;

namespace PR.Chat.Infrastructure
{
    public static class Bootstrapper
    {
         public static void Run()
         {
             IoC
                 .ResolveAll<IBootstrapperTask>()
                 .ToList()
                 .ForEach(t => t.Run());
         }
    }
}