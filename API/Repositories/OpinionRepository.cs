using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{
    public class OpinionRepository : IOpinionRepository
    {
        private readonly DataContext _context;

        public OpinionRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Opinion> GetOpinions() 
        {
            return _context.Opinions.OrderBy(o=>o.OpinionID).ToList();
        }

        public bool OpinionExistsOnRecipe(int id)
        {
            return _context.Opinions.Any(o => o.RecipeID == id);
        }

        public bool OpinionExistsOnUser(int id)
        {
            return _context.Opinions.Any(o => o.UserID == id);
        }

        public ICollection<Opinion> GetOpinionsForRecipe(int id)
        {
            return _context.Opinions.Where(o => o.RecipeID == id).ToList();
        }

        public ICollection<Opinion> GetOpinionsForUser(int id)
        {
            return _context.Opinions.Where(o => o.UserID == id).ToList();
        }

        public bool CreateOpinion(Opinion opinion)
        {
            _context.Add(opinion);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
