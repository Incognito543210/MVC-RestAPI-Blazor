using API.Interfaces;
using DAL;
using Model.MODEL;
using SQLitePCL;

namespace API.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private DataContext _context;  
        public IngridientRepository(DataContext context)
        {
            _context= context;  
        }

        public bool CreateIngridient(Ingridient ingridient, int recipeId)
        {
            var ingridientCheckExists = GetIngridients().Where(c => c.name.Trim().ToUpper() == ingridient.name.TrimEnd().ToUpper()).FirstOrDefault();
            var recipe = _context.Recipes.Where(a=>a.RecipeID== recipeId).FirstOrDefault();

            if (ingridientCheckExists != null)
            {
                var hasIngridient = new HasIngridient()
                {
                    Ingridient = ingridientCheckExists,
                    IngridientID = ingridientCheckExists.IngridientID,
                    RecipeID = recipeId,
                    Recipe = recipe,

                };
                _context.Add(hasIngridient);
            }
            else {
                var hasIngridient = new HasIngridient()
                {
                    Ingridient = ingridient,
                    IngridientID = ingridient.IngridientID,
                    RecipeID = recipeId,
                    Recipe = recipe,

                };
                _context.Add(hasIngridient);
                _context.Add(ingridient);
            }
            return Save();
        }

        public Ingridient GetIngridient(int id)
        {
            return _context.Ingridients.Where(c => c.IngridientID == id).FirstOrDefault();
        }

        public ICollection<Ingridient> GetIngridients()
        {
            return _context.Ingridients.OrderBy(c=>c.IngridientID).ToList();
        }

      
        public bool IngridientExists(int id)
        {
            return _context.Ingridients.Any(c => c.IngridientID == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
