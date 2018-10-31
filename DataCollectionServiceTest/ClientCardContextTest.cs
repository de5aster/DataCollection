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

        private ClientCardDatabaseService dbservice = new ClientCardDatabaseService();
        private ClientCard clientCard = new ClientCard(clientCardFromBody);

        [Test]
        public void CanAddAndUpdateSomeData()
        {
            var clientCardFromBody = new ClientCardFromBody (
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
            var dbservice = new ClientCardDatabaseService();
            var clientCard = new ClientCard(clientCardFromBody);
            using (var context = new ClientCardContext())
            {
                dbservice.AddClientCard(clientCard);
                context.SaveChanges();
                var db = context.ClientCards.ToList();
                context.ClientCards.Last().ClientName.Should().Be("Антон");
                db.Select(p => p.WorkList
                .Where(e => e.Work == "1")
                .Should()
                .NotBeEmpty());
            }
        }
    }
}
