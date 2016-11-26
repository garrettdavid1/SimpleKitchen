using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CookBook
    {
        public int CookBookId { get; set; }
        public string CookBookName { get; set; }
        public string CookBookDescription { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}