using System;
using System.Collections.Generic;
using DataCollectionService;
using DataCollectionService.Entities;
using DataCollectionService.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            new Work[2]
            {
               new Work("sr"), 
               new Work("ass")
            }, 
            new RepairEquipment[2]
            {
                new RepairEquipment("resistor1", 10),
                new RepairEquipment("resistor2",15)
            } 
            );

        private readonly DataCollection dataCollection = new DataCollection();
        private string path = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\data.xml";

        [Test]
        public void SerializeDataToXmlTest()
        {
           dataCollection.SerializeDataToXml(data).Should().BeTrue();
        }

        [Test]
        public void DeserializeDataFromXmlTest()
        {
            var data = dataCollection.DeserializeDataFromXml(path);
            data.WorkList[0].MasterWork.Should().Be("sr");
        }
    }
}
