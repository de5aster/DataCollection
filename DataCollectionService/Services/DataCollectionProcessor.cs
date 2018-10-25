using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using DataCollectionService.Models;

namespace DataCollectionService.Services
{
    public class DataCollectionProcessor
    {
        public string SerializeDataToXml(Data data, string path)
        {
            var filePath = path + "\\Files\\data.xml";
            var formatter = new XmlSerializer(typeof(Data));
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

        public Data DeserializeDataFromXml(string filepath)
        {
            var formatter = new XmlSerializer(typeof(Data));
            Data data;
            using (var fs = new FileStream(filepath, FileMode.Open))
            {
                try
                {
                    data = (Data)formatter.Deserialize(fs);
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
