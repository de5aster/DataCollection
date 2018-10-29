using System;
using System.IO;
using System.Xml.Serialization;
using DataCollectionService.Entities;

namespace DataCollectionService.Services
{
    public class ClientCardSerializeService
    {
        public string SerializeDataToXml(ClientCard data, string path)
        {
            var filePath = path + "data.xml";
            var formatter = new XmlSerializer(typeof(ClientCard));
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                try
                {
                    formatter.Serialize(fs, data);
                    return filePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ClientCard DeserializeDataFromXml(string filepath)
        {
            var formatter = new XmlSerializer(typeof(ClientCard));
            ClientCard data;
            using (var fs = new FileStream(filepath, FileMode.Open))
            {
                try
                {
                    data = (ClientCard)formatter.Deserialize(fs);
                }
                catch (Exception ex)
                {
                   throw ex;
                }
            }

            return data;
        }
    }
}
