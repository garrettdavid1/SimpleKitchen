using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models
{
    public class EntitySorter
    {
        public EntitySorter()
        {

        }

        public static List<CookBook> SortCookBooks(List<CookBook> cookBooksInput)
        {
            List<CookBook> AllButSavedAndMyRecipes = new List<CookBook>();
            for (int i = 2; i < cookBooksInput.Count; i++)
            {
                AllButSavedAndMyRecipes.Add(cookBooksInput[i]);
            }
            List<CookBook> SortedCookBooks = AllButSavedAndMyRecipes.OrderBy(x => x.CookBookName).ToList();
            SortedCookBooks.Insert(0, cookBooksInput[1]);
            SortedCookBooks.Insert(0, cookBooksInput[0]);
            foreach(var cookbook in SortedCookBooks)
            {
                SortRecipes(cookbook.Recipes);
            }
            return SortedCookBooks;
        }

        internal static CookBook SortRecipesInCookBook(CookBook cookBook)
        {
            SortRecipes(cookBook.Recipes);
            return cookBook;
        }

        public static void SortRecipes(List<Recipe> recipesInput)
        {
            recipesInput.OrderBy(x => x.RecipeName).ToList();
        }

        public IEnumerable<Recipe> SortRecipesAndReturn(IEnumerable<Recipe> recipesInput)
        {
            return recipesInput.OrderBy(x => x.RecipeName).ToList();
        }
    }
}