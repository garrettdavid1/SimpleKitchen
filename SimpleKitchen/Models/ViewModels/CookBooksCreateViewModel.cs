using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.ViewModels
{
    public class CookBooksCreateViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string CookBookName { get; set; }
        [Display(Name = "Description")]
        public string CookBookDescription { get; set; }
    }
}