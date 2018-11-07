using System;
using System.Collections.Generic;
using DataCollectionService.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class ClientCard
    {
        public ClientCard()
        {
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

        public virtual List<Work> Works { get; set; } = new List<Work>();

        public virtual List<RepairEquipment> RepairEquipments { get; set; } = new List<RepairEquipment>();

        public static ClientCard ConvertToClientCard(ClientCardFromBody client)
        {
            return new ClientCard
            {
                Id = Guid.NewGuid(),
                ClientName = client.ClientName.Trim(),
                ClientAddress = client.ClientAddress.Trim(),
                PhoneNumber = client.PhoneNumber.Trim(),
                Email = client.Email.Trim(),
                Equipment = client.Equipment.Trim(),
                Breakage = client.Breakage.Trim(),
                MasterName = client.MasterName.Trim(),
                MasterPersonnelNumber = client.MasterPersonnelNumber.Trim(),
                PutDate = client.PutDate,
                PerformData = client.PerformData,
                Works = ConvertToWorks(client.WorkList),
                RepairEquipments = ConvertToRepairEquipment(client.RepairEquipments)
            };
        }

        private static List<Work> ConvertToWorks(string[] works)
        {
            var workList = new List<Work>();
            foreach (var work in works)
            {
                workList.Add(new Work(work));
            }

            return workList;
        }

        private static List<RepairEquipment> ConvertToRepairEquipment(string[][] equips)
        {
            var repairEquips = new List<RepairEquipment>();
            foreach (var equip in equips)
            {
                var name = equip[0];
                var count = Convert.ToInt32(equip[1]);
                repairEquips.Add(new RepairEquipment(name, count));
            }

            return repairEquips;
        }
    }
}