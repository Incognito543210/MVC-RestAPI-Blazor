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

        public Opinion GetOpinion(int id)
        {
            return _context.Opinions.Where(o => o.OpinionID == id).FirstOrDefault();
        }

        public bool OpinionExistsOnRecipe(int id)
        {
            return _context.Opinions.Any(o => o.RecipeID == id);
        }

        public bool OpinionExistsOnUser(int id)
        {
            return _context.Opinions.Any(o => o.UserID == id);
        }

        public bool OpinionExists(int id)
        {
            return _context.Opinions.Any(o=>o.OpinionID == id);
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
            return true;
        }

        public bool UpdateOpinion(Opinion opinion)
        {
            _context.Update(opinion);
            return true;
        }

        public bool DeleteOpinion(Opinion opinion)
        {
            _context.Remove(opinion);
            return true;
        }
    }
}
