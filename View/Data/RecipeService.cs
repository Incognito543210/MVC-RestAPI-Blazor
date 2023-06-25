using Model;
using Model.DTO;

namespace View.Data
{
    public class RecipeService
    {
        private List<RecipeDto> recipes = new();

        public Task AddRecipeAsync(RecipeDto recipe)
        {
            recipes.Add(recipe);
            return Task.CompletedTask;
        }

        public async Task<List<RecipeDto>> GetRecipesAsync()
        {
            return await Task.FromResult(recipes);
        }

        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RecipeDto>> GetRecipeListAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RecipeDto>>("/api/Recipe/AllRecipes");
        }

        public async Task<RecipeDto> GetRecipeAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<RecipeDto>("/api/Recipe/" + id);
        }

        public async Task<List<IngridientDto>> GetIngridientsListAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<IngridientDto>>("/api/Recipe/ingridients/" + id);
        }

    }
}
