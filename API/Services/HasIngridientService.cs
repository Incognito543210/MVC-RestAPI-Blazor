using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Services
{
    public class HasIngridientService : IHasIngridientService
    {
        private DataContext _context;

        public HasIngridientService(DataContext context)
        {
            _context=context;
        }

        public bool AmountByRecipeAndIngridientExists(int recipeId, int ingridientId)
        {
            var hasIngridient = _context.HasIngridients.Any(p => p.RecipeID == recipeId && p.IngridientID == ingridientId);

            if(hasIngridient != null)
            {
                return true;
            }

            return false;

        }

        public HasIngridient getAmountByRecipeAndIngridient(int recipeId, int ingridientId)
        {
            var hasIngridient = _context.HasIngridients.FirstOrDefault(p => p.RecipeID == recipeId && p.IngridientID == ingridientId);

            return hasIngridient;
          
        }
    }
}
