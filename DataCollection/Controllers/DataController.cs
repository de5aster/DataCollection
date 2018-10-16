using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using DataCollectionService.Entities;

namespace DataCollection.Controllers
{
    
    [Route("home/api/[action]")]
    public class DataController : Controller
    {
        string filePath_xml = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\data.xml";
        string filePath_xls = "C:\\Users\\kalistratov\\Desktop\\Projects\\Web_develop_modul_project\\struct.txt";

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

                    var dcs = new DataCollectionService.Services.DataCollection();
                    return Ok(dcs.DeserializeDataFromXml(filePath));
                }

                return BadRequest("Don't deserialize file");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] Data data)
        {
            var dcs = new DataCollectionService.Services.DataCollection();
            return Ok(dcs.SerializeDataToXml(data));
        }

        [HttpGet]
        //[Route("Home/Api/GetFile/{format}")]
        public IActionResult SaveInUser(string format) {
            string reqFile = format.ToLower() == "xml" ? filePath_xml : (format.ToLower() == "xls" ? filePath_xls : "");
            string fileName = "data." + format.ToLower();

            var dataBytes = System.IO.File.ReadAllBytes(reqFile);
            var dataStream = new MemoryStream(dataBytes);

            return new FileResult(dataStream, Request, fileName);
        }
    }


    public class FileResult : IActionResult
    {
        MemoryStream fileStuff;
        string FileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        private MemoryStream dataStream;
        private HttpRequest request;

        public FileResult(MemoryStream data, HttpRequestMessage request, string filename) {
            fileStuff = data;
            httpRequestMessage = request;
            FileName = filename;
        }

        public FileResult(MemoryStream dataStream, HttpRequest request, string fileName)
        {
            this.fileStuff = dataStream;
            this.request = request;
            this.FileName = fileName;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(fileStuff);
            //httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = FileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return Task.FromResult(httpResponseMessage);
        }
    }
}

