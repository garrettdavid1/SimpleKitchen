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

namespace SimpleKitchen.Controllers
{
    public class CookBooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CookBooks
        public async Task<ActionResult> Index()
        {
            var cookBooks = db.CookBooks
                .Include(c => c.Recipes
                .Select(r => r.Owner));
            return View(await cookBooks.ToListAsync());
        }

        // GET: CookBooks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = await db.CookBooks.FindAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // GET: CookBooks/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: CookBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CookBookId,CookBookName,CookBookDescription,OwnerId")] CookBook cookBook)
        {
            if (ModelState.IsValid)
            {
                db.CookBooks.Add(cookBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", cookBook.OwnerId);
            return View(cookBook);
        }

        // GET: CookBooks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = await db.CookBooks.FindAsync(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", cookBook.OwnerId);
            return View(cookBook);
        }

        // POST: CookBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CookBookId,CookBookName,CookBookDescription,OwnerId")] CookBook cookBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookBook).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", cookBook.OwnerId);
            return View(cookBook);
        }

        // GET: CookBooks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = await db.CookBooks.FindAsync(id);
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
            CookBook cookBook = await db.CookBooks.FindAsync(id);
            db.CookBooks.Remove(cookBook);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
