using Microsoft.AspNet.Identity;
using SimpleKitchen.Models;
using SimpleKitchen.Models.Data_Access;
using SimpleKitchen.Models.Repositories;
using SimpleKitchen.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SimpleKitchen.Controllers.APIControllers
{
    public class RecipeTransfersController : ApiController
    {
        public IHttpActionResult GetUserRecipes()
        {
            List<Recipe> recipes = new RecipeRetriever().GetUserRecipes(
                    new CurrentUserIdRetriever()
                    .GetUserId(User.Identity as ClaimsIdentity));
            return Ok(recipes);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRecipe(int recipeId)
        {
            Recipe recipe = await new RecipeRepository().GetAsync(recipeId);
            return Ok(recipe);
        }

        [Route("api/RecipeTransfers/filter/{rid?}/{cid?}")]
        [HttpPut]
        public IHttpActionResult RemoveRecipeFromCookBook(int rid, int cid)
        {
            string result = new RecipeHandler().RemoveRecipeFromCookBook(rid, cid);
            return Ok(result);
        }

        [Route("api/RecipeTransfers/{id}")]
        [HttpPut]
        public IHttpActionResult SaveRecipe(int id)
        {
            string message = new RecipeHandler().AddRecipeToCookBookByName(id, "Saved Recipes",
                new CurrentUserIdRetriever().GetUserId(User.Identity as ClaimsIdentity));
            return Ok(message);
        }
    }
}