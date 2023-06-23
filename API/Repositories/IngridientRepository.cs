using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private DataContext _context;  
        public IngridientRepository(DataContext context)
        {
            _context= context;  
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
    }
}
