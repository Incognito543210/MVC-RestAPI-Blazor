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

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.OrderBy(o => o.TagID).ToList();
        }
    }
}
