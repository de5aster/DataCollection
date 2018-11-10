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
            var filePath = Path.GetTempFileName();
            var str = string.Empty;
            if (file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    str = this.encode.GetString(stream.GetBuffer());
                }

               return this.Ok(ClientCardSerializeService.DeserializeDataFromXml(str, this.encode));
            }

            return this.BadRequest("Don't deserialize file");
        }

        [HttpPost]
        public IActionResult Save([FromBody] ClientCardFromBody clientCardFromBody)
        {
            const string fileType = "application/otcet-stream";
            const string fileName = "client.xml";
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            var xmlData = ClientCardSerializeService.SerializeDataToXml(clientCard, this.encode);
            //var ms = new MemoryStream();
            var bytes = this.encode.GetBytes(xmlData);
            //ms.Write(bytes, 0, bytes.Length);
            //ms.Position = 0;
            return this.File(bytes, fileType, fileName);
        }

        [HttpPost]
        public IActionResult SaveDatabase([FromBody] ClientCardFromBody clientCardFromBody)
        {
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
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
            var d = new List<ClientCardFromBody>();
            foreach (var client in clientCards)
            {
                d.Add(ClientCardFromBody.ConvertToClientCardFromBody(client));
            }

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