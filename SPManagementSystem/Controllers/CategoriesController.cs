﻿using Microsoft.AspNetCore.Mvc;
using SPManagementSystem.Models;

namespace SPManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            var categories = CategoriesRepository.GetCategories();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var category = CategoriesRepository.GetCategoryById(id.HasValue?id.Value:0);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                CategoriesRepository.UpdateCategory(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(category);
        }


        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if(ModelState.IsValid)
            {
                CategoriesRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
            var result = CategoriesRepository.DeleteCategory(categoryId);
            if (result == -1)
            {
                ModelState.AddModelError(string.Empty, "Cannot delete the category because there are products that contain it.");
                var categories = CategoriesRepository.GetCategories(); // Assuming you have a method to retrieve all categories
                return View("Index", categories);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}