using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class ClientCard
    {
        public ClientCard()
        {
        }

        public ClientCard(string clientName, string clientAddress, string phoneNumber, string email, string equipment, string breakage, string masterName, string masterPersonnelNumber, DateTime putDate, DateTime performData, List<Works> workList, List<RepairEquipment> repairEquipments)
        {
            this.Id = Guid.NewGuid();
            this.ClientName = clientName.Trim();
            this.ClientAddress = clientAddress.Trim();
            this.PhoneNumber = phoneNumber.Trim();
            this.Email = email.Trim();
            this.Equipment = equipment.Trim();
            this.Breakage = breakage.Trim();
            this.MasterName = masterName.Trim();
            this.MasterPersonnelNumber = masterPersonnelNumber.Trim();
            this.PutDate = putDate;
            this.PerformData = performData;
            this.WorkList = this.AddWork(workList);
            this.RepairEquipments = this.AddRepairEquipments(repairEquipments);
        }

        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Equipment { get; set; }

        public string Breakage { get; set; }

        public string MasterName { get; set; }

        public string MasterPersonnelNumber { get; set; }

        public DateTime PutDate { get; set; }

        public DateTime PerformData { get; set; }

        public List<Works> WorkList { get; set; } = new List<Works>();

        public List<RepairEquipment> RepairEquipments { get; set; } = new List<RepairEquipment>();

        private List<Works> AddWork(List<Works> works)
        {
            var workList = new List<Works>();
            foreach (var work in works)
            {
                workList.Add(work);
            }

            return workList;
        }

        private List<RepairEquipment> AddRepairEquipments(List<RepairEquipment> equips)
        {
            var repairEquips = new List<RepairEquipment>();
            foreach (var equip in equips)
            {
                repairEquips.Add(equip);
            }

            return repairEquips;
        }
    }
}