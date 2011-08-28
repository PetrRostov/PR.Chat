using System;

namespace PR.Chat.Infrastructure
{
    public static class Check
    {
         public static void NotNull(object obj, string name)
         {
             if (obj == null)
                 throw new ArgumentNullException(string.Format("{0} should not be null", name));
         }

        public static void NotNullOrEmpty(string obj, string name)
        {
            if (string.IsNullOrEmpty(obj))
                throw new ArgumentException(string.Format("{0} should not be null or not be emty", name));
        }
    }
}