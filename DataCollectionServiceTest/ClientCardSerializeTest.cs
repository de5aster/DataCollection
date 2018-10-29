using System;
using System.IO;
using DataCollectionService.Entities;
using DataCollectionService.Services;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardSerializeTest
    {
        private readonly ClientCard clientCard = new ClientCard(
            "Антон",
            "Ekb",
            "89122221408",
            "mail@mail.ru",
            "TV",
            "Кинескоп",
            "Сергей",
            "123434",
            new DateTime(2018, 08, 08),
            new DateTime(2018, 08, 15),
            new[] { "sr", "ass" },
            new string[][] { new string[] { "resistor1", "10" }, new string[] { "resistor2", "15" } });

        private readonly ClientCardSerializeService dataCollection = new ClientCardSerializeService();

        [Test]
        public void SerializeDataToXmlTest()
        {

            this.clientCard.Id = new Guid("00000000-0000-0000-0000-000000000000");
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers");
            var formatter = this.dataCollection.SerializeDataToXml(this.clientCard, filePath);
            var dataFromXml = this.dataCollection.DeserializeDataFromXml(formatter);
            formatter.Should().BeOfType<string>();
            dataFromXml.Should().BeEquivalentTo(this.clientCard);
        }

        [Test]
        public void DeserializeDataFromXmlTest()
        {
            this.clientCard.Id = new Guid("00000000-0000-0000-0000-000000000000");
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers\\Files\\data.xml");
            var dataFromXml = this.dataCollection.DeserializeDataFromXml(filePath);
            dataFromXml.Should().BeEquivalentTo(this.clientCard);
        }
    }
}
