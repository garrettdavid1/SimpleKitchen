using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RecipesCreateViewModel
    {
        [Required]
        [Display(Name = "Recipe Name*")]
        public string RecipeName { get; set; }
        [Required]
        [Display(Name = "Ingredients*")]
        public string Ingredients { get; set; }
        [Required]
        [Display(Name = "Instructions*")]
        public string Instructions { get; set; }
        [Required]
        [Display(Name = "Public Recipe?")]
        public bool IsPublic { get; set; }
        [Required]
        [Display(Name = "Select a CookBook*")]
        public string CookBookName { get; set; }
        [Display(Name = "Upload a Photo")]
        public string ImageReference { get; set; }
        public RecipesCreateViewModel()
        {

        }
    }
}