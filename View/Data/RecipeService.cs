using Model;
using Model.DTO;
using Model.MODEL;
using System.Diagnostics;
using View.Pages;

namespace View.Data
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private const int _userId = 1;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RecipeDto>> GetRecipeListAsync()
        {
            return (await _httpClient.GetFromJsonAsync<List<RecipeDto>>("/api/Recipe/AllRecipes"))!;
        }

        public async Task<RecipeDto> GetRecipeAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<RecipeDto>("/api/Recipe/" + id))!;
        }

        public async Task<List<IngridientDto>> GetIngredientsListAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<List<IngridientDto>>("/api/Recipe/ingridients/" + id))!;
        }

        public async Task AddRecipeAsync(RecipeDto recipe)
        {
            var wynik = await _httpClient.PostAsJsonAsync<RecipeDto>("/api/Recipe?userID=" + _userId, recipe);
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.WriteLine(wynik.RequestMessage);
        }

        public async Task AddIngredientsAsync(List<IngridientDto> ingredients, string recipeTitle)
        {
            var wynik = await _httpClient.PostAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + recipeTitle, ingredients);
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.WriteLine(wynik.RequestMessage);
        }

    }
}
