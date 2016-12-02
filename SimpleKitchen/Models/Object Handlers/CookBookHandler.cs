using SimpleKitchen.Models.Repositories;
using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models
{
    public class CookBookHandler
    {
        CookBookRepository repository;
        public CookBookHandler()
        {
            repository = new CookBookRepository();
        }
        public async Task<int> CreateAndSaveCookBook(CookBooksCreateViewModel viewModel, ClaimsIdentity identity)
        {
            repository.Add(new CookBook(viewModel, new CurrentUserIdRetriever().GetUserId(identity)));
            return await repository.SaveChangesAsync();
            
        }
    }
}