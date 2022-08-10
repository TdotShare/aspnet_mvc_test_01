using aspnet_mvc_test_01.Config;
using aspnet_mvc_test_01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_mvc_test_01.Controllers
{
    public class AuthorController : Controller
    {
        private DbContext LibraryContext;

        [Route("author", Name = "author_index_page")]
        public IActionResult actionIndex()
        {
            var context = new LibraryContext();
            var author_list = context.Author.ToList();
            return View("Views/Author/Index.cshtml", author_list);
        }

        [HttpGet("author/create", Name = "author_create_page")]
        [HttpPost("author/create_data", Name = "author_create_data")]
        public IActionResult actionCreate()
        {
            if (HttpContext.Request.Method == "GET")
            {
                return View("Views/Author/Create.cshtml");
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }


            var context = new LibraryContext();
            var model = new Author
            {
                author_fullname = data["author_fullname"],
                author_age = int.Parse(data["author_age"]),
                author_gender = data["author_gender"],
            };

            try
            {
                context.Author.Add(model);
                context.SaveChanges();
                actionAlertData("create success");
                return RedirectToRoute("author_index_page");
            }
            catch (Exception e)
            {
                actionAlertData("failed create data", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

        }


        [Route("author/delete/{id}", Name = "author_delete_data")]
        public IActionResult actionDelete(int id)
        {
            var context = new LibraryContext();
            var author_data = context.Author.Find(id);

            if (author_data != null)
            {
                context.Author.Remove(author_data);
                context.SaveChanges();
                actionAlertData("delete success");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            else
            {
                return null;
            }
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
