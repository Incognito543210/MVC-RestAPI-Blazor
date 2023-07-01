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

        public HasIngridient getHasIngridientByRecipeAndIngridient(int recipeId, int ingridientId)
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
    }
}
