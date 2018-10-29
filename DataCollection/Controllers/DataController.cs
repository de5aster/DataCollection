using System;
using System.IO;
using System.Threading.Tasks;
using DataCollectionService.Entities;
using DataCollectionService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        //public DataController(ClientCardContext context)
        //{
        //    this.db = context;
        //}

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
                    return this.Ok(dcs.DeserializeDataFromXml(filePath));
                }

                return this.BadRequest("Don't deserialize file");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ClientCard clientCard)
        {
            var dcp = new ClientCardSerializeService();
            using (var db = new ClientCardContext())
            {
                db.ClientCards.Add(clientCard);
                await db.SaveChangesAsync();
            }

            const string fileType = "application/xml";
            const string fileName = "client.xml";
            var filePath = dcp.SerializeDataToXml(clientCard, Path.GetTempPath());
            return this.PhysicalFile(filePath, fileType, fileName);
        }
    }
}