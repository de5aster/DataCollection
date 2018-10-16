using System;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class RepairEquipment
    {
        public string RepairParts { get; set; }
        public int CountRepairParts { get; set; }

        public RepairEquipment()
        {
        }

        public RepairEquipment(string repairParts, int countRepairParts)
        {
            RepairParts = repairParts;
            CountRepairParts = countRepairParts;
        }
    }
}
