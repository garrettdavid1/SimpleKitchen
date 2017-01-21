using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RemoveRecipeFromCookBookViewModel
    {
        public int RecipeId { get; set; }
        public int CookBookId { get; set; }

    }
}