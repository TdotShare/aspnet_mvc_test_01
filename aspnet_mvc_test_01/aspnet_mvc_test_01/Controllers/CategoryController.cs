using aspnet_mvc_test_01.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_mvc_test_01.Controllers
{
    public class CategoryController : Controller
    {

        private DbContext LibraryContext;

        [Route("category")]
        public IActionResult actionIndex()
        {
            var context = new LibraryContext();
            var category_list = context.Category.ToList();
            return View("Views/Category/Index.cshtml", category_list);
        }


        [HttpGet("category/create", Name = "category_create_page")]
        public IActionResult actionCreate()
        {
            if (HttpContext.Request.Method == "GET")
            {
                return View("Views/Category/Create.cshtml");
            }

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        [HttpPost("category/create_data")]
        public String actionCreateData()
        {
            return "";
        }

        [Route("category/delete/{id}")]
        public IActionResult actionDelete(int id)
        {
            var context = new LibraryContext();
            var category_data = context.Category.Find(id);

            if (category_data != null)
            {
                context.Category.Remove(category_data);
                context.SaveChanges();
                actionAlertData("delete success");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            else
            {
                return null;
            }
        }

        [Route("category/all")]
        public List<Models.Category> actionGetAll()
        {
            var context = new LibraryContext();
            var category_list = context.Category.ToList();
            return category_list;
        }

        public void actionAlertData(string msg, string status = "success")
        {
            // success , danger , warning
            TempData["alear"] = "true";
            TempData["msg"] = msg;
            TempData["status"] = status;
        }

    }
}
