using System;

namespace DataCollectionService.Models
{
    [Serializable]
    public class RepairEquipment
    {
        public RepairEquipment()
        {
        }

        public RepairEquipment(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
