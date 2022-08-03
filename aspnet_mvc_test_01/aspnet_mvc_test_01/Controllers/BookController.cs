using Microsoft.AspNetCore.Mvc;

namespace aspnet_mvc_test_01.Controllers
{
    public class BookController : Controller
    {
        [Route("book")]
        public IActionResult actionIndex()
        {
            return View("Views/Book/Index.cshtml");
        }

        [Route("book/view/{id?}")]
        public IActionResult actionView(int? id)
        {
            ViewData["data_id"] = id != null ? id : "null";
            return View("Views/Book/View.cshtml");
        }
    }
}
