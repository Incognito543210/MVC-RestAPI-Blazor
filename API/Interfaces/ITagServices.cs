using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagServices
    {
        bool CreateTag(Tag tag);
        bool DeleteTag(int id);
        ICollection<Tag> GetAllTags();
        bool Save();
        bool TagExists(Tag tag);
        bool TagExists(int id);
        bool UpdateTag(Tag tag);
    }
}