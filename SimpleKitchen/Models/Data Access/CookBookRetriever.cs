using SimpleKitchen.Models.Data_Access;
using SimpleKitchen.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CookBookRetriever : RepositoryInstantiator<CookBook>
    {

        public IEnumerable<string> GetUserCookBookNames(ClaimsIdentity identity)
        {
            string userId = new CurrentUserIdRetriever().GetUserId(identity);
            List<CookBook> cookBooks = EntitySorter.SortCookBooks(repository
                .GetAll()
                .Where(c => c.OwnerId == userId)
                .ToList());
            List<string> cookBookNames = new List<string>();
            foreach (var cookBook in cookBooks)
            {
                    cookBookNames.Add(cookBook.CookBookName);
            }
            repository.Dispose();
            return cookBookNames;
        }
        public IEnumerable<string> GetUserCookBookNames(ClaimsIdentity identity, string excludeCookBook)
        {
            string userId = new CurrentUserIdRetriever().GetUserId(identity);
            List<CookBook> cookBooks = EntitySorter.SortCookBooks(repository
                .GetAll()
                .Where(c => c.OwnerId == userId)
                .ToList());
            List<string> cookBookNames = new List<string>();
            foreach(var cookBook in cookBooks)
            {
                if(!(cookBook.CookBookName == excludeCookBook))
                {
                    cookBookNames.Add(cookBook.CookBookName);
                }
            }
            repository.Dispose();
            return cookBookNames;
        }

        public CookBook GetUserCookBookByName(string userId, string name)
        {
            CookBook CookBookForRecipe = repository
                .GetAll()
                .Where(c => c.OwnerId == userId && c.CookBookName == name)
                .Single();
            repository.Dispose();
            return CookBookForRecipe;
        }
    }
}