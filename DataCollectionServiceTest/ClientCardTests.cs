using System;
using System.Collections.Generic;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardTests
    {
        private ClientCardFromBody clientCardFromBody = new ClientCardFromBody(
            "Антон",
            "ЕКБ",
            "8",
            "mail@mail.ru",
            "TV",
            "Display",
            "Sergey",
            "123",
            new DateTime(2018, 01, 01),
            new DateTime(2018, 03, 01),
            new string[] { "sr" },
            new string[][] { new string[] { "resistor1", "10" }, new string[] { "resistor2", "15" } });

        private ClientCard client = new ClientCard
        {
            ClientName = "Антон",
            ClientAddress = "ЕКБ",
            PhoneNumber = "8",
            Email = "mail@mail.ru",
            Equipment = "TV",
            Breakage = "Display",
            MasterName = "Sergey",
            MasterPersonnelNumber = "123",
            PutDate = new DateTime(2018, 01, 01),
            PerformDate = new DateTime(2018, 03, 01),
            Works = new List<Work> { new Work("sr") },
            RepairEquipments = new List<RepairEquipment> { new RepairEquipment("resistor1", 10), new RepairEquipment("resistor2", 15) }
        };

        [Test]
        public void ConvertToClientCardTest()
        {
            var clientCard = ClientCard.ConvertToClientCard(this.clientCardFromBody);
            this.SetDefaultGuid(this.client, clientCard);
            clientCard.Should().BeEquivalentTo(this.client);
        }

        private void SetDefaultGuid(ClientCard client, ClientCard clientResult)
        {
            var defaultGuidId = Guid.Empty;
            client.Id = defaultGuidId;
            client.Works[0].WorkId = defaultGuidId;
            client.RepairEquipments[0].Id = defaultGuidId;
            client.RepairEquipments[1].Id = defaultGuidId;

            clientResult.Id = defaultGuidId;
            clientResult.Works[0].WorkId = defaultGuidId;
            clientResult.RepairEquipments[0].Id = defaultGuidId;
            clientResult.RepairEquipments[1].Id = defaultGuidId;
        }
    }
}
