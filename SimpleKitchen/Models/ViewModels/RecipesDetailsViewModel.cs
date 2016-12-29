using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RecipesDetailsViewModel : RecipeViewModel
    {
        public int RecipeId { get; set; }
        [Required]
        [Display(Name = "Recipe Name")]
        public string OwnerId { get; set; }
        public List<string> IngredientList { get; set; }
        public List<string> InstructionList { get; set; }
        public RecipesDetailsViewModel(Recipe recipe)
        {
            RecipeId = recipe.RecipeId;
            RecipeName = recipe.RecipeName;
            IsPublic = recipe.IsPublic;
            OwnerId = recipe.OwnerId;
            ImageReference = recipe.ImageReference;
            Description = recipe.Description;
            IngredientList = recipe.Ingredients.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            InstructionList = recipe.Instructions.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}