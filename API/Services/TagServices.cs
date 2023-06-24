using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.MODEL;

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

        public bool CreateTag(Tag tag)
        {
            bool result;

            result = _tagRepository.CreateTag(tag);
            
            result &= Save();

            return result;
        }

        public bool DeleteTag(int id)
        {
            bool resultHasCategory=true;
            bool resultTag;
            bool resultSave;
            if (!_tagRepository.TagExists(id))
                return false;

            //resultHasCategory = !_hasCategoryRepository.HasCategoryExistsByTag(tag.TagID) || _hasCategoryRepository.DeleteHasCategoryByTag(tag.TagID);

            var tagToDelete = _tagRepository.GetTag(id);

            resultTag = _tagRepository.DeleteTag(tagToDelete);

            resultSave = Save();

            return resultHasCategory && resultTag && resultSave;
        }

        public ICollection<Tag> GetAllTags()
        {
            var tags = _tagRepository.GetTags();
            return tags;
        }

        public bool TagExists(Tag tag)
        {
            bool result = _tagRepository.TagExists(tag.TagID);
            return result;
        }

        public bool TagExists(int id)
        {
            bool result = _tagRepository.TagExists(id);
            return result;
        }

        public bool UpdateTag(Tag tag)
        { 
            bool result = _tagRepository.UpdateTag(tag);

            result &= Save();
            
            return result;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
