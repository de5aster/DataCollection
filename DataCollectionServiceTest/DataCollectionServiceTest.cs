using System;
using System.IO;
using System.Xml.Serialization;
using DataCollectionService;
using DataCollectionService.Entities;
using DataCollectionService.Services;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class DataCollectionServiceTest
    {
        private readonly Data data = new Data(
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

            // new[] { new Work("sr"), new Work("ass") },
            new[] { new RepairEquipment("resistor1", 10), new RepairEquipment("resistor2", 15) });

        private readonly DataCollectionProcessor dataCollection = new DataCollectionProcessor();

        [Test]
        public void SerializeDataToXmlTest()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers");
            var formatter = this.dataCollection.SerializeDataToXml(this.data, filePath);
            var dataFromXml = this.dataCollection.DeserializeDataFromXml(formatter);
            formatter.Should().BeOfType<string>();
            dataFromXml.Should().BeEquivalentTo(this.data);
        }

        [Test]
        public void DeserializeDataFromXmlTest()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers\\Files\\data.xml");
            var dataFromXml = this.dataCollection.DeserializeDataFromXml(filePath);
            dataFromXml.Should().BeEquivalentTo(this.data);
        }
    }
}
