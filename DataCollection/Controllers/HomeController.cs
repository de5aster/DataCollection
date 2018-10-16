using Microsoft.AspNetCore.Mvc;

namespace DataCollection.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}