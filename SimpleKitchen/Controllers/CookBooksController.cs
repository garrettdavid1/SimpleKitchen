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

        // GET: CookBooks
        public async Task<ActionResult> Index()
        {
            return View(await repository.GetUserCookBooksWithEagerLoadedObjects(User.Identity as ClaimsIdentity));
        }

        // GET: CookBooks/Details/5
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

        // GET: CookBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CookBooks/Create
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

        // GET: CookBooks/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CookBook cookBook = await repository.GetAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // POST: CookBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CookBookId,CookBookName,CookBookDescription,OwnerId")] CookBook cookBook)
        {
            if (ModelState.IsValid)
            {
                repository.Update(cookBook);
                await repository.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cookBook);
        }

        // GET: CookBooks/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CookBook cookBook = await repository.GetAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // POST: CookBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            repository.Delete(id);
            await repository.SaveChangesAsync();
            return RedirectToAction("Index");
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
