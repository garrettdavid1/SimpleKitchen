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
            HttpPostedFileBase file = viewModel.UploadedFile;
            if(UploadedFileExists(file)){
                if (IsImage(file)) {
                    viewModel.ImageReference = new ImageHandler().SaveInitialImageAndGetReference(viewModel.UploadedFile);
                }
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
            if (UploadedFileExists(file))
            {
                if (IsImage(file)) {
                    if (!ImageRefExists) //No existing ImageReference
                    {
                        viewModel.ImageReference = new ImageHandler()
                            .SaveInitialImageAndGetReference(viewModel.UploadedFile);
                    }
                    else //Existing ImageReference
                    {
                        viewModel.ImageReference = new ImageHandler()
                            .SaveEditedImageAndGetReference(viewModel.ImageReference, 
                            viewModel.UploadedFile);
                    }
                }
            }
            Recipe recipe = new Recipe(viewModel);
            repository.Update(recipe);
            return await repository.SaveChangesAsync();
        }

        public bool UploadedFileExists(HttpPostedFileBase file)
        {
            return (!(file == null) && file.ContentLength > 0);
        }

        public bool IsImage(HttpPostedFileBase file)
        {
            string ContentType = file.ContentType.ToLower();
            return (ContentType == "image/jpg" ||
                     ContentType == "image/jpeg" ||
                     ContentType == "image/pjpeg" ||
                     ContentType == "image/gif" ||
                     ContentType == "image/x-png" ||
                     ContentType == "image/png");
        }
    }
}