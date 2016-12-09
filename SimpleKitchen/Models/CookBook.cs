using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CookBook
    {
        public int CookBookId { get; set; }
        [Display(Name = "Title")]
        public string CookBookName { get; set; }
        [Display(Name = "Description")]
        public string CookBookDescription { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
        public string OwnerId { get; set; }
        public bool IsDeletable { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public CookBook()
        {
            
        }

        public CookBook(CookBooksCreateViewModel viewModel, string ownerId)
        {
            CookBookName = viewModel.CookBookName;
            CookBookDescription = viewModel.CookBookDescription;
            IsDeletable = true;
            OwnerId = ownerId;
        }
    }
}