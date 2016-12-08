using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [Display(Name = "Recipe Name")]
        public string RecipeName { get; set; }
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
        [Display(Name = "Public Recipe?")]
        public bool IsPublic { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual List<CookBook> CookBooksContainingRecipe { get; set; } = new List<CookBook>();
        
        public Recipe()
        {

        }

        public Recipe(RecipesCreateViewModel viewModel, string ownerId, CookBook cookBook)
        {
            RecipeName = viewModel.RecipeName;
            Ingredients = viewModel.Ingredients;
            Instructions = viewModel.Instructions;
            IsPublic = viewModel.IsPublic;
            OwnerId = ownerId;
            CookBooksContainingRecipe.Add(cookBook);
        }

        public Recipe(RecipesEditViewModel viewModel)
        {
            RecipeId = viewModel.RecipeId;
            RecipeName = viewModel.RecipeName;
            Ingredients = viewModel.Ingredients;
            Instructions = viewModel.Instructions;
            IsPublic = viewModel.IsPublic;
            OwnerId = viewModel.OwnerId;
            //RowVersion = viewModel.RowVersion;
        }
    }
}