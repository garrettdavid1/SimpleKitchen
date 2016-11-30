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
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

namespace SimpleKitchen.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private RecipeRepository repository = new RecipeRepository();

        // GET: Recipes
        public ActionResult Index()
        {
            string userIdValue = new CurrentUserIdRetriever()
                .GetUserId(User.Identity as ClaimsIdentity);
            var recipes = repository.GetAll()
                .Where(x => x.OwnerId == userIdValue);
            return View(recipes);
        }

        public ActionResult PublicRecipes()
        {
            string userIdValue = new CurrentUserIdRetriever()
                .GetUserId(User.Identity as ClaimsIdentity);
            var recipes = repository.GetAll()
                .Where(x => x.IsPublic == true && x.OwnerId != userIdValue);
            return View(recipes);
        }

        public async Task<ActionResult> PublicRecipeDetails(int id)
        {
            Recipe recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Recipe recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.CookBookNames = new UserCookBookRetriever().GetUserCookBookNames(User.Identity as ClaimsIdentity);
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RecipeName,Ingredients,Instructions,IsPublic,CookBookName")] RecipesCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await new NewRecipeHandler()
                    .CreateAndSaveRecipe(viewModel, User.Identity as ClaimsIdentity);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Recipes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Recipe recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(new RecipesEditViewModel(recipe));
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecipeId,RecipeName,Ingredients,Instructions,IsPublic,OwnerId")] RecipesEditViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    Recipe recipe = new Recipe(viewModel);
                    repository.Update(recipe);
                    await repository.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.Message = "It seems someone changed this recipe while you were editing it!";
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        // GET: Recipes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Recipe recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
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
