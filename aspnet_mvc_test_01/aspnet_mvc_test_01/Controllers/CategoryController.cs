﻿using aspnet_mvc_test_01.Config;
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
            return View("Views/Category/Index.cshtml" , category_list);
        }

        [Route("category/all")]
        public List<Models.Category> actionGetAll()
        {
            var context = new LibraryContext();
            var category_list = context.Category.ToList();
            return category_list;
        }

    }
}