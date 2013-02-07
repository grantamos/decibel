using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Byteopia.Helpers
{
    public class JSON
    {
        public static T DeserializeObject<T>(String data)
        {
            return Deserialize<T>(data);
        }

        public static string SeralizeObject<T>(T o) where T : class
        {
            return Serialize<T>(o);
        }

        public static T Deserialize<T>(String data)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(ms);
        }

        public static string Serialize<T>(T obj) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray(), 0, stream.ToArray().Length);
            }
        }
    }
}
