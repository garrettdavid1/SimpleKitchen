using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class CookBooksEditViewModel
    {
        public int CookBookId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string CookBookName { get; set; }
        [Display(Name = "Description")]
        public string CookBookDescription { get; set; }
        public List<Recipe> Recipes = new List<Recipe>();
        public bool IsDeletable { get; set; }
        public string OwnerId { get; set; }
        public CookBooksEditViewModel()
        {
                
        }
        public CookBooksEditViewModel(CookBook cookBook)
        {
            CookBookId = cookBook.CookBookId;
            CookBookName = cookBook.CookBookName;
            CookBookDescription = cookBook.CookBookDescription;
            Recipes = cookBook.Recipes;
            OwnerId = cookBook.OwnerId;
            IsDeletable = cookBook.IsDeletable;
        }
    }
}