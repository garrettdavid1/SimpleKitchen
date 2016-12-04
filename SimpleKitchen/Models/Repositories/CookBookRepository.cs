using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleKitchen.Models.Repositories
{
    public class CookBookRepository : Repository<CookBook>
    {
        public async Task<List<CookBook>> GetUserCookBooksWithEagerLoadedObjectsAsync(ClaimsIdentity identity)
        {
             string currentUserId = new CurrentUserIdRetriever().GetUserId(identity);
            return await context
                .CookBooks
                .Where(i => i.OwnerId == currentUserId)
                .Include(r => r.Recipes)
                .Include(o => o.Owner)
                .ToListAsync();
        }

        public async Task<CookBook> GetCookBookWithEagerLoadedObjectsAsync(int id)
        {
            return await context
                .CookBooks
                .Where(i => i.CookBookId == id)
                .Include(r => r.Recipes)
                .Include(o => o.Owner)
                .SingleAsync();
        }
    }
}