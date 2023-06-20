using Model.MODEL;

namespace API.Interfaces
{
    public interface IOpinionRepository
    {
        public ICollection<Opinion> GetOpinions();
    }
}
