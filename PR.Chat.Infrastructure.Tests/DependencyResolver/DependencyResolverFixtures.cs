using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class DependencyResolverFixtures
    {
        private Mock<IDependencyResolver> _dependencyResolver;
        private readonly IList<string> _resolveObject = new List<string>{"123", "1235", string.Empty};
        private readonly IList<string> _resolveNamedObject  = new List<string> { "123", "1235", string.Empty, "1235" };
        private readonly IEnumerable<IList<string>> _resolveAllObject = new List<List<string>> {
            new List<string>{"1", "2"},
            new List<string>{"3", "4"}
        };



        [TestFixtureSetUp]
        public void Setup()
        {
            _dependencyResolver = new Mock<IDependencyResolver>();
            
            _dependencyResolver
                .Setup(dr => dr.Register(It.IsAny<object>()))
                .Verifiable("Register<> method should call");

            _dependencyResolver
                .Setup(dr => dr.Resolve<IList<string>>())
                .Returns(_resolveObject)
                .Verifiable("Resolve<> method should call");

            _dependencyResolver
                .Setup(dr => dr.Resolve<IList<string>>(It.IsAny<string>()))
                .Returns(_resolveNamedObject)
                .Verifiable("Resolve<>(name) method should call");

            
            _dependencyResolver
                .Setup(dr => dr.ResolveAll<IList<string>>())
                .Returns(_resolveAllObject)
                .Verifiable("ResolveAll<> method should call");


            DependencyResolver.InitializeWith(_dependencyResolver.Object);
        }

        [Test]
        public void Generic_Resolve_should_return_right_result()
        {
            var result = DependencyResolver.Resolve<IList<string>>();
            Assert.AreSame(result, _resolveObject);
        }

        [Test]
        public void Generic_Resolve_with_name_should_return_right_result()
        {
            var result = DependencyResolver.Resolve<IList<string>>("hdf3");
            Assert.AreSame(result, _resolveNamedObject);
        }

        [Test]
        public void Generic_Resolve_all_should_return_right_result()
        {
            var result = DependencyResolver.ResolveAll<IList<string>>();
            Assert.AreSame(result, _resolveAllObject);
        }

        [Test]
        public void Register_should_work()
        {
            DependencyResolver.Register((object)"123");
        }

        [TestFixtureTearDown]
        public void Should_call_all_inner_methods()
        {
            _dependencyResolver.VerifyAll();            
        }
    }
}
