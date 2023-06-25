using Model.MODEL;

namespace API.Interfaces
{
    public interface IOpinionServices
    {
        bool CreateOpinion(Opinion opinion);
        bool DeleteOpinion(Opinion opinion);
        Opinion GetOpinion(int id);
        ICollection<Opinion> GetOpinions();
        ICollection<Opinion> GetOpinionsForRecipe(int id);
        ICollection<Opinion> GetOpinionsForUser(int id);
        bool OpinionExists(int id);
        bool OpinionExistsOnRecipe(int id);
        bool OpinionExistsOnUser(int id);
        bool Save();
        bool UpdateOpinion(Opinion opinion);
    }
}