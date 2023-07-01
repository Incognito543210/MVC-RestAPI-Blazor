using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.MODEL;

namespace API.Repositories
{
    public class TagsService : ITagsService
    {
        private readonly DataContext _context;

        public TagsService(DataContext context)
        {
            _context = context;
        }

        public Tag GetTag(int id)
        {
            return _context.Tags.Where(t => t.TagID == id).FirstOrDefault();
        }

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.OrderBy(t => t.TagID).ToList();
        }

        public bool TagExists(int id)
        {
            return _context.Tags.Any(t=>t.TagID == id);
        }

      
    }
}
