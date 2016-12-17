using SimpleKitchen.Models.Object_Handlers;
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
    public class RecipeHandler
    {
        RecipeRepository repository;
        public RecipeHandler()
        {
            repository = new RecipeRepository();
        }

        public async Task<int> CreateAndSaveRecipe(RecipesCreateViewModel viewModel, ClaimsIdentity identity)
        {
            if(viewModel.UploadedFile != null) { 
                viewModel.ImageReference = new ImageHandler().SaveInitialImageAndGetReference(viewModel.UploadedFile);
            }
            CookBook selectedCookBook = new CookBookRetriever()
                    .GetCookBookForNewRecipe(identity, viewModel.CookBookName);
            Recipe recipe = new Recipe(viewModel,
                new CurrentUserIdRetriever().GetUserId(identity),
                selectedCookBook);
            repository.AttachCookBookAndAddRecipe(selectedCookBook, recipe);
            return await repository.SaveChangesAsync();
        }

        public async Task<int> EditAndSaveRecipe(RecipesEditViewModel viewModel)
        {
            HttpPostedFileBase file = viewModel.UploadedFile;
            bool ImageRefExists = !(viewModel.ImageReference == null);
            bool UploadedFileExists = (!(viewModel.UploadedFile == null) && viewModel.UploadedFile.ContentLength > 0);
            if(!(ImageRefExists) && UploadedFileExists)//No existing ImageReference, new UploadedFile
            {
                viewModel.ImageReference = new ImageHandler().SaveInitialImageAndGetReference(viewModel.UploadedFile);
            } else if(ImageRefExists && UploadedFileExists)//Existing ImageReference, new UploadedFile
            {
                viewModel.ImageReference = new ImageHandler().SaveEditedImageAndGetReference(viewModel.ImageReference, viewModel.UploadedFile);
            }
            Recipe recipe = new Recipe(viewModel);
            repository.Update(recipe);
            return await repository.SaveChangesAsync();
        }
    }
}