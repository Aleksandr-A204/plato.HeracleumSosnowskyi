using System.Xml.Serialization;
using System;
using HeracleumSosnowskyiService.Models;

namespace HeracleumSosnowskyiService.Data
{
    public static class XmlDeserializer
    {
        public static T GetAllValues<T>(byte[] buffer)
        {
            var serializer = new XmlSerializer(typeof(T));

            if (serializer is null)
                throw new ArgumentNullException($"Unable to serialize {nameof(XmlSerializer)} from byte file.");

            T? data;
            using (Stream reader = new MemoryStream(buffer))
            {
                data = (T)serializer.Deserialize(reader);
            }

            if (data == null)
                throw new Exception($"Unable to read {nameof(T)} from file XML.");

            return data;
        }
    }
}
