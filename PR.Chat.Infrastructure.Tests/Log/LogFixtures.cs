using System;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class LogFixtures
    {
        private Mock<IDependencyResolver> _dependencyResolver;
        private Mock<ILog> _log;

        [TestFixtureSetUp]
        public void Setup()
        {
            _log                = new Mock<ILog>();
            _dependencyResolver = new Mock<IDependencyResolver>();

            _dependencyResolver
                .Setup(dr => dr.Resolve<ILog>())
                .Returns(_log.Object)
                .Verifiable();

            _log.Setup(l => l.Debug(It.IsAny<string>())).Verifiable();
            _log.Setup(l => l.Info(It.IsAny<string>())).Verifiable();
            _log.Setup(l => l.Error(It.IsAny<string>())).Verifiable();
            _log.Setup(l => l.Error(It.IsAny<Exception>())).Verifiable();

            DependencyResolver.InitializeWith(_dependencyResolver.Object);
        }

        [Test]
        public void Debug_should_work()
        {
            Log.Debug("123");

            Assert.Throws<ArgumentNullException>(() => Log.Debug(null));
            Assert.Throws<ArgumentException>(() => Log.Debug(string.Empty));
        }

        [Test]
        public void Info_should_work()
        {
            Log.Info("123");
            Assert.Throws<ArgumentNullException>(() => Log.Info(null));
            Assert.Throws<ArgumentException>(() => Log.Info(string.Empty));
        }

        [Test]
        public void Error_should_work()
        {
            Log.Error("123");
            Log.Error(new Exception());

            Assert.Throws<ArgumentNullException>(() => Log.Error((string)null));
            Assert.Throws<ArgumentException>(() => Log.Error(string.Empty));

            Assert.Throws<ArgumentNullException>(() => Log.Error((Exception)null));
        }


        [TestFixtureTearDown]
        public void Should_calls_all_inner_ILog_methods()
        {
            _dependencyResolver.VerifyAll();
            _log.VerifyAll();
        }

    }
}
