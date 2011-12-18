using PR.Chat.Domain;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.UnitOfWork;

namespace PR.Chat.Presentation.Web.Core
{
    public class FillStartValuesBootstrapperTask : IBootstrapperTask
    {
        public void Run()
        {
            var userRepository = IoC.Resolve<IUserRepository>("SingletonUserRepository");
            var userFactory     = IoC.Resolve<IUserFactory>();

            using (var unitOfWork = UnitOfWork.StartAsync())
            {
                var user = userFactory.CreateUnregistered();
                userRepository.Add(user);

                unitOfWork.Commit();
            }
        }
    }
}