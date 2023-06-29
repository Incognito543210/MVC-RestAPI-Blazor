using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Services
{
    public class OpinionServices : IOpinionServices
    {
        private readonly DataContext _context;
        private readonly IOpinionRepository _opinionRepository;

        public OpinionServices(DataContext context, IOpinionRepository opinionRepository)
        {
            _context = context;
            _opinionRepository = opinionRepository;
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

        public bool UpdateOpinion(Opinion opinion)
        {
            _context.Update(opinion);
            return Save();
        }

        public bool DeleteOpinion(Opinion opinion)
        {
            _context.Remove(opinion);
            return Save();
        }

        public ICollection<Opinion> GetOpinions()
        {
            return _opinionRepository.GetOpinions();
        }

        public Opinion GetOpinion(int id)
        {
            return _opinionRepository.GetOpinion(id);
        }
        public bool OpinionExistsOnRecipe(int id)
        {
            return _opinionRepository.OpinionExistsOnRecipe(id);
        }
        public bool OpinionExistsOnUser(int id)
        {
            return _opinionRepository.OpinionExistsOnUser(id);
        }
        public bool OpinionExists(int id)
        {
            return _opinionRepository.OpinionExists(id);
        }
        public ICollection<Opinion> GetOpinionsForRecipe(int id)
        {
            return _opinionRepository.GetOpinionsForRecipe(id);
        }
        public ICollection<Opinion> GetOpinionsForUser(int id)
        {
            return _opinionRepository.GetOpinionsForUser(id);
        }
    }
}
