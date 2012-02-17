using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using PR.Chat.Infrastructure.Data.NH.Tests;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH
{
    [TestFixture, Category(TestCategory.DataMapping)]
    public class LambdaExpressionTypeConverterFixtures
    {
        private ISession _session;

        [SetUp]
        public void Init()
        {
            var _configuration = BaseMappingFixtures.GetSQLiteInMemoryConfiguration()
                .AddAssembly(GetType().Assembly);

            var sessionFactory = _configuration.BuildSessionFactory();
            _session = sessionFactory.OpenSession();

            new SchemaExport(_configuration).Execute(
                true,
                true,
                false,
                _session.Connection,
                Console.Out
            );  
        }

        [Test]
        public void Save_and_load_expression_should_work()
        {
            var returnStr = "<>";
            var expressionTest = new ExpressionTest(
                (Expression<Func<string>>)(() => returnStr)
            );

            using (var tran = _session.BeginTransaction())
            {
                _session.Save(expressionTest);

                tran.Commit();
            }

            _session.Clear();

            using (var tran = _session.BeginTransaction())
            {
                var objects = _session.Query<ExpressionTest>().ToArray();

                Assert.AreEqual(objects.Length, 1);

                expressionTest = objects[0];
                Assert.NotNull(expressionTest);
                Assert.NotNull(expressionTest.LambdaExpression);
                Assert.AreEqual(expressionTest.LambdaExpression.Compile().DynamicInvoke(), returnStr);

                tran.Commit();
            }

            _session.Clear();


        }
    }
}