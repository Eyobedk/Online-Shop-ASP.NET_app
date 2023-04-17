using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryController()
        {
            context = new ProductCategoryRepository();
        }
        // GET
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductCategory categories = new ProductCategory();
            return View(categories);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
           
            context.Insert(category);
            context.Commit();
            return RedirectToAction("Index");
            
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory category = context.Find(Id);

            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory category, string Id)
        {
            ProductCategory categoryToEdit = context.Find(Id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                categoryToEdit.Category = category.Category;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return HttpNotFound();
                }
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);

            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}