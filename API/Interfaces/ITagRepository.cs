using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagRepository
    {
        public ICollection<Tag> GetTags();
        public Tag GetTag(int id);
        public bool TagExists(int id);
        public bool CreateTag(Tag tag);
        public bool UpdateTag(Tag tag);
        public bool DeleteTag(Tag tag);
        bool Save();
    }
}
