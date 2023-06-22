using Model.MODEL;

namespace API.Interfaces
{
    public interface IOpinionRepository
    {
        public ICollection<Opinion> GetOpinions();
        public bool OpinionExistsOnRecipe(int id);
        public bool OpinionExistsOnUser(int id);
        public ICollection<Opinion> GetOpinionsForRecipe(int id);
        public ICollection<Opinion> GetOpinionsForUser(int id);
        bool CreateOpinion(Opinion opinion);
        bool Save();
    }
}
