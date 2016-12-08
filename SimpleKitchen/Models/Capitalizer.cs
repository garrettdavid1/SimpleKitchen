using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models
{
    public static class Capitalizer
    {
        public static string CapitalizeFirstLetter(string originalText)
        {
            string firstLetterCapitalized = originalText.Substring(0, 1).ToUpper();
            return firstLetterCapitalized + originalText.Substring(1, originalText.Length - 1);
        }
    }
}