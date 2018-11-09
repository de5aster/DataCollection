using System;
using System.Collections.Generic;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardForExcelTest
    {
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
            Works = new List<Work> { new Work("sr"), new Work("srrrr") },
            RepairEquipments = new List<RepairEquipment> { new RepairEquipment("resistor1", 10), new RepairEquipment("resistor2", 15) }
        };

        private ClientCardForExcel clientForExcel = new ClientCardForExcel
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
            Works = "sr\r\nsrrrr",
            RepairEquipments = "resistor1 - 10\r\nresistor2 - 15"
        };

        [Test]
        public void CanConvertToClientCardForExcel()
        {
            var result = ClientCardForExcel.ConvertToClientCardForExcel(this.client);
            result.Should().BeEquivalentTo(this.clientForExcel);
        }

        [Test]
        public void CanConvertToListClientCardForExcel()
        {
            var clientCardList = new List<ClientCard>
            {
                this.client,
                this.client
            };
            var listForExcel = new List<ClientCardForExcel>
            {
                this.clientForExcel,
                this.clientForExcel
            };
            var result = ClientCardForExcel.ConvertToListClientCardForExcel(clientCardList);
            result.Should().BeEquivalentTo(listForExcel);
        }
    }
}
