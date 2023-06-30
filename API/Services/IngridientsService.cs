using API.Interfaces;
using DAL;
using Model.MODEL;
using SQLitePCL;

namespace API.Services
{
    public class IngridientsService : IIngridientsService
    {
        private DataContext _context;
    
       
        public IngridientsService(DataContext context)
        {
            _context = context;
        
        }

        public bool CreateIngridient(Ingridient ingridient, string recipeName, string amount)
        {
            var ingridientCheckExists = GetIngridients().Where(c => c.Name.Trim().ToUpper() == ingridient.Name.TrimEnd().ToUpper()).FirstOrDefault();
            var recipe = _context.Recipes.Where(u => u.Title.Trim().ToLower() == recipeName.Trim().ToLower()).FirstOrDefault();

            if (ingridientCheckExists != null)
            {
                var hasIngridient = new HasIngridient()
                {
                    Ingridient = ingridientCheckExists,
                    IngridientID = ingridientCheckExists.IngridientID,
                    RecipeID = recipe.RecipeID,
                    Recipe = recipe,
                    Amount = amount,

                };
                _context.Add(hasIngridient);
            }
            else
            {
                var hasIngridient = new HasIngridient()
                {
                    Ingridient = ingridient,
                    IngridientID = ingridient.IngridientID,
                    RecipeID = recipe.RecipeID,
                    Recipe = recipe,
                    Amount = amount,

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
            return _context.Ingridients.OrderBy(c => c.IngridientID).ToList();
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
