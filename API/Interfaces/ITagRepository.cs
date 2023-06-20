using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagRepository
    {
        public ICollection<Tag> GetTags();
    }
}
