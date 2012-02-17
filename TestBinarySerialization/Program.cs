using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;
using PR.Chat.Domain;
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
}
