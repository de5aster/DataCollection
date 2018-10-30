using System;
using System.Xml.Serialization;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class RepairEquipment
    {
        public RepairEquipment()
        {
        }

        public RepairEquipment(string name, int count)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Count = count;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        [XmlIgnore]
        public Guid ClientCardId { get; set; }

        [XmlIgnore]
        public ClientCard ClientCard { get; set; }
    }
}
