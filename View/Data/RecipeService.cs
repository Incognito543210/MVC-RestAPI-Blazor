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
        private readonly ILogger<RecipeService> _log;
        private const int _userId = 1;

        public RecipeService(HttpClient httpClient, ILogger<RecipeService> log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<List<RecipeDto>> GetRecipeListAsync()
        {
            return (await _httpClient.GetFromJsonAsync<List<RecipeDto>>("/api/Recipe/AllRecipes"))!;
        }

        public async Task<List<TagDto>> GetTagListAsync()
        {
            return (await _httpClient.GetFromJsonAsync<List<TagDto>>("/api/Tag"))!;
        }

        public async Task<RecipeAdapter> GetRecipeAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<RecipeAdapter>("/api/Recipe/" + id))!;
        }

        public async Task<List<IngridientDto>> GetIngredientsListAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<List<IngridientDto>>("/api/Recipe/ingridients/" + id))!;
        }

        public async Task AddRecipeAsync(RecipeDto recipe)
        {
            var wynik = await _httpClient.PostAsJsonAsync<RecipeDto>("/api/Recipe/" + _userId, recipe);
            await LogRequest(wynik);
        }

        public async Task AddIngredientsToRecipeAsync(List<IngridientDto> ingredients, string recipeTitle)
        {
            var wynik = await _httpClient.PostAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + recipeTitle, ingredients);
            await LogRequest(wynik);
        }

        public async Task AddTagsToRecipeAsync(List<TagDto> tags, string recipeTitle)
        {
            await _httpClient.PostAsJsonAsync<List<TagDto>>("api/Tag/" + recipeTitle, tags);
        }

        public async Task UpdateRecipeAsync(RecipeDto recipe, int id)
        {
            var wynik = await _httpClient.PutAsJsonAsync<RecipeDto>("api/Recipe/" + id, recipe);
        }

        public async Task UpdateIngredientsInRecipeAsync(List<IngridientDto> ingredients, int id)
        {
            var wynik = await _httpClient.PutAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + id, ingredients);
            await LogRequest(wynik);
        }

        public async Task UpdateTagsInRecipeAsync(List<TagDto> tags, int id)
        {
            var wynik = await _httpClient.PutAsJsonAsync<List<TagDto>>("api/Tag/" + id, tags);
            await LogRequest(wynik);
        }

        public async Task DeleteRecipeById(int id)
        {
            var wynik = await _httpClient.DeleteAsync("api/Recipe/" + id);
        }

        private async Task LogRequest(HttpResponseMessage wynik)
        {
            var str = await wynik.RequestMessage!.Content!.ReadAsStringAsync();
            var url = wynik.RequestMessage.RequestUri;
#pragma warning disable CA2254 // Szablon powinien być wyrażeniem statycznym
            _log.LogWarning(url + " " + str);
#pragma warning restore CA2254 // Szablon powinien być wyrażeniem statycznym
        }

    }
}
