using System;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Castle.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class WindsorContainerWrapperFixtures
    {
        private IWindsorContainer _windsorContainer;
        private WindsorContainerAdapter _adapterContainer;

        [SetUp]
        public void Init()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Register(
                Component
                    .For<IClassForInjection>()
                    .ImplementedBy<ClassForInjection>()
                    .LifeStyle.Singleton,
                Component
                    .For<IClassForInjection>()
                    .ImplementedBy<SecondClassForInjection>()
                    .Named("second")
                    .LifeStyle.Singleton,
                Component
                    .For<IClassForInjection>()
                    .ImplementedBy<ThirdClassForInjection>()
                    .Named("third")
                    .LifeStyle.PerThread
            );

            _adapterContainer = new WindsorContainerAdapter(_windsorContainer);
        }


        [Test]
        public void Constructor_should_work()
        {
            var windsorContainer   = new WindsorContainer();
            var container          = new WindsorContainerAdapter(windsorContainer);
        }

        [Test]
        public void Constructor_should_throw_exception_if_argument_null()
        {
            Assert.Throws<ArgumentNullException>(() => new WindsorContainerAdapter(null));
        }

        [Test]
        public void Register_should_work()
        {
            var forInjection = new ThirdClassForInjection();
            _adapterContainer.Register(forInjection);
            Assert.AreSame(_windsorContainer.Resolve<ThirdClassForInjection>(), forInjection);
        }

        [Test]
        public void Dispose_call_internal_WindsorContainer_dispose()
        {
            var mockWindsorContainer = new Mock<IWindsorContainer>();
            mockWindsorContainer.Setup(w => w.Dispose()).Verifiable();
            var wrapperContainer = new WindsorContainerAdapter(mockWindsorContainer.Object);
            wrapperContainer.Dispose();
            mockWindsorContainer.VerifyAll();
        }

        [Test]
        public void Resolve_without_parameters_should_return_correct_result()
        {
            var classForInjection =  _adapterContainer.Resolve<IClassForInjection>();
            Assert.IsInstanceOf<ClassForInjection>(classForInjection);
        }

        [Test]
        public void Resolve_with_name_should_return_correct_result()
        {
            var classForInjection = _adapterContainer.Resolve<IClassForInjection>("second");
            Assert.IsInstanceOf<SecondClassForInjection>(classForInjection);
        }

        [Test]
        public void Resolve_with_name_should_throw_exception_if_argument_null_or_emty()
        {
            Assert.Throws<ArgumentNullException>(() => _adapterContainer.Resolve<IClassForInjection>(null));
            Assert.Throws<ArgumentException>(() => _adapterContainer.Resolve<IClassForInjection>(string.Empty));
        }

        [Test]
        public void ResolveAll_should_return_correct_result()
        {
            var classes = _adapterContainer.ResolveAll<IClassForInjection>();
            Assert.NotNull(classes);
            Assert.AreEqual(classes.Count(), 3);
            Assert.IsTrue(classes.Any(clazz => clazz is SecondClassForInjection));
            Assert.IsTrue(classes.Any(clazz => clazz is ClassForInjection));
            Assert.IsTrue(classes.Any(clazz => clazz is ThirdClassForInjection));
        }
    }
}