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
        private readonly IHostingEnvironment appHostingEnvironment;
        private const string FilePathXml = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\data.xml";
        private const string FilePathXls = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\struct.txt";

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
        public IActionResult Save([FromBody] Data data)
        {
            var dcs = new DataCollectionProcessor();
            var tempPath = dcs.SerializeDataToXml(data, this.appHostingEnvironment.ContentRootPath);
            var path = this.appHostingEnvironment.ContentRootPath + "\\Files\\data.xml";
            const string fileType = "application/xml";
            const string fileName = "new_client_card.xml";
            return this.PhysicalFile(tempPath, fileType, fileName);
        }

        [HttpGet]

        // [Route("Home/Api/GetFile/{format}")]
        public IActionResult SaveInUser(string format)
        {
            var reqFile = format.ToLower() == "xml" ? FilePathXml : (format.ToLower() == "xls" ? FilePathXls : string.Empty);
            var fileName = "data." + format.ToLower();

            var dataBytes = System.IO.File.ReadAllBytes(reqFile);
            var dataStream = new MemoryStream(dataBytes);

            return new FileResult(dataStream, this.Request, fileName);
        }
    }
}
