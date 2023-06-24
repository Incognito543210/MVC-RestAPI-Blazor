using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTag(Tag tag)
        {
            _context.Add(tag);
            return true;
        }

        public bool DeleteTag(Tag tag)
        {
            _context.Remove(tag);
            return true;
        }

        public Tag GetTag(int id)
        {
            return _context.Tags.Where(t => t.TagID == id).FirstOrDefault();
        }

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.OrderBy(o => o.TagID).ToList();
        }

        public bool TagExists(int id)
        {
            return _context.Tags.Any(t=>t.TagID == id);
        }

        public bool UpdateTag(Tag tag)
        {
            _context.Update(tag);
            return true;
        }
    }
}
