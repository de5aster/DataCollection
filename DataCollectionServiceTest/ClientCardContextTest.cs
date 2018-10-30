using System;
using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardContextTest
    {
        [Test]
        public void CanAddAndUpdateSomeData()
        {
            var clientCard = new ClientCard (
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
                new List<Works> { new Works("1") },
                new List<RepairEquipment> { new RepairEquipment("resistor1", 10), new RepairEquipment("resistor2", 15) });
            var dbservice = new ClientCardDatabaseService();

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
