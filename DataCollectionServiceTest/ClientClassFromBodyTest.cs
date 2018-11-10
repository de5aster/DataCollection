using System;
using System.Collections.Generic;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientClassFromBodyTest
    {
        private ClientCardFromBody clientCardFromBody = new ClientCardFromBody(
            "1",
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
            ContractId = 1,
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
        public void ConvertToClientCardFromBodyTest()
        {
            var clientFromBody = ClientCardFromBody.ConvertToClientCardFromBody(this.client);
            clientFromBody.Should().BeEquivalentTo(this.clientCardFromBody);
        }

        private void SetDefaultGuid(ClientCard client)
        {
            var defaultGuidId = Guid.Empty;
            client.Id = defaultGuidId;
            client.Works[0].WorkId = defaultGuidId;
            client.RepairEquipments[0].Id = defaultGuidId;
            client.RepairEquipments[1].Id = defaultGuidId;
        }
    }
}
