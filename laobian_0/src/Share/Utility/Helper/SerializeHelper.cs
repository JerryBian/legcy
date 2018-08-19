using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Utility.Helper
{
    /// <summary>
    /// Useful helpers for serialize and deserialize
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>
        /// Serialize given object to XML string
        /// </summary>
        /// <typeparam name="T">Any valid object type.</typeparam>
        /// <param name="obj">The input object</param>
        /// <returns>Converted XML string</returns>
        public static string SerializeToXml<T>(T obj)
        {
            using (var sw = new Utf8StringWriter())
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj, xmlNamespaces);
                return sw.ToString();
            }
        }

        public static string SerializeToJson<T>(T obj, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static byte[] SerializeToProtoBufBytes<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T DeserializeFromProtoBufBytes<T>(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }
}