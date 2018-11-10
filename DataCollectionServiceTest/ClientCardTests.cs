using System;
using System.Collections.Generic;
using DataCollectionService.Entities;
using DataCollectionService.Exceptions;
using DataCollectionService.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardTests
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
            new[] { "sr" },
            new[] { new[] { "resistor1", "10" }, new[] { "resistor2", "15" } });

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
        public void CanConvertToClientCard()
        {
            var clientCard = ClientCard.ConvertToClientCard(this.clientCardFromBody);
            SetDefaultGuid(this.client, clientCard);
            clientCard.Should().BeEquivalentTo(this.client);
        }

        [Test]
        public void CanNotConvertNullToCardClient()
        {
            ClientCardFromBody clientFromBody = null;

            Action act = () => ClientCard.ConvertToClientCard(clientFromBody);
            act.Should().Throw<EntitiesException>()
                .WithMessage("Input can not be null");
        }

        [Test]
        public void CanNotConvertToCardClientWithoutWorks()
        {
            var clientFromBody = new ClientCardFromBody(
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
                new string[] { },
                new[] { new[] { "resistor1", "10" }, new[] { "resistor2", "15" } });

            Action act = () => ClientCard.ConvertToClientCard(clientFromBody);
            act.Should().Throw<EntitiesException>()
                .WithMessage("Works can not be null");
        }

        private static void SetDefaultGuid(ClientCard client, ClientCard clientResult)
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
