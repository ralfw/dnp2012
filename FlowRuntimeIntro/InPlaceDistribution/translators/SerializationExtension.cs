using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace InPlaceDistribution.translators
{
    static class SerializationExtension
    {
        public static byte[] Serialize(this object obj)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.GetBuffer();
        }

        public static object Deserialize(this byte[] bytes)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(bytes);
            return bf.Deserialize(ms);
        }  
    }
}
