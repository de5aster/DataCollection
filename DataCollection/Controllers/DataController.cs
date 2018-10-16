using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataCollectionService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        private const string FilePathXml = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\data.xml";
        private const string FilePathXls = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\struct.txt";

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

                    var dcs = new DataCollectionService.Services.DataCollectionProcessor();
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
            var dcs = new DataCollectionService.Services.DataCollectionProcessor();
            return this.Ok(dcs.SerializeDataToXml(data));
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
