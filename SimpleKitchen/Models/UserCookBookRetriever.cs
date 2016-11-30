using SimpleKitchen.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SimpleKitchen.Models
{
    public class UserCookBookRetriever
    {
        CookBookRepository repository = new CookBookRepository();
        public IEnumerable<string> GetUserCookBookNames(ClaimsIdentity identity)
        {
            string userId = new CurrentUserIdRetriever().GetUserId(identity);
            List<CookBook> cookBooks = repository
                .GetAll()
                .Where(c => c.OwnerId == userId)
                .ToList();
            List<string> cookBookNames = new List<string>();
            foreach(var cookBook in cookBooks)
            {
                cookBookNames.Add(cookBook.CookBookName);
            }
            repository.Dispose();
            return cookBookNames;
        }

        public CookBook GetCookBookForNewRecipe(ClaimsIdentity identity, string name)
        {
            string userId = new CurrentUserIdRetriever().GetUserId(identity);
            CookBook CookBookForRecipe = repository
                .GetAll()
                .Where(c => c.OwnerId == userId && c.CookBookName == name)
                .Single();
            repository.Dispose();
            return CookBookForRecipe;
        }
    }
}