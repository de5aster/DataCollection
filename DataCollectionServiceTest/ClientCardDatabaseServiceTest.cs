using System;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using DataCollectionService.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardDatabaseServiceTest
    {
        private static readonly ClientCardFromBody ClientCardFromBody = new ClientCardFromBody(
                "Антон",
                "ЕКБ",
                "8",
                "mail@mail.ru",
                "TV",
                "Display",
                "Sergey",
                "123",
                new DateTime(2018, 01, 01),
                new DateTime(2018, 01, 01),
                new string[] { "sr" },
                new string[][] { new string[] { "resistor1", "10" }, new string[] { "resistor2", "15" } });

        private readonly ClientCard clientCard = ClientCard.ConvertToClientCard(ClientCardFromBody);

        [Test]
        public void CanAddClientCard()
        {
            var options = SetOptions("Add_writes_to_database");
            var dbService = new ClientCardDatabaseService(options);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(this.clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault()?.ClientName.Should().Be("Антон");
            db.FirstOrDefault()?.Works.FirstOrDefault()?.Name.Should().Be("sr");
        }

        [Test]
        public void CanGetAllClientCards()
        {
            var options = SetOptions("Get_all_client_card");
            var dbService = new ClientCardDatabaseService(options);
            var clientCard = ClientCard.ConvertToClientCard(ClientCardFromBody);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault()?.Works.FirstOrDefault()?.Name.Should().Be("sr");
        }

        [Test]
        public void CanAddRepairEquipments()
        {
            var options = SetOptions("Add_repair_equipments");
            var dbService = new ClientCardDatabaseService(options);
            var clientCard = ClientCard.ConvertToClientCard(ClientCardFromBody);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault()?.RepairEquipments.FirstOrDefault()?.Name.Should().Be("resistor1");
        }

        private static DbContextOptions<ClientCardContext> SetOptions(string name)
        {
            return new DbContextOptionsBuilder<ClientCardContext>()
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(name)
                .Options;
        }
    }
}
