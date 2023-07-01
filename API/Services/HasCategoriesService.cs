using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.MODEL;

namespace API.Services
{
    public class HasCategoriesService : IHasCategoriesService
    {
        private readonly DataContext _context;

        public HasCategoriesService(DataContext context)
        {
            _context = context;
        }

        public bool CreateHasCategory(HasCategory hasCategory)
        {
            _context.Add(hasCategory);
            return true;
        }

        public ICollection<HasCategory> GetHasCategoriesByTag(int tagID)
        {
            return _context.HasCategories.Where(hs => hs.TagID == tagID).ToList();
        }

        public ICollection<HasCategory> GetHasCategoriesByRecipe(int recipeID)
        {
            return _context.HasCategories.Where(hs => hs.RecipeID == recipeID).ToList();
        }

        public bool DeleteHasCategoryByTag(int tagID)
        {
            var hasCategoryToDelete = GetHasCategoriesByTag(tagID);
            _context.Remove(hasCategoryToDelete);
            return true;
        }

        public bool DeleteHasCategoryByRecipe(int recipeID)
        {
            var hasCategoryToDelete = GetHasCategoriesByRecipe(recipeID);
            _context.Remove(hasCategoryToDelete);
            return true;
        }

        public HasCategory GetHasCategoryForRecipe(int id)
        {
            return _context.HasCategories.Where(hc => hc.RecipeID == id).FirstOrDefault();
        }
        public HasCategory GetHasCategoryForTag(int id)
        {
            return _context.HasCategories.Where(hc => hc.TagID == id).FirstOrDefault();
        }

        public bool HasCategoryExistsByRecipe(int id)
        {
            return _context.HasCategories.Any(hc => hc.RecipeID == id);
        }
        public bool HasCategoryExistsByTag(int id)
        {
            return _context.HasCategories.Any(hc => hc.TagID == id);
        }

        public bool DelateHasCategory(HasCategory hasCategory)
        {
            _context.Remove(hasCategory);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public HasCategory GetHasCategoryByRecipeAndTag(int recipeId, int tagId)
        {
            return _context.HasCategories.FirstOrDefault(p => p.RecipeID == recipeId && p.TagID == tagId);
        }

        public bool HasCategorytByRecipeAndTagExists(int recipeId, int tagId)
        {
            return _context.HasCategories.Any(p => p.RecipeID == recipeId && p.TagID == tagId);

        }
    }
}
