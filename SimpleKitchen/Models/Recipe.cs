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
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
        public Recipe()
        {

        }

        public Recipe(RecipesCreateViewModel viewModel, string ownerId)
        {
            RecipeName = viewModel.RecipeName;
            Ingredients = viewModel.Ingredients;
            Instructions = viewModel.Instructions;
            IsPublic = viewModel.IsPublic;
            OwnerId = ownerId;
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