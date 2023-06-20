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

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(p=>p.RecipeID).ToList();
        }
    }
}
