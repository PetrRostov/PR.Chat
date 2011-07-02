using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PR.Chat.DAL
{
    internal static class Serializator
    {
        private readonly static BinaryFormatter BinaryFormatter = new BinaryFormatter();

        public static T Deserialze<T>(byte[] serializedObj) where T : class
        {
            using (var m = new MemoryStream(serializedObj))
            {
                return (T) BinaryFormatter.Deserialize(m);
            }
        }

        public static byte[] Serialize<T>(T obj) where T : class
        {
            using (var m = new MemoryStream())
            {
                BinaryFormatter.Serialize(m, obj);
                m.Position = 0;
                return m.ToArray();
            }
        }
    }
}