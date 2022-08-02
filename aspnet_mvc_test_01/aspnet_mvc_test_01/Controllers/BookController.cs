using Microsoft.AspNetCore.Mvc;

namespace aspnet_mvc_test_01.Controllers
{
    public class BookController : Controller
    {
        [Route("book")]
        public string GetTests()
        {
            return "testss";
            //return View();
        }

        [Route("book/view/{id?}")]
        public string GetId(int? id)
        {

            if (id != null)
            {
                return $"id = {id}";
            }

            return "id = null";
            //return View();
        }
    }
}
