using Model.MODEL;

namespace API.Interfaces
{
    public interface IOpinionRepository
    {
        public ICollection<Opinion> GetOpinions();
        public Opinion GetOpinion(int id);
        public bool OpinionExistsOnRecipe(int id);
        public bool OpinionExistsOnUser(int id);
        public bool OpinionExists(int id);
        public ICollection<Opinion> GetOpinionsForRecipe(int id);
        public ICollection<Opinion> GetOpinionsForUser(int id);
        bool CreateOpinion(Opinion opinion);
        bool UpdateOpinion(Opinion opinion);
        bool DeleteOpinion(Opinion opinion);
        bool Save();
    }
}
