using aspnet_mvc_test_01.Config;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_mvc_test_01.Controllers
{
    public class BookController : Controller
    {
        [Route("book", Name = "book_index_page")]
        public IActionResult actionIndex()
        {
            var db = new LibraryContext();

            //var book_list = db.Book.Join(db.Author,
            //    book => book.book_author_id,
            //    author => author.author_id,
            //    (book, author) => new
            //    {
            //        book = book,
            //    }
            //).Join(db.Category,
            //book => book.book.book_category_id,
            //category => category.category_id,
            //(book ,category) => new
            //{
            //    category = category
            //}).ToList();

            var book_list = (from book in db.Book
                             join author in db.Author on book.book_author_id equals author.author_id
                             join category in db.Category on book.book_category_id equals category.category_id
                             select new aspnet_mvc_test_01.Models.Join.BookJoin
                             {
                                 book = book,
                                 author = author,
                                 category = category
                             }).ToList();



            ViewData["book_list"] = book_list;
            ViewData["book"] = "testss";

            //return Json(ViewData["book_list"]);

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
