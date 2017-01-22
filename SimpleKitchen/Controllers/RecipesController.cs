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
            string userIdValue = (User.Identity as ClaimsIdentity).GetUserId();
            var recipes = repository.GetAll()
                .Where(x => x.OwnerId == userIdValue);
            return View(recipes);
        }

        [AllowAnonymous]
        public ActionResult PublicRecipes()
        {
            string userIdValue = new CurrentUserIdRetriever()
                .GetUserId(User.Identity as ClaimsIdentity);
            CookBook cookBook = new CookBook();
            cookBook.Recipes = new EntitySorter()
                .SortRecipesAndReturn(repository.GetAll()
                    .Where(x => x.IsPublic == true && x.OwnerId != userIdValue)
                    .ToList())
                .ToList();
            return View(cookBook);
        }

        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            RecipesDetailsViewModel viewModel = new RecipesDetailsViewModel(
                await repository.GetAsync(id), 
                new CurrentUserIdRetriever()
                    .GetUserId(User.Identity as ClaimsIdentity));
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.CookBookNames = new CookBookRetriever().GetUserCookBookNames(User.Identity as ClaimsIdentity, "Saved Recipes");
            return View();
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RecipeName,Ingredients,Instructions,IsPublic,CookBookName,UploadedFile,Description")] RecipesCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await new RecipeHandler()
                    .CreateAndSaveRecipe(viewModel, new CurrentUserIdRetriever().GetUserId(User.Identity as ClaimsIdentity));
                return RedirectToAction("Index", "CookBooks");
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
        public async Task<ActionResult> Edit([Bind(Include = "RecipeId,RecipeName,Ingredients,Instructions,IsPublic,OwnerId,UploadedFile,ImageReference,Description,CookBooksContainingRecipe")] RecipesEditViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    await new RecipeHandler().EditAndSaveRecipe(viewModel);
                    return RedirectToAction("Index", "CookBooks");
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
            return RedirectToAction("Index", "CookBooks");
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
