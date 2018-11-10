using System;
using System.Collections.Generic;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardForOutputTest
    {
        private readonly ClientCard client = new ClientCard
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
            Works = new List<Work> { new Work("sr"), new Work("srrrr") },
            RepairEquipments = new List<RepairEquipment> { new RepairEquipment("resistor1", 10), new RepairEquipment("resistor2", 15) }
        };

        private readonly ClientCardForOutput clientForOutput = new ClientCardForOutput
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
            Works = "sr;\r\nsrrrr;",
            RepairEquipments = "resistor1 - 10 шт;\r\nresistor2 - 15 шт;"
        };

        [Test]
        public void CanConvertToClientCardForOutput()
        {
            var result = ClientCardForOutput.ConvertToClientCardForOutput(this.client);
            result.Should().BeEquivalentTo(this.clientForOutput);
        }

        [Test]
        public void CanConvertToListClientCardForOutput()
        {
            var clientCardList = new List<ClientCard>
            {
                this.client,
                this.client
            };
            var listForExcel = new List<ClientCardForOutput>
            {
                this.clientForOutput,
                this.clientForOutput
            };
            var result = ClientCardForOutput.ConvertToListClientCardForOutput(clientCardList);
            result.Should().BeEquivalentTo(listForExcel);
        }
    }
}
