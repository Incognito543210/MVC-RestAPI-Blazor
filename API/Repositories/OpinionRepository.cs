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
    }
}
