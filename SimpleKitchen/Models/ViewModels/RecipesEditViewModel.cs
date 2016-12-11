using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RecipesEditViewModel
    {
        public int RecipeId { get; set; }
        [Required]
        [Display(Name = "Recipe Name")]
        public string RecipeName { get; set; }
        [Required]
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }
        [Required]
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
        [Required]
        [Display(Name = "Public Recipe?")]
        public bool IsPublic { get; set; }
        public string OwnerId { get; set; }
        public string ImageReference { get; set; }
        public RecipesEditViewModel()
        {

        }

        public RecipesEditViewModel(Recipe recipe)
        {
            RecipeId = recipe.RecipeId;
            RecipeName = recipe.RecipeName;
            Ingredients = recipe.Ingredients;
            Instructions = recipe.Instructions;
            IsPublic = recipe.IsPublic;
            OwnerId = recipe.OwnerId;
            ImageReference = recipe.ImageReference;
        }
    }
}