using System;
using System.IO;
using System.Text;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using DataCollectionService.Services;
using FluentAssertions;
using NUnit.Framework;

namespace DataCollectionServiceTest
{
    [TestFixture]
    public class ClientCardSerializeTest
    {
        private static ClientCardFromBody clientCardFromBody = new ClientCardFromBody(
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
            new string[] { "sr" },
            new string[][] { new string[] { "resistor1", "10" }, new string[] { "resistor2", "15" } });

        private Guid defaultGuidId = new Guid("00000000-0000-0000-0000-000000000000");
        private ClientCard clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);

        [Test]
        public void CanSerializeDataToXml()
        {
            this.clientCard.Id = this.defaultGuidId;
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers\\Files\\");
            var encode = Encoding.GetEncoding(1251);
            var xmlData = ClientCardSerializeService.SerializeDataToXml(this.clientCard, encode);
            var dataFromXml = ClientCardSerializeService.DeserializeDataFromXml(xmlData, encode);
            xmlData.Should().BeOfType<string>();
            dataFromXml.Should().BeEquivalentTo(this.clientCard);
        }

        [Test]
        public void CanDeserializeDataFromXml()
        {
            this.clientCard.Id = this.defaultGuidId;
            this.clientCard.WorkList[0].WorkId = this.defaultGuidId;
            this.clientCard.RepairEquipments[0].Id = this.defaultGuidId;
            this.clientCard.RepairEquipments[1].Id = this.defaultGuidId;
            var encode = Encoding.GetEncoding(1251);
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestHelpers\\Files\\data.xml");
            var file = File.ReadAllLines(filePath, encode);
            string stringFile = ClientCardSerializeService.ConvertToString(file);

            var dataFromXml = ClientCardSerializeService.DeserializeDataFromXml(stringFile, encode);
            dataFromXml.Should().BeEquivalentTo(this.clientCard);
        }

        [Test]
        public void CanConvertToString()
        {
            var strings = new string[] { "test1", "test2" };
            var result = ClientCardSerializeService.ConvertToString(strings);
            result.Should().Be("test1test2");
        }
    }
}
