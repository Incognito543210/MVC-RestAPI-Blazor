using API.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.MODEL;

namespace API.Services
{

    public class RecipesService : IRecipesService
    {
        private DataContext _context;
       

        public RecipesService(DataContext context)
        {
            _context = context;
        
        }

        public ICollection<Ingridient> GetIngridientsByRecipe(int id)
        {
            return _context.HasIngridients.Where(p => p.RecipeID == id).Select(c => c.Ingridient).ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(p => p.RecipeID == id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipesbyUser(int id)
        {
            return _context.Recipes.Where(p => p.UserID == id).ToList();
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(p => p.RecipeID).ToList();
        }

        public bool IngredientsExistsOnRecipe(int id)
        {
            return _context.HasIngridients.Any(p => p.RecipeID == id);
        }

        public bool TagsExistsOnRecipe(int id)
        {
            return _context.HasCategories.Any(p => p.RecipeID == id);
        }


        public ICollection<Tag> GetTagsByRecipe(int id)
        {
            return _context.HasCategories.Where(p => p.RecipeID == id).Select(c => c.Tag).ToList();
        }


        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(p => p.RecipeID == recipeId);
        }

        public bool RecipeExistsOnUser(int id)
        {
            return _context.Recipes.Any(p => p.UserID == id);
        }

        public bool CreateRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            return Save();
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }

        public ICollection<Recipe> GetRecipesByTags(ICollection<Tag> tags)
        {
            var recipeIdWithTags= _context.HasCategories.Where(mapping => tags.Contains(mapping.Tag))
            .GroupBy(mapping => mapping.RecipeID)
            .Where(group => group.Count() == tags.Count)
            .Select(group => group.Key)
            .ToList();

            var recipes = _context.Recipes .Where(recipe => recipeIdWithTags.Contains(recipe.RecipeID)).ToList();

            return recipes;
        }

        public bool GetRecipesByTagsExists(ICollection<Tag> tags)
        {
            var recipeIdWithTags = _context.HasCategories.Where(mapping => tags.Contains(mapping.Tag))
            .GroupBy(mapping => mapping.RecipeID)
            .Where(group => group.Count() == tags.Count)
            .Select(group => group.Key)
            .ToList();

            var recipes = _context.Recipes.Where(recipe => recipeIdWithTags.Contains(recipe.RecipeID)).ToList();

            if(recipes != null)
            {
                return true;
            }
            else
            { 
                return false; 
            };
        }
    }
}
