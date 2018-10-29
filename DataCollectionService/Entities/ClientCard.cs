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

        public ClientCard(string clientName, string clientAddress, string phoneNumber, string email, string equipment, string breakage, string masterName, string masterPersonnelNumber, DateTime putDate, DateTime performData, string[] workList, string[][] repairEquipments)
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
            this.WorkList = workList;
            this.RepairEquipments = repairEquipments;
        }

        [XmlIgnore]
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Equipment { get; set; } // Возможно Enum

        public string Breakage { get; set; }

        public string MasterName { get; set; }

        public string MasterPersonnelNumber { get; set; }

        public DateTime PutDate { get; set; }

        public DateTime PerformData { get; set; }

        [XmlArrayItem("Work")]
        public string[] WorkList { get; set; }

        [XmlArrayItem("EquipmentDetails")]
        public string[][] RepairEquipments { get; set; }

        private RepairEquipment[] SetRepairEquipments(string[][] equipments)
        {
            var re = new RepairEquipment[equipments.Length];
            for (var i = 0; i < equipments.Length; i++)
            {
                var equipment = equipments[i];
                re[i] = new RepairEquipment(equipment[0], Convert.ToInt32(equipment[1]));
            }
            return re;
        }
    }
}