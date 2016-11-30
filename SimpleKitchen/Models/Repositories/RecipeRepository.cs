using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.Repositories
{
    public class RecipeRepository: Repository<Recipe>
    {
        public void AttachCookBook(CookBook cookBook)
        {
            context.Entry(cookBook).State = EntityState.Modified;
        }

        public void AttachCookBookAndAddRecipe(CookBook cookBook, Recipe recipe)
        {
            context.Entry(cookBook).State = EntityState.Modified;
            DbSet.Add(recipe);
        }
    }
}