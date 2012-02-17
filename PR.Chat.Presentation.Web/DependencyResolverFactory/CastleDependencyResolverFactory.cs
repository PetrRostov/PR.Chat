using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using PR.Chat.Application;
using PR.Chat.Domain;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.Castle;
using PR.Chat.Infrastructure.Data;
using PR.Chat.Infrastructure.Data.NH;
using PR.Chat.Infrastructure.UnitOfWork;
using PR.Chat.Presentation.Services;
using PR.Chat.Presentation.Web.Core;
using MembershipService = PR.Chat.Application.MembershipService;

namespace PR.Chat.Presentation.Web
{
    public class CastleDependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly IList<IRegistration> _registrations;

        public CastleDependencyResolverFactory()
        {
            _registrations = new List<IRegistration>();

            RegisterDatabase().ToList().ForEach(_registrations.Add);
            RegisterUnitOfWork().ToList().ForEach(_registrations.Add);
            RegisterRepositories().ToList().ForEach(_registrations.Add);
            RegisterFactories().ToList().ForEach(_registrations.Add);
            RegisterMappers().ToList().ForEach(_registrations.Add);
            RegisterApplicationServices().ToList().ForEach(_registrations.Add);
            RegisterDomainServices().ToList().ForEach(_registrations.Add);
            RegisterBootstrapperTasks().ToList().ForEach(_registrations.Add);
            RegisterWebServiceHostFactory().ToList().ForEach(_registrations.Add);
            RegisterWebServices().ToList().ForEach(_registrations.Add);
        }

        public IDependencyResolver Create()
        {
            var windsorContainer = new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(_registrations.ToArray()); ;

            return new WindsorContainerAdapter(windsorContainer);
        }

        private static IEnumerable<IRegistration> RegisterWebServiceHostFactory()
        {
            yield return Component
                .For<ServiceHostFactoryBase>()
                .ImplementedBy<DefaultServiceHostFactory>()
                .LifestyleTransient()
                .UsingFactoryMethod(
                    (kernel, creationContext) => new DefaultServiceHostFactory(kernel)
                );
            
        }
        

        private static IEnumerable<IRegistration> RegisterWebServices()
        {
            
            yield return Component
                .For<Services.ITest>()
                .ImplementedBy<Services.Test>()
                .AsWcfService(
                    new DefaultServiceModel().Hosted()
                )
                .LifestylePerWcfOperation();

            yield return Component
                .For<Presentation.Services.IMembershipService>()
                .ImplementedBy<Presentation.Services.MembershipService>()
                .AsWcfService(
                    new DefaultServiceModel().Hosted()
                )
                .LifestylePerWcfOperation();

        }


        private static IEnumerable<IRegistration> RegisterBootstrapperTasks()
        {
            yield return Component
                .For<IBootstrapperTask>()
                .ImplementedBy<RegisterRoutesBootstrapperTask>()
                .Named("RegisterRoutesBootstrapperTask")
                .LifestyleTransient();

            yield return Component
                .For<IBootstrapperTask>()
                .ImplementedBy<RegisterServiceRoutesBootstrapperTask>()
                .Named("RegisterServiceRoutesBootstrapperTask")
                .LifestyleTransient();

            //yield return Component
            //    .For<IBootstrapperTask>()
            //    .ImplementedBy<FillStartValuesBootstrapperTask>()
            //    .Named("FillStartValuesBootstrapperTask")
            //    .LifestyleTransient();
        }

        private static IEnumerable<IRegistration> RegisterDomainServices()
        {
            yield return Classes.FromAssembly(typeof(Domain.MembershipService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .WithServiceDefaultInterfaces()
                .LifestylePerWcfOperation();
        }

        private static IEnumerable<IRegistration> RegisterApplicationServices()
        {
            yield return Classes.FromAssembly(typeof(MembershipService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .WithServiceDefaultInterfaces()
                .LifestylePerWcfOperation();
        }

        private static IEnumerable<IRegistration> RegisterMappers()
        {
            yield return Component
                .For<IMappingProvider>()
                .ImplementedBy<MappingProvider>()
                .LifestyleSingleton();
        }

        private static IEnumerable<IRegistration> RegisterDatabase()
        {
            yield return Component
                .For<IConnectionStringProvider>()
                .ImplementedBy<ConnectionStringProvider>()
                .LifestyleSingleton()
                .DependsOn(Dependency.OnValue("connectionStringName", "ChatDB"));

            yield return Component
                .For<INHibernateDatabaseConfigurator>()
                .ImplementedBy<MsSql2008NHibernateDatabaseConfigurator>()
                .Named("MsSql2008NHibernateDatabaseConfigurator")
                .LifestyleSingleton();

            yield return Component
                .For<IDatabaseFactory>()
                .ImplementedBy<PerSessionFactoryNHibernateDatabaseFactory>()
                .DependsOn(Dependency.OnComponent(typeof(INHibernateDatabaseConfigurator), "MsSql2008NHibernateDatabaseConfigurator"))
                .Named("DatabaseFactory")
                .LifestyleSingleton();

            yield return Component
                .For<IDatabaseFactory>()
                .ImplementedBy<PerSessionFactoryNHibernateDatabaseFactory>()
                .LifestyleSingleton()
                .Named("SingletonDatabaseFactory");

        }

        private static IEnumerable<IRegistration> RegisterUnitOfWork()
        {
            yield return Component
                .For<IUnitOfWork>()
                .ImplementedBy<Infrastructure.Data.UnitOfWork.UnitOfWork>()
                .DependsOn(Dependency.OnComponent(typeof(IDatabaseFactory), "DatabaseFactory"))
                .LifestyleTransient();

            yield return Component
                .For<IUnitOfWork>()
                .ImplementedBy<Infrastructure.Data.UnitOfWork.UnitOfWork>()
                .DependsOn(Dependency.OnComponent(typeof(IDatabaseFactory), "SingletonDatabaseFactory"))
                .LifestyleTransient()
                .Named("SingletonUnitOfWork");
        }

        private static IEnumerable<IRegistration> RegisterRepositories()
        {
            yield return Classes
                .FromAssembly(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .WithServiceDefaultInterfaces()
                .LifestylePerWcfOperation();

            yield return Component
                .For<IUserRepository>()
                .ImplementedBy<UserRepository>()
                .Named("SingletonUserRepository")
                .DependsOn(Dependency.OnComponent(typeof(IDatabaseFactory), "SingletonDatabaseFactory"))
                .LifestyleSingleton();
        }

        private static IEnumerable<IRegistration> RegisterFactories()
        {
            yield return Component
                .For<IUserFactory>()
                .ImplementedBy<UserFactory>()
                .LifestyleSingleton();

            yield return Component
                .For<IMembershipFactory>()
                .ImplementedBy<MembershipFactory>()
                .LifestyleSingleton();

        }
    }
}