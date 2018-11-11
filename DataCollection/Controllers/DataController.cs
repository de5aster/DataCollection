using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataCollectionService.Entities;
using DataCollectionService.Exceptions;
using DataCollectionService.Helpers;
using DataCollectionService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCollection.Controllers
{
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        private readonly Encoding encode = Encoding.UTF8;
        private readonly ClientCardContext context;
        private readonly ClientCardDatabaseService dbService;
        private readonly DbContextOptions<ClientCardContext> options;

        public DataController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientCardContext>();
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=clientcardsnew1db;Trusted_Connection=True;");
            this.options = optionsBuilder.Options;
            this.dbService = new ClientCardDatabaseService(this.options);
            this.context = new ClientCardContext(this.options);
        }

        [HttpPost]
        public async Task<IActionResult> Load(IFormFile file)
        {
            var str = string.Empty;
            ClientCard deserializeClientCard;
            if (file.Length == 0)
            {
                return this.StatusCode(411);
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                str = this.encode.GetString(stream.GetBuffer());
            }

            try
            {
                deserializeClientCard = ClientCardSerializeService.DeserializeDataFromXml(str, this.encode);
            }
            catch (SerializeServiceException)
            {
                return this.StatusCode(409);
            }

            return this.Ok(deserializeClientCard);
        }

        [HttpPost]
        public IActionResult Save([FromBody] ClientCardFromBody clientCardFromBody)
        {
            try
            {
                var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
                var xmlData = ClientCardSerializeService.SerializeDataToXml(clientCard, this.encode);
                var bytes = this.encode.GetBytes(xmlData);
                return this.File(bytes, "application/otcet-stream", "client.xml");
            }
            catch (EntitiesException)
            {
                return this.StatusCode(209);
            }
        }

        [HttpPost]
        public IActionResult SaveDatabase([FromBody] ClientCardFromBody clientCardFromBody)
        {
            ClientCard clientCard;
            try
            {
                clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            }
            catch (EntitiesException)
            {
                return this.StatusCode(209);
            }

            try
            {
                this.dbService.AddClientCardWithContext(clientCard, this.context);
                return this.Ok("Saved");
            }
            catch (DatabaseException)
            {
                return this.StatusCode(409);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clientCards = this.dbService.GetAllClientCardsWithContext(this.context);
            var d = ClientCardForOutput.ConvertToListClientCardForOutput(clientCards);
            return this.Ok(d);
        }

        [HttpGet]
        public ActionResult Download()
        {
            var clientCards = this.dbService.GetAllClientCardsWithContext(this.context);
            byte[] bytes;
            using (var package = ExcelPackageService.CreateExcelPackage(clientCards))
            {
                bytes = package.GetAsByteArray();
            }

            return this.File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "clients.xlsx");
        }

        [HttpGet]
        public ActionResult GetContractCount()
        {
            var count = this.dbService.GetContractCountWithContext(this.context);
            return this.Ok(count);
        }
    }
}