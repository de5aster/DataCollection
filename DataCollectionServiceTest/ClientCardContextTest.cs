using System;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using DataCollectionService.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardContextTest
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
        private DbContextOptions<ClientCardContext> options;

        public ClientCardContextTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientCardContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "test_client_card_context");
            this.options = optionsBuilder.Options;
        }

        [Test]
        public void CanAddClientCard()
        {
            var dbService = new ClientCardDatabaseService(this.options);
            var context = new ClientCardContext(this.options);
            dbService.AddClientCardWithContext(this.clientCard, context);
            var db = dbService.GetAllClientCardsWithContext(context);
            db.FirstOrDefault().ClientName.Should().Be("Антон");
            db.FirstOrDefault().WorkList.FirstOrDefault().Work.Should().Be("sr");
        }

        [Test]
        public void CanGetAllClientCards()
        {
            var dbService = new ClientCardDatabaseService(this.options);
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            List<ClientCard> db;
            var context = new ClientCardContext(this.options);
            dbService.AddClientCardWithContext(clientCard, context);
            db = dbService.GetAllClientCardsWithContext(context);

            var work = db.FirstOrDefault().WorkList.First().Work;
            work.Should().Be("sr");
        }
    }
}
