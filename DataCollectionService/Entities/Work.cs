using System;
using System.Xml.Serialization;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class Work
    {
        public Work()
        {
        }

        public Work(string work)
        {
            this.WorkId = Guid.NewGuid();
            this.Name = work;
        }

        public Guid WorkId { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public Guid ClientCardId { get; set; }

        [XmlIgnore]
        public virtual ClientCard ClientCard { get; set; }
    }
}
