using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SimpleKitchen.Models.Data_Access
{
    public class RecipeRetriever : RepositoryInstantiator<Recipe>
    {
        public IEnumerable<string> GetUserRecipeNames(ClaimsIdentity identity)
        {
            string userId = new CurrentUserIdRetriever().GetUserId(identity);
            List<Recipe> recipes = repository
                .GetAll()
                .Where(c => c.OwnerId == userId)
                .ToList();
            List<string> recipeNames = new List<string>();
            foreach (var recipe in recipes)
            {
                recipeNames.Add(recipe.RecipeName);
            }
            repository.Dispose();
            return recipeNames;
        }
    }
}