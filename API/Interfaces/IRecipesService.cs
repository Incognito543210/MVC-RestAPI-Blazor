using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.MODEL;

namespace API.Interfaces
{
    public interface IRecipesService 
    {

        ICollection<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        bool RecipeExists(int recipeId);

        bool IngredientsExistsOnRecipe(int id);
        
        ICollection<Ingridient> GetIngridientsByRecipe(int id);

        bool RecipeExistsOnUser(int id);

        ICollection<Recipe> GetRecipesbyUser(int id);

        bool CreateRecipe(Recipe recipe);

        bool Save();

        bool UpdateRecipe(Recipe recipe);

         ICollection<Tag> GetTagsByRecipe(int id);
         bool TagsExistsOnRecipe(int id);

        public bool DeleteRecipe(Recipe recipe);
       



    }
}
