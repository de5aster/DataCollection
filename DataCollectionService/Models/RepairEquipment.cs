using System;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class RepairEquipment
    {
        public RepairEquipment()
        {
        }

        public RepairEquipment(string repairParts, int countRepairParts)
        {
            this.RepairParts = repairParts;
            this.CountRepairParts = countRepairParts;
        }

        public string RepairParts { get; set; }

        public int CountRepairParts { get; set; }
    }
}
