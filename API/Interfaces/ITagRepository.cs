using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTags();
        Tag GetTag(int id);
        bool TagExists(int tag);
    }
}
