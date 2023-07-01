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
           await LogRequest(wynik);
        }

        public async Task AddIngredientsToRecipeAsync(List<IngridientDto> ingredients, string recipeTitle)
        {
            var wynik = await _httpClient.PostAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + recipeTitle, ingredients);
            await LogRequest(wynik);
        }

        private async Task LogRequest(HttpResponseMessage wynik)
        {
            var str = await wynik.RequestMessage!.Content!.ReadAsStringAsync();
            var url = wynik.RequestMessage.RequestUri;
            _log.LogWarning(url + " przerwa " + str);
        }

        public async Task AddTagsToRecipeAsync(List<TagDto> ingredients, string recipeTitle)
        {
            //var wynik = await _httpClient.PostAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + recipeTitle, ingredients);
            //await LoqRequest(wynik);
        }

    }
}
