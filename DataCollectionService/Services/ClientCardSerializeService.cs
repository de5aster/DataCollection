using System.IO;
using System.Text;
using System.Xml.Serialization;
using DataCollectionService.Entities;

namespace DataCollectionService.Services
{
    public class ClientCardSerializeService
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(ClientCard));

        public static string SerializeDataToXml(ClientCard data, Encoding encode)
        {
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, data);
                ms.Position = 0;
                return encode.GetString(ms.GetBuffer());
            }
        }

        public static ClientCard DeserializeDataFromXml(string data, Encoding encode)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                var bytes = encode.GetBytes(data);
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (ClientCard)serializer.Deserialize(ms);
            }
        }

        public static string ConvertToString(string[] items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += item;
            }

            return result;
        }
    }
}
