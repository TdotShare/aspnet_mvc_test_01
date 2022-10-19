using aspnet_mvc_test_01.Config;
using aspnet_mvc_test_01.Models;
using aspnet_mvc_test_01.Models.Join;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_mvc_test_01.Controllers
{
    public class BookController : Controller
    {
        [Route("book", Name = "book_index_page")]
        public IActionResult actionIndex()
        {

            try
            {

                var db = new LibraryContext();

                var book_list = (from book in db.Book
                                 join author in db.Author on book.book_author_id equals author.author_id
                                 join category in db.Category on book.book_category_id equals category.category_id
                                 select new BookJoin
                                 {
                                     book = book,
                                     author = author,
                                     category = category
                                 }).ToList();


                ViewData["book_list"] = book_list;

                return View("Views/Book/Index.cshtml");

            }
            catch (Exception e)
            {
                return Json(e.Message);

            }


            //return Json(ViewData["book_list"]);


        }

        [HttpGet("book/create", Name = "book_create_page")]
        [HttpPost("book/create_data", Name = "book_create_data")]

        public IActionResult actionCreate()
        {

            var db = new LibraryContext();

            if (HttpContext.Request.Method == "GET")
            {
                ViewData["author_list"] = db.Author.ToList();
                ViewData["category_list"] = db.Category.ToList();
                return View("Views/Book/Create.cshtml");
            }

            try
            {

                Dictionary<string, string> data = new Dictionary<string, string>();

                foreach (var key in Request.Form.Keys)
                {
                    data.Add(key, Request.Form[key]);
                }

                var model = new Book
                {
                    book_name = data["book_name"],
                    book_num_page = int.Parse(data["book_num_page"]),
                    book_author_id = int.Parse(data["book_author_id"]),
                    book_category_id = int.Parse(data["book_category_id"]),
                };

                //return Json(data);

                db.Book.Add(model);
                db.SaveChanges();
                actionAlertData("create success");
                return RedirectToRoute("book_index_page");
            }
            catch (Exception e)
            {
                actionAlertData("failed create data", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        [HttpGet("book/update/{id?}", Name = "book_update_page")]
        [HttpPost("book/update_data/{id}", Name = "book_update_data")]
        public IActionResult actionUpdate(int? id)
        {

            var context = new LibraryContext();

            var book_data = context.Book.Find(id);

            if (HttpContext.Request.Method == "GET")
            {

                if (book_data == null)
                {
                    return null;
                }

                ViewData["book_list"] = book_data;
                ViewData["author_list"] = context.Author.ToList();
                ViewData["category_list"] = context.Category.ToList();

                return View("Views/Book/Update.cshtml");


            }

            if (book_data == null)
            {
                actionAlertData("failed update data", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }


            book_data.book_name = data["book_name"];
            book_data.book_num_page = int.Parse(data["book_num_page"]);
            book_data.book_author_id = int.Parse(data["book_author_id"]);
            book_data.book_category_id = int.Parse(data["book_category_id"]);
            context.SaveChanges();

            actionAlertData("update success");
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }


        [Route("book/view/{id?}")]
        public IActionResult actionView(int? id)
        {
            ViewData["data_id"] = id != null ? id : "null";
            return View("Views/Book/View.cshtml");
        }

        [Route("book/delete/{id}", Name = "book_delete_data")]
        public IActionResult actionDelete(int id)
        {
            var context = new LibraryContext();
            var book_data = context.Book.Find(id);

            if (book_data != null)
            {
                context.Book.Remove(book_data);
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
