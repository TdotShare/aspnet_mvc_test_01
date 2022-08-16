using Microsoft.AspNetCore.Mvc;
using aspnet_mvc_test_01.Config;
using aspnet_mvc_test_01.Models;

namespace aspnet_mvc_test_01.Controllers
{
    public class FileController : Controller
    {
        [Route("book/file/{id}", Name = "filebook_index_page")]
        public IActionResult actionIndex(int? id)
        {
            var db = new LibraryContext();

            var book_data = db.Book.Find(id);

            if (book_data == null)
            {
                return null;
            }


            ViewData["book_data"] = book_data;
            ViewData["attachment_list"] = db.Attachment.Where(a => a.attachment_book_id == book_data.book_id).ToList();

            return View("Views/Book/File.cshtml");
        }

        [HttpPost("file/upload_data/{id}", Name = "file_upload_data")]
        public async Task<IActionResult> actionUpload(int id)
        {


            var db = new LibraryContext();
            var book_data = db.Book.Find(id);

            if (book_data == null)
            {
                return null;
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }

            try
            {

                var file_data = Request.Form.Files["file_data"];

                var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"wwwroot\uploads"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, file_data.FileName), FileMode.Create))
                {
                    await file_data.CopyToAsync(fileStream);
                }

                var model = new Attachment
                {
                    attachment_book_id = book_data.book_id,
                    attachment_filename = file_data.FileName,
                };

                db.Attachment.Add(model);
                db.SaveChanges();

                actionAlertData("add file success");
                return Redirect(HttpContext.Request.Headers["Referer"]);

            }
            catch (Exception ex)
            {
                actionAlertData("add file fail", "danger");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        [Route("file/delete/{id}", Name = "file_delete_data")]
        public IActionResult actionDelete(int id)
        {
            var context = new LibraryContext();
            var attachment_data = context.Attachment.Find(id);

            if (attachment_data != null)
            {

                var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"wwwroot\uploads\" + attachment_data.attachment_filename));

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                context.Attachment.Remove(attachment_data);
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
