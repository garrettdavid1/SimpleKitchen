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
        ApplicationDbContext context;
        public RecipeHandler()
        {
            repository = new RecipeRepository();
            context = new ApplicationDbContext();
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
        internal string RemoveRecipeFromCookBook(int recipeId, int cookbookId)
        {
            CookBook cookBook = GetCookbookById(cookbookId, context);
            Recipe recipe = GetRecipeById(recipeId, context);
            recipe.CookBooksContainingRecipe.Remove(cookBook);
            cookBook.Recipes.Remove(recipe);
            context.Entry(cookBook).State = EntityState.Modified;
            context.Entry(recipe).State = EntityState.Modified;
            string message = recipe.RecipeName + " removed!";
            context.SaveChanges();
            context.Dispose();
            return message;
        }

        internal string AddRecipeToCookBookById(int recipeId, int cookbookId)
        {
            return AddRecipeToCookBook(
                GetRecipeById(recipeId, context),
                GetCookbookById(cookbookId, context),
                context);
        }

        internal string AddRecipeToCookBookByName(int recipeId, string cookBookName, string userId)
        {
            return AddRecipeToCookBook(
                GetRecipeById(recipeId, context), 
                GetCookBookByNameAndUserId(cookBookName, userId, context), 
                context);
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

        internal string AddRecipeToCookBook(Recipe recipe, CookBook cookbook, ApplicationDbContext context)
        {
            recipe.CookBooksContainingRecipe.Add(cookbook);
            cookbook.Recipes.Add(recipe);
            context.Entry(cookbook).State = EntityState.Modified;
            context.Entry(recipe).State = EntityState.Modified;
            context.SaveChanges();
            context.Dispose();
            return recipe.RecipeName + " saved!";
        }

        public Recipe GetRecipeById(int recipeId, ApplicationDbContext context)
        {
            return context
                .Recipes
                .Where(r => r.RecipeId == recipeId)
                .Include(c => c.CookBooksContainingRecipe)
                .Include(o => o.Owner)
                .Single();
        }

        public CookBook GetCookbookById(int cookbookId, ApplicationDbContext context)
        {
            return context
                .CookBooks
                .Where(c => c.CookBookId == cookbookId)
                .Include(o => o.Owner)
                .Include(r => r.Recipes)
                .SingleOrDefault();
        }

        public CookBook GetCookBookByNameAndUserId(string cookbookName, string userId, ApplicationDbContext context)
        {
            return context
                .CookBooks
                .Where(c => c.CookBookName == cookbookName && c.OwnerId == userId)
                .Include(o => o.Owner)
                .Include(r => r.Recipes)
                .SingleOrDefault();
        }
    }
}