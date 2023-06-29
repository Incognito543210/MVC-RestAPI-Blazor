using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.MODEL;

namespace API.Services
{
    public class RecipesService: IRecipesService
    {
        private DataContext _context;
        private IIngridientRepository _ingridientrepository;
        private IRecipeRepository _recipeRepository;

        public RecipesService(DataContext context, IRecipeRepository recipeRepository, IIngridientRepository ingridientRepository)
        {
            _context=context;
            _recipeRepository=recipeRepository;
            _ingridientrepository = ingridientRepository;     
        }

     /*   public bool CreateRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            _recipeRepository.Save();
            foreach (var ingrident in recipe.HasIngridients)
            {
                _ingridientrepository.CreateIngridient(ingrident, recipe.RecipeID);
            }
        }*/
    }
}
