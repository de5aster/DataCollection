using System;
using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Exceptions;
using DataCollectionService.Helpers;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class ClientCard
    {
        public ClientCard()
        {
        }

        public Guid Id { get; set; }

        public int ContractId { get; set; }

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

        public virtual List<Work> Works { get; set; } = new List<Work>();

        public virtual List<RepairEquipment> RepairEquipments { get; set; } = new List<RepairEquipment>();

        public static ClientCard ConvertToClientCard(ClientCardFromBody client)
        {
            if (client == null)
            {
                throw new EntitiesException("Input can not be null");
            }

            return new ClientCard
            {
                Id = Guid.NewGuid(),
                ContractId = client.ContractId,
                ClientName = client.ClientName.Trim(),
                ClientAddress = client.ClientAddress.Trim(),
                PhoneNumber = client.PhoneNumber.Trim(),
                Email = client.Email.Trim(),
                Equipment = client.Equipment.Trim(),
                Breakage = client.Breakage.Trim(),
                MasterName = client.MasterName.Trim(),
                MasterPersonnelNumber = client.MasterPersonnelNumber.Trim(),
                PutDate = CheckDate(client.PutDate),
                PerformDate = CheckDate(client.PerformDate),
                Works = ConvertToWorks(client.WorkList),
                RepairEquipments = ConvertToRepairEquipment(client.RepairEquipments)
            };
        }

        private static List<Work> ConvertToWorks(IEnumerable<string> works)
        {
            var enumerable = works as string[] ?? works.ToArray();
            if (enumerable.Length == 0)
            {
                throw new EntitiesException("Works can not be null");
            }

            var workList = new List<Work>();
            foreach (var work in enumerable)
            {
                workList.Add(new Work(work));
            }

            return workList;
        }

        private static List<RepairEquipment> ConvertToRepairEquipment(IEnumerable<string[]> equips)
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

        private static DateTime CheckDate(DateTime date)
        {
            if (date.Year > 3000)
            {
                throw new EntitiesException("Invalid year");
            }

            return date;
        }
    }
}