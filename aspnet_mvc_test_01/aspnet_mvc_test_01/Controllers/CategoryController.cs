using aspnet_mvc_test_01.Config;
using aspnet_mvc_test_01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_mvc_test_01.Controllers
{
    public class CategoryController : Controller
    {

        private DbContext LibraryContext;

        [Route("category", Name = "category_index_page")]
        public IActionResult actionIndex()
        {
            var context = new LibraryContext();
            var category_list = context.Category.ToList();
            return View("Views/Category/Index.cshtml", category_list);
        }


        [HttpGet("category/create", Name = "category_create_page")]
        [HttpPost("category/create_data", Name = "category_create_data")]
        public IActionResult actionCreate()
        {
            if (HttpContext.Request.Method == "GET")
            {
                return View("Views/Category/Create.cshtml");
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }


            var context = new LibraryContext();
            var model = new Category { category_name = data["category_name"] };

            //return Json(data); 

            try
            {
                context.Category.Add(model);
                context.SaveChanges();
                actionAlertData("create success");
                return RedirectToRoute("category_index_page");
            }
            catch (Exception e)
            {
                actionAlertData("failed create data", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

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

        [HttpGet("category/update/{id?}", Name = "category_update_page")]
        [HttpPost("category/update_data/{id}", Name = "category_update_data")]
        public IActionResult actionUpdate(int? id)
        {
            var context = new LibraryContext();
            var category_data = context.Category.Find(id);

            if (HttpContext.Request.Method == "GET")
            {
                if (category_data != null)
                {
                    return View("Views/Category/Update.cshtml", category_data);
                }
                else
                {
                    return null;
                }

            }

            if (category_data == null)
            {
                actionAlertData("failed update data", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }


            category_data.category_name = data["category_name"];
            context.SaveChanges();

            actionAlertData("update success");
            return Redirect(HttpContext.Request.Headers["Referer"]);

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
