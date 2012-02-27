using System;
using System.Linq;

namespace PR.Chat.Infrastructure
{
    public static class Require
    {
         public static void NotNull(object obj, string name)
         {
             if (obj == null)
                 throw new ArgumentNullException(string.Format("{0} should not be null", name));
         }

        public static void NotNullOrEmpty(string obj, string name)
        {
            NotNull(obj, name);

            if (obj.Equals(string.Empty))
                throw new ArgumentException(string.Format("{0} should not be empty", name));
                
        }

        public static void InheritedFromInterface<T>(Type objectType)
        {
            var typeOfInterface = typeof (T);
            var check = objectType.GetInterfaces().Any(i => i == typeOfInterface);
            if (!check)
                throw new ArgumentException(string.Format(
                    "Type {0} should be inhereted from {1}",
                    objectType.FullName,
                    typeOfInterface.FullName
                ));
        }
    }
}