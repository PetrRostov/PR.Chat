using System.CodeDom.Compiler;
using System.IO;

namespace PR.Chat.Domain
{
   public class CodeDomExample
   {
      public CodeDomExample()
      {
         var provider = CodeDomProvider.CreateProvider("CSharp");

         provider.Parse(new StreamReader(""));
      }
   }
}