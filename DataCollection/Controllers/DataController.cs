using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using DataCollectionService.Models;
using DataCollectionService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        private readonly IHostingEnvironment appHostingEnvironment;

        public DataController(IHostingEnvironment appHostingEnvironment)
        {
            this.appHostingEnvironment = appHostingEnvironment;
        }

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

                    var dcs = new DataCollectionProcessor();
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
        public async Task<IActionResult> Save([FromBody] Data data)
        {
            const string fileType = "application/xml";
            const string fileName = "client.xml";
            var dcs = new DataCollectionProcessor();
            var tempPath = dcs.SerializeDataToXml(data, this.appHostingEnvironment.ContentRootPath);
            var memory = new MemoryStream();

            using (var stream = new FileStream(tempPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return this.File(memory, fileType, fileName);
        }
    }
}