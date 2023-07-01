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

        public bool AddTagsForRecipe(Tag tag, string recipeName)
        {
            var recipe = _context.Recipes.Where(u => u.Title.Trim().ToLower() == recipeName.Trim().ToLower()).FirstOrDefault();
            var tagContext = GetTags().Where(c => c.Name.Trim().ToUpper() == tag.Name.Trim().ToUpper()).FirstOrDefault();

            var hasCategory = new HasCategory()
            {
                Tag = tagContext,
                TagID = tagContext.TagID,
                RecipeID = recipe.RecipeID,
                Recipe = recipe,
                

            };
            _context.Add(hasCategory);

            return Save();
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

        public bool UpdateTagsForRecipe(Tag tag, int recipeId)
        {
            var recipe = _context.Recipes.Where(u => u.RecipeID == recipeId).FirstOrDefault();
            var tagContext = GetTags().Where(c => c.Name.Trim().ToUpper() == tag.Name.Trim().ToUpper()).FirstOrDefault();
            var HasCategoryExists = _context.HasCategories.Where(p => p.RecipeID == recipeId && (p.Tag.Name.Trim().ToUpper() == tag.Name.Trim().ToUpper())).FirstOrDefault();

            if (HasCategoryExists != null)
            {
                return true;
            }
            else
            {
                var hasCategory = new HasCategory()
                {
                    Tag = tagContext,
                    TagID = tagContext.TagID,
                    RecipeID = recipe.RecipeID,
                    Recipe = recipe,


                };
                _context.Add(hasCategory);

                return Save();
            }
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
