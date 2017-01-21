using SimpleKitchen.Models.Object_Handlers;
using SimpleKitchen.Models.Repositories;
using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models
{
    public class RecipeHandler
    {
        RecipeRepository repository;
        public RecipeHandler()
        {
            repository = new RecipeRepository();
        }

        public async Task<int> CreateAndSaveRecipe(RecipesCreateViewModel viewModel, string userId
            )
        {
            HttpPostedFileBase file = viewModel.UploadedFile;
            if(UploadedFileExists(file)){
                if (IsImage(file)) {
                    viewModel.ImageReference = new ImageHandler().SaveInitialImageAndGetReference(viewModel.UploadedFile);
                }
            }
            CookBook selectedCookBook = new CookBookRetriever()
                    .GetUserCookBookByName(userId, viewModel.CookBookName);
            Recipe recipe = new Recipe(viewModel, userId, selectedCookBook);
            repository.AttachCookBookAndAddRecipe(selectedCookBook, recipe);
            return await repository.SaveChangesAsync();
        }

        //Returns success string to be displayed to user
        internal string RemoveRecipeFromCookBook(int rId, int cId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            CookBook cookBook = context
                .CookBooks
                .Where(c => c.CookBookId == cId)
                .Include(o => o.Owner)
                .Include(r => r.Recipes)
                .SingleOrDefault();
            Recipe recipe = context
                .Recipes
                .Where(r => r.RecipeId == rId)
                .Include(c => c.CookBooksContainingRecipe)
                .Include(o => o.Owner)
                .Single();
            recipe.CookBooksContainingRecipe.Remove(cookBook);
            cookBook.Recipes.Remove(recipe);
            context.Entry(cookBook).State = EntityState.Modified;
            context.Entry(recipe).State = EntityState.Modified;
            string message = recipe.RecipeName + " removed!";
            context.SaveChanges();
            context.Dispose();
            return message;
        }

        internal async Task<int> AddRecipeToCookBook(int recipeId, CookBook cookBook)
        {
            Recipe recipe = await repository.GetAsync(recipeId);
            cookBook.Recipes.Add(recipe);
            repository.AttachCookBook(cookBook);
            recipe.CookBooksContainingRecipe.Add(cookBook);
            repository.Update(recipe);
            return await repository.SaveChangesAsync();
        }

        internal string AddRecipeToCookBookByName(int recipeId, string cookBookName, string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            CookBook cookBook = context
                .CookBooks
                .Where(c => c.CookBookName == cookBookName && c.OwnerId == userId)
                .Include(o => o.Owner)
                .Include(r => r.Recipes)
                .SingleOrDefault();
            Recipe recipe = context
                .Recipes
                .Where(r => r.RecipeId == recipeId)
                .Include(c => c.CookBooksContainingRecipe)
                .Include(o => o.Owner)
                .Single();
            recipe.CookBooksContainingRecipe.Add(cookBook);
            cookBook.Recipes.Add(recipe);
            context.Entry(cookBook).State = EntityState.Modified;
            context.Entry(recipe).State = EntityState.Modified;
            string message = recipe.RecipeName + " saved!";
            context.SaveChanges();
            context.Dispose();
            return message;
        }

        public async Task<int> EditAndSaveRecipe(RecipesEditViewModel viewModel)
        {
            HttpPostedFileBase file = viewModel.UploadedFile;
            bool ImageRefExists = !(viewModel.ImageReference == null);
            if (UploadedFileExists(file))
            {
                if (IsImage(file)) {
                    if (!ImageRefExists) //No existing ImageReference
                    {
                        viewModel.ImageReference = new ImageHandler()
                            .SaveInitialImageAndGetReference(viewModel.UploadedFile);
                    }
                    else //Existing ImageReference
                    {
                        viewModel.ImageReference = new ImageHandler()
                            .SaveEditedImageAndGetReference(viewModel.ImageReference, 
                            viewModel.UploadedFile);
                    }
                }
            }
            Recipe recipe = new Recipe(viewModel);
            repository.Update(recipe);
            return await repository.SaveChangesAsync();
        }

        public bool UploadedFileExists(HttpPostedFileBase file)
        {
            return (!(file == null) && file.ContentLength > 0);
        }

        public bool IsImage(HttpPostedFileBase file)
        {
            string ContentType = file.ContentType.ToLower();
            return (ContentType == "image/jpg" ||
                     ContentType == "image/jpeg" ||
                     ContentType == "image/pjpeg" ||
                     ContentType == "image/gif" ||
                     ContentType == "image/x-png" ||
                     ContentType == "image/png");
        }
    }
}