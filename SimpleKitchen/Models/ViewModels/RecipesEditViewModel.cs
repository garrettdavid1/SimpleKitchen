﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RecipesEditViewModel : RecipeViewModel
    {
        public int RecipeId { get; set; }
        [Required]
        [Display(Name = "Recipe Name")]
        public string OwnerId { get; set; }
        public List<CookBook> CookBooksContainingRecipe = new List<CookBook>();
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
            Description = recipe.Description;
            CookBooksContainingRecipe = recipe.CookBooksContainingRecipe;
        }
    }
}