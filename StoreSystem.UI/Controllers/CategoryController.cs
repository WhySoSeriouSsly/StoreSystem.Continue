using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Business.Abstract;
using StoreSystem.UI.Models;

namespace StoreSystem.UI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult List(string categoryName)
        {
            var model = new CategoryListViewModel
            {
                Categories = _categoryService.GetByName(categoryName)
            };
            return View(model);
        }
       
    }
}
