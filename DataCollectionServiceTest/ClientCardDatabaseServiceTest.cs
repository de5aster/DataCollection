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
        private static ClientCardFromBody clientCardFromBody = new ClientCardFromBody(
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

        private ClientCard clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);

        [Test]
        public void CanAddClientCard()
        {
            var options = this.SetOptions("Add_writes_to_database");
            var dbService = new ClientCardDatabaseService(options);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(this.clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault().ClientName.Should().Be("Антон");
            db.FirstOrDefault().Works.FirstOrDefault().Name.Should().Be("sr");
        }

        [Test]
        public void CanGetAllClientCards()
        {
            var options = this.SetOptions("Get_all_client_card");
            var dbService = new ClientCardDatabaseService(options);
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault().Works.FirstOrDefault().Name.Should().Be("sr");
        }

        [Test]
        public void CanAddRepairEquipments()
        {
            var options = this.SetOptions("Add_repair_equipments");
            var dbService = new ClientCardDatabaseService(options);
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            var context = new ClientCardContext(options);
            dbService.AddClientCardWithContext(clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault().RepairEquipments.FirstOrDefault().Name.Should().Be("resistor1");
        }

        private DbContextOptions<ClientCardContext> SetOptions(string name)
        {
            return new DbContextOptionsBuilder<ClientCardContext>()
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}
