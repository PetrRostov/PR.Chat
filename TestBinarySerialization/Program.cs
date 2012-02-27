using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using PR.Chat.Infrastructure.LinqSpecs;

namespace TestBinarySerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var binaryFormatter = new BinaryFormatter();
            var leftBool = true;
            var spec1 = new AdHocSpecification<Stream>(s => s.CanRead || string.IsNullOrEmpty(string.Empty));
            var spec2 = new AdHocSpecification<Stream>(s => s.CanSeek && leftBool);
            var spec3 = spec1 & spec2;

            
            var memoryStream = new MemoryStream();


            binaryFormatter.Serialize(memoryStream, spec3);

            memoryStream.Position = 0;
            //var seriaizedExpression = memoryStream.ToArray();


            spec3 = (Specification<Stream>) binaryFormatter.Deserialize(memoryStream);

            var spec3Expression = spec3.IsSatisfiedBy();
            
            Console.WriteLine(spec3Expression.Compile()(memoryStream));

            Console.ReadLine();

        }
    }

    public class Nick
    {
        public Guid Id { get; private set; }

        private readonly RightRuleRepository _rightRuleReository = new RightRuleRepository();

        [RequirePermission("ReceiveMessage", Parameters = new[] {"{this}", "{currentUser}", "message"})]
        private void ReceiveMessage(string message)
        {
            var hasRight = true;
            var rules = _rightRuleReository.GetRules(Id, "ReceiveMessage");
            foreach (var rightRule in rules)
            {
                hasRight = hasRight && (bool) rightRule.CheckExpression.Compile().DynamicInvoke(this, message);
                if (!hasRight)
                    throw new Exception("Епта наха");
            }
        }
    }

    internal class RequirePermissionAttribute : Attribute
    {
        private readonly string _permissionName;

        public RequirePermissionAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        public string[] Parameters
        {
            get;
            set;
        }
    }

    public class RightRuleRepository
    {
        public IEnumerable<RightRule> GetRules(Guid ownerId, string permissionName)
        {
            throw new NotImplementedException();
        }
    }

    public class RightRule
    {
        public LambdaExpression CheckExpression 
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
