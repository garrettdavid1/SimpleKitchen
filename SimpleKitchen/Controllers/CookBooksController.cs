using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleKitchen.Models;
using SimpleKitchen.Models.Repositories;
using SimpleKitchen.Models.ViewModels;
using System.Security.Claims;

namespace SimpleKitchen.Controllers
{
    [Authorize]
    public class CookBooksController : Controller
    {
        private CookBookRepository repository;

        public CookBooksController()
        {
            repository = new CookBookRepository();
        }
        
        public async Task<ActionResult> Index()
        {
            return View(await repository.GetUserCookBooksWithEagerLoadedObjectsAsync(User.Identity as ClaimsIdentity));
        }
        
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            CookBook cookBook = await repository.GetAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CookBookName,CookBookDescription")] CookBooksCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await new CookBookHandler().CreateAndSaveCookBook(viewModel, User.Identity as ClaimsIdentity);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        
        public async Task<ActionResult> Edit(int id)
        {
            CookBooksEditViewModel viewModel = new CookBooksEditViewModel(await repository.GetCookBookWithEagerLoadedObjectsAsync(id));
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CookBookId,CookBookName,CookBookDescription,OwnerId,IsDeletable")] CookBooksEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await new CookBookHandler().UpdateAndSaveCookBook(viewModel);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            CookBook cookBook = await repository.GetAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            repository.Delete(id);
            await repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveRecipeFromCookBook(int cookBookId, int recipeId)
        {
            await new CookBookHandler().RemoveRecipeFromCookBookAndSave(cookBookId, recipeId);
            return RedirectToAction("Edit", new { id = cookBookId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
