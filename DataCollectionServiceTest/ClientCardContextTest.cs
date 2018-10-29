using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollectionService.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardContextTest
    {
        private DbContextOptions<ClientCardContext> options;

        public ClientCardContextTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientCardContext>();
            this.options = optionsBuilder.Options;
        }

        [Test]
        public void CanAddAndUpdateSomeData()
        {
            var clientCard = new ClientCard
            (
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
                new[] { "1" },
                new string[][] { new string[] { "resistor1", "10" }, new string[] { "resistor2", "15" } });
            using (var context = new ClientCardContext(this.options))
            {
                context.ClientCards.Add(clientCard);
                context.SaveChanges();
            }

            using (var context = new ClientCardContext(this.options))
            {
                context.ClientCards.FirstOrDefault().ClientName.Should().Be("Антон");
            }
        }
    }
}
