using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SimpleKitchen.Models.Data_Access
{
    public class RecipeRetriever : RepositoryInstantiator<Recipe>
    {
        public List<Recipe> GetUserRecipes(string userId)
        {
            List<Recipe> recipes = repository
                .GetAll()
                .Where(c => c.OwnerId == userId)
                .ToList();
            repository.Dispose();
            return recipes;
        }
        public IEnumerable<string> GetUserRecipeNames(string userId)
        {
            List<Recipe> recipes = GetUserRecipes(userId);
            List<string> recipeNames = new List<string>();
            foreach (var recipe in recipes)
            {
                recipeNames.Add(recipe.RecipeName);
            }
            return recipeNames;
        }
    }
}