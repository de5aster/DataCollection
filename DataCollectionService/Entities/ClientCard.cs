using System;
using System.Collections.Generic;
using DataCollectionService.Helpers;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class ClientCard
    {
        public ClientCard()
        {
        }

        // public ClientCard(ClientCardFromBody clientCardFromBody)
        // {
        //     this.Id = Guid.NewGuid();
        //    this.ClientName = clientCardFromBody.ClientName.Trim();
        //    this.ClientAddress = clientCardFromBody.ClientAddress.Trim();
        //    this.PhoneNumber = clientCardFromBody.PhoneNumber.Trim();
        //    this.Email = clientCardFromBody.Email.Trim();
        //    this.Equipment = clientCardFromBody.Equipment.Trim();
        //    this.Breakage = clientCardFromBody.Breakage.Trim();
        //    this.MasterName = clientCardFromBody.MasterName.Trim();
        //    this.MasterPersonnelNumber = clientCardFromBody.MasterPersonnelNumber.Trim();
        //    this.PutDate = clientCardFromBody.PutDate;
        //    this.PerformData = clientCardFromBody.PerformData;
        //    this.WorkList = this.AddWork(clientCardFromBody.WorkList);
        //    this.RepairEquipments = this.AddRepairEquipments(clientCardFromBody.RepairEquipments);
        // }
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
                WorkList = AddWork(client.WorkList),
                RepairEquipments = AddRepairEquipments(client.RepairEquipments)
            };
        }

        private static List<Works> AddWork(string[] works)
        {
            var workList = new List<Works>();
            foreach (var work in works)
            {
                workList.Add(new Works(work));
            }

            return workList;
        }

        private static List<RepairEquipment> AddRepairEquipments(string[][] equips)
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