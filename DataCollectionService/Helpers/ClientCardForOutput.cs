using System;
using System.Collections.Generic;
using DataCollectionService.Entities;

namespace DataCollectionService.Helpers
{
    public class ClientCardForOutput
    {
        public ClientCardForOutput()
        {
        }

        public Guid CardId { get; set; }

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

        public string Works { get; set; }

        public string RepairEquipments { get; set; }

        public static List<ClientCardForOutput> ConvertToListClientCardForOutput(IList<ClientCard> clientCards)
        {
            var result = new List<ClientCardForOutput>();
            foreach (var clientCard in clientCards)
            {
                result.Add(ConvertToClientCardForOutput(clientCard));
            }

            return result;
        }

        public static ClientCardForOutput ConvertToClientCardForOutput(ClientCard clientCard)
        {
            return new ClientCardForOutput()
            {
                CardId = clientCard.Id,
                ContractId = clientCard.ContractId,
                ClientName = clientCard.ClientName,
                ClientAddress = clientCard.ClientAddress,
                PhoneNumber = clientCard.PhoneNumber,
                Email = clientCard.Email,
                Equipment = clientCard.Equipment,
                Breakage = clientCard.Breakage,
                MasterName = clientCard.MasterName,
                MasterPersonnelNumber = clientCard.MasterPersonnelNumber,
                PutDate = clientCard.PutDate,
                PerformDate = clientCard.PerformDate,
                Works = ConvertWorksToString(clientCard.Works),
                RepairEquipments = ConvertRepairEquipmentsToString(clientCard.RepairEquipments)
            };
        }

        private static string ConvertRepairEquipmentsToString(IEnumerable<RepairEquipment> repairEquipments)
        {
            var result = "";
            foreach (var equip in repairEquipments)
            {
                result += equip.Name + " - " + equip.Count + " шт;" + Environment.NewLine;
            }

            return result.Trim();
        }

        private static string ConvertWorksToString(IEnumerable<Work> works)
        {
            var worksString = "";
            foreach (var work in works)
            {
                worksString += work.Name + ";" + Environment.NewLine;
            }

            return worksString.Trim();
        }
    }
}
