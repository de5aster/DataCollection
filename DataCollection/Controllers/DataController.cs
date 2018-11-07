using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataCollectionService.Entities;
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
        private DbContextOptions<ClientCardContext> options;
        private ClientCardContext context;
        private ClientCardDatabaseService dbService;

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
            string str = string.Empty;
            try
            {
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
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] ClientCardFromBody clientCardFromBody)
        {
            const string fileType = "application/otcet-stream";
            const string fileName = "client.xml";
            var clientCard = ClientCard.ConvertToClientCard(clientCardFromBody);
            this.dbService.AddClientCardWithContext(clientCard, this.context);
            var xmlData = ClientCardSerializeService.SerializeDataToXml(clientCard, this.encode);
            var ms = new MemoryStream();
            var bytes = this.encode.GetBytes(xmlData);
            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;
            return this.File(ms, fileType);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
           var clientCardList = this.dbService.GetAllClientCardsWithContext(this.context);
            var d = new List<ClientCardFromBody>();
            foreach (var client in clientCardList)
            {
                d.Add(ClientCardFromBody.ConvertToClientCardFromBody(client));
            }

            return this.Ok(d);
        }
    }
}