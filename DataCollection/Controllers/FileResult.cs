using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    public class FileResult : IActionResult
    {
        private readonly MemoryStream fileStuff;
        private readonly string fileName;
        private HttpRequestMessage httpRequestMessage;
        private HttpResponseMessage httpResponseMessage;
        private MemoryStream dataStream;
        private HttpRequest request;

        public FileResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            this.fileStuff = data;
            this.httpRequestMessage = request;
            this.fileName = filename;
        }

        public FileResult(MemoryStream dataStream, HttpRequest request, string fileName)
        {
            this.fileStuff = dataStream;
            this.request = request;
            this.fileName = fileName;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            this.httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(this.fileStuff)
            };

            // httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());
            this.httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            this.httpResponseMessage.Content.Headers.ContentDisposition.FileName = this.fileName;
            this.httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return Task.FromResult(this.httpResponseMessage);
        }
    }
}
