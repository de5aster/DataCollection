using System;
using System.Collections.Generic;
using DataCollectionService.Entities;

namespace DataCollectionService.Helpers
{
    public class ClientCardFromBody
    {
        public ClientCardFromBody()
        {
        }

        public ClientCardFromBody(string clientName, string clientAddress, string phoneNumber, string email, string equipment, string breakage, string masterName, string masterPersonnelNumber, DateTime putDate, DateTime performDate, string[] workList, string[][] repairEquipments)
        {
            this.ClientName = clientName.Trim();
            this.ClientAddress = clientAddress.Trim();
            this.PhoneNumber = phoneNumber.Trim();
            this.Email = email.Trim();
            this.Equipment = equipment.Trim();
            this.Breakage = breakage.Trim();
            this.MasterName = masterName.Trim();
            this.MasterPersonnelNumber = masterPersonnelNumber.Trim();
            this.PutDate = putDate;
            this.PerformDate = performDate;
            this.WorkList = workList;
            this.RepairEquipments = repairEquipments;
        }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Equipment { get; set; }

        public string Breakage { get; set; }

        public string MasterName { get; set; }

        public string MasterPersonnelNumber { get; set; }

        public DateTime PutDate { get; set; }

        public DateTime PerformDate { get; set; }

        public string[] WorkList { get; set; }

        public string[][] RepairEquipments { get; set; }

        public static ClientCardFromBody ConvertToClientCardFromBody(ClientCard client)
        {
            return new ClientCardFromBody
            {
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Equipment = client.Equipment,
                Breakage = client.Breakage,
                MasterName = client.MasterName,
                MasterPersonnelNumber = client.MasterPersonnelNumber,
                PutDate = client.PutDate,
                PerformDate = client.PerformDate,
                WorkList = AddWork(client.Works),
                RepairEquipments = AddRepairEquipments(client.RepairEquipments)
            };
        }

        private static string[] AddWork(IReadOnlyCollection<Work> workList)
        {
            var works = new string[workList.Count];
            var index = 0;
            foreach (var work in workList)
            {
                works.SetValue(work.Name, index);
                index++;
            }

            return works;
        }

        private static string[][] AddRepairEquipments(IReadOnlyCollection<RepairEquipment> repairEquipmentList)
        {
            var equips = new string[repairEquipmentList.Count][];
            var index = 0;
            foreach (var repairEquipments in repairEquipmentList)
            {
                var temp = new string[] { repairEquipments.Name, repairEquipments.Count.ToString() };
                equips.SetValue(temp, index);
                index++;
            }

            return equips;
        }
    }
}
