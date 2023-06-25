using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagServices
    {
        ICollection<Tag> GetAllTags();
        Tag GetByID(int id);
    }
}