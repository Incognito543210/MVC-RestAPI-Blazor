using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.MODEL;
using System.Reflection.Metadata.Ecma335;

namespace API.Services
{
    public class TagServices : ITagServices
    {
        private readonly DataContext _dataContext;
        private readonly IHasCategoryRepository _hasCategoryRepository;
        private readonly ITagRepository _tagRepository;

        public TagServices(DataContext dataContext, ITagRepository tagRepository, IHasCategoryRepository hasCategoryRepository)
        {
            _dataContext = dataContext;
            _tagRepository = tagRepository;
            _hasCategoryRepository = hasCategoryRepository;
        }

        public Tag GetByID(int id)
        {
            var tag = _tagRepository.GetTag(id);
            return tag;
        }

        public ICollection<Tag> GetAllTags()
        {
            var tags = _tagRepository.GetTags();
            return tags;
        }
    }
}
