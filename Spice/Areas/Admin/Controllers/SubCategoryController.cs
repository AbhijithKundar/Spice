using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Models.ViewModel;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            var subCategory = await _db.SubCategory.Include(x => x.Category).ToListAsync();
            return View(subCategory);
        }

        //Get - CREATE
        public async Task<IActionResult> Create()
        {
            SubcategoryAndCategoryViewModel model = new SubcategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                Subcategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategory.OrderBy(x => x.Name).Select(x => x.Name).Distinct().ToListAsync()
            };

            return View(model);

        }

        //POSt - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubcategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var SubCategoryExists = _db.SubCategory.Include(x => x.Category).Where(x => x.Name == model.Subcategory.Name && x.CategoryId == model.Subcategory.CategoryId);

                if(SubCategoryExists.Count() > 0)
                {

                    StatusMessage = $"Error: Sub Category Exists Under {SubCategoryExists.First().Category.Name}. Please use another name";
                }
                else
                {
                    _db.SubCategory.Add(model.Subcategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }

            SubcategoryAndCategoryViewModel modelVM = new SubcategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                Subcategory =model.Subcategory,
                SubCategoryList = await _db.SubCategory.OrderBy(x => x.Name).Select(x => x.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };

            return View(modelVM);

        }





        [ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {

            List<SubCategory> subCategories = new List<SubCategory>();

            subCategories =await _db.SubCategory.Where(x => x.CategoryId == id).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Name"));

        }

    }
}