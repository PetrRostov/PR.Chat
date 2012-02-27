using System.CodeDom;
using System.IO;

namespace PR.Chat.Domain
{
   public class CodeDomExample
   {
      public CodeDomExample()
      {
         var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp");

         provider.Parse(new StreamReader(""));
      }
   }
}