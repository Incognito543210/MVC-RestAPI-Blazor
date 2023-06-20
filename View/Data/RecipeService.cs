using API.Models;

namespace View.Data
{
    public class RecipeService
    {
        private List<Recipe> recipes = new();

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }
        public List<Recipe> GetRecipes() {  return recipes; }
    }
}
