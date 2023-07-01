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

        public bool HasIngridientByRecipeAndIngridientExists(int recipeId, int ingridientId)
        {
             return _context.HasIngridients.Any(p => p.RecipeID == recipeId && p.IngridientID == ingridientId);

        }

        public ICollection<HasIngridient> GetHasIngridientsByRecipe(int recipeID)
        {
            return _context.HasIngridients.Where(hs => hs.RecipeID == recipeID).ToList();
        }

        public HasIngridient GetHasIngridientByRecipeAndIngridient(int recipeId, int ingridientId)
        {
            return _context.HasIngridients.FirstOrDefault(p => p.RecipeID == recipeId && p.IngridientID == ingridientId);
          
        }

        public bool DelateHasIngridient(HasIngridient hasIngridient)
        {
            _context.Remove(hasIngridient);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteIngridientsForRecipe(List<HasIngridient> hasIngridients)
        {
            _context.RemoveRange(hasIngridients);
            return Save();
        }
    }
}
