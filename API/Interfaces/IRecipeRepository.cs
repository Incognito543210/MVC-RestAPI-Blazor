using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.MODEL;

namespace API.Interfaces
{
    public interface IRecipeRepository 
    {

        ICollection<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        bool RecipeExists(int recipeId);

        bool IngredientsExistsOnRecipe(int id);
        
        ICollection<Ingridient> GetIngridientsByRecipe(int id);

        bool RecipeExistsOnUser(int id);

        ICollection<Recipe> GetRecipesbyUser(int id);

    }
}
