using System.IO;
using System.Text;
using System.Xml.Serialization;
using DataCollectionService.Entities;
using DataCollectionService.Exceptions;
using System;

namespace DataCollectionService.Services
{
    public class ClientCardSerializeService
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientCard));

        public static string SerializeDataToXml(ClientCard data, Encoding encode)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                ms.Position = 0;
                return encode.GetString(ms.GetBuffer()).Trim();
            }
        }

        public static ClientCard DeserializeDataFromXml(string data, Encoding encode)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            ClientCard deserializeClientCard;
            using (var ms = new MemoryStream())
            {
                var bytes = encode.GetBytes(data);
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                try
                {
                    deserializeClientCard = (ClientCard)Serializer.Deserialize(ms);
                }
                catch (Exception)
                {
                    throw new SerializeServiceException("Invalid xml file");
                }

                return deserializeClientCard;
            }
        }

        public static string ConvertToString(string[] items)
        {
            var result = "";
            foreach (var item in items)
            {
                result += item;
            }

            return result;
        }
    }
}
