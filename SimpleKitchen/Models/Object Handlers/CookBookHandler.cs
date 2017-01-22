using SimpleKitchen.Models.Repositories;
using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CookBookHandler
    {
        CookBookRepository repository;
        public CookBookHandler()
        {
            repository = new CookBookRepository();
        }
        public async Task<int> CreateAndSaveCookBook(CookBooksCreateViewModel viewModel, ClaimsIdentity identity)
        {
            repository.Add(new CookBook(viewModel, new CurrentUserIdRetriever().GetUserId(identity)));
            return await repository.SaveChangesAsync();
            
        }

        public CookBook UpdateCookBookWithViewModelData(CookBook cookBook, CookBooksEditViewModel viewModel)
        {
            cookBook.CookBookName = viewModel.CookBookName;
            cookBook.CookBookDescription = viewModel.CookBookDescription;
            return cookBook;
        }

        internal async Task<int> UpdateAndSaveCookBook(CookBooksEditViewModel viewModel)
        {
            CookBook cookBook = await repository.GetAsync(viewModel.CookBookId);
            cookBook = UpdateCookBookWithViewModelData(cookBook, viewModel);
            repository.Update(cookBook);
            return await repository.SaveChangesAsync();
        }

        internal async Task<int> RemoveRecipeFromCookBookAndSave(int cookBookId, int recipeId)
        {
            CookBook cookBook = await repository.GetAsync(cookBookId);
            List<Recipe> newRecipeList = new List<Recipe>();
            foreach(var recipe in cookBook.Recipes)
            {
                if (recipe.RecipeId != recipeId)
                    newRecipeList.Add(recipe);
            }
            cookBook.Recipes = newRecipeList;
            repository.Update(cookBook);
            return await repository.SaveChangesAsync();
        }

        internal List<CookBook> RemoveCookBookFromList(List<CookBook> cookBooks, string cookbookName)
        {
            foreach (var cookBook in cookBooks.ToList())
            {
                if (cookBook.CookBookName == "MealPlan")
                {
                    cookBooks.Remove(cookBook);
                }
            }
            return (cookBooks);
        }
    }
}