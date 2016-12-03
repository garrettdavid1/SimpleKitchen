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
    public class RecipeHandler
    {
        RecipeRepository repository;
        public RecipeHandler()
        {
            repository = new RecipeRepository();
        }

        public async Task<int> CreateAndSaveRecipe(RecipesCreateViewModel viewModel, ClaimsIdentity identity)
        {
            CookBook selectedCookBook = new CookBookRetriever()
                    .GetCookBookForNewRecipe(identity, viewModel.CookBookName);
            Recipe recipe = new Recipe(viewModel,
                new CurrentUserIdRetriever().GetUserId(identity),
                selectedCookBook);
            repository.AttachCookBookAndAddRecipe(selectedCookBook, recipe);
            return await repository.SaveChangesAsync();
        }
    }
}