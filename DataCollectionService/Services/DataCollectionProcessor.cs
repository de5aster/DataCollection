using System;
using System.IO;
using System.Xml.Serialization;
using DataCollectionService.Entities;

namespace DataCollectionService.Services
{
    public class DataCollectionProcessor
    {
        private readonly string filePath = Path.GetTempFileName();

        public bool SerializeDataToXml(Data data)
        {
            var formatter = new XmlSerializer(typeof(Data));
            using (var fs = new FileStream(this.filePath + "data.xml", FileMode.Create))
            {
                try
                {
                    formatter.Serialize(fs, data);
                    return true;
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
