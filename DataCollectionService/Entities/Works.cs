using System;
using System.Xml.Serialization;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class Works
    {
        public Works()
        {
        }

        public Works(string work)
        {
            this.WorkId = Guid.NewGuid();
            this.Work = work;
        }

        public Guid WorkId { get; set; }

        public string Work { get; set; }

        [XmlIgnore]
        public Guid ClientCardId { get; set; }

        [XmlIgnore]
        public ClientCard ClientCard { get; set; }
    }
}
