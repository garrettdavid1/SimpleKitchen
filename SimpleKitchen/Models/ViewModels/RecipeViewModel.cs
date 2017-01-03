using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class RecipeViewModel
    {
        [Required]
        [StringLength(40)]
        [Display(Name = "Recipe Name")]
        public string RecipeName { get; set; }
        [Required]
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }
        [Required]
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
        [Required]
        [Display(Name = "Make Recipe Public?")]
        public bool IsPublic { get; set; }
        [Display(Name = "Select a CookBook")]
        public string CookBookName { get; set; }
        [StringLength(maximumLength: 200, ErrorMessage = "You may only enter up to 200 characters.")]
        public string Description { get; set; }
        public string ImageReference { get; set; }
        [Display(Name = "Upload a Photo")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}