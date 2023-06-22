using Model.MODEL;

namespace API.Interfaces
{
    public interface IOpinionRepository
    {
        ICollection<Opinion> GetOpinions();
        Opinion GetOpinion(int id);
        bool OpinionExistsOnRecipe(int id);
        bool OpinionExistsOnUser(int id);
        bool OpinionExists(int id);
        ICollection<Opinion> GetOpinionsForRecipe(int id);
        ICollection<Opinion> GetOpinionsForUser(int id);
        bool CreateOpinion(Opinion opinion);
        bool UpdateOpinion(Opinion opinion);
        bool DeleteOpinion(Opinion opinion);
        bool Save();
    }
}
