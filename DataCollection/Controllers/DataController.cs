using System;
using System.IO;
using System.Threading.Tasks;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using DataCollectionService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        private ClientCardDatabaseService dbService = new ClientCardDatabaseService();

        [HttpPost]
        public async Task<IActionResult> Load(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var dcs = new ClientCardSerializeService();
                    return this.Ok(ClientCardSerializeService.DeserializeDataFromXml(filePath));
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
            this.dbService.AddClientCard(clientCard);
            var filePath = ClientCardSerializeService.SerializeDataToXml(clientCard, Path.GetTempPath());
            return this.PhysicalFile(filePath, fileType, fileName);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clientCardList = this.dbService.GetAllClientCards();
            return this.Ok(clientCardList);
        }
    }
}