using System.Linq;
using System.Reflection;
using System.Text;

namespace PR.Chat.Infrastructure
{
    public static class ObjectExtensions 
    {
        public static string ToUserString(this object obj)
        {
            var type = obj.GetType();
            var sb = new StringBuilder();
            sb.Append(type.FullName);
            sb.Append(" {\n");

            type.GetProperties(BindingFlags.Public | BindingFlags.GetProperty)
                .ToList()
                .ForEach(
                    prop => sb.AppendFormat("  {0} = {1}\n", prop.Name, prop.GetValue(obj, null))
                );

            sb.Append("}");

            return sb.ToString();
        }
    }
}