using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{

    public class RecipeRepository: IRecipeRepository
    {
        private readonly DataContext _context;

        public RecipeRepository(DataContext context)
        {
            _context = context;
        }

        public Recipe GetRecipe(int id)
        {
           return _context.Recipes.Where(p=>p.RecipeID==id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(p=>p.RecipeID).ToList();
        }

        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(p => p.RecipeID == recipeId);
        }
    }
}
