using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Model.MODEL;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
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

        public async Task<List<RecipeDto>> GetRecipesByUserAsync(int id)
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<List<RecipeDto>>("/api/Recipe/ByUser/" + id))!;
            }
            catch (Exception ex)
            {
                return new();
            }            
        }

        public async Task<List<RecipeDto>> GetRecipesByTagsAsync(ICollection<TagDto> tags)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Recipe/ByTags", tags);
                return (await response.Content.ReadFromJsonAsync<List<RecipeDto>>())!;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public async Task<List<TagDto>> GetTagsListAsync(int id)
        {
            return (await _httpClient.GetFromJsonAsync<List<TagDto>>("/api/Recipe/tags/" + id))!;
        }
        public async Task<HasIngridientDto> GetIngredientAmountAsync(int recipeId, int ingredientId)
        {
            var wynik = await _httpClient.GetFromJsonAsync<HasIngridientDto>("/api/HasIngridient/" + recipeId + "," + ingredientId)!;
            return wynik!;
        }

        public async Task<string> AddRecipeAsync(int userId, RecipeDto recipe)
        {
            var wynik = await _httpClient.PostAsJsonAsync<RecipeDto>("/api/Recipe/" + userId, recipe);
            await LogRequest(wynik);

            if (!wynik.IsSuccessStatusCode)
            {
                var errorMessage = await wynik.Content.ReadAsStringAsync();
                return errorMessage;
            }
            else
                return null!;
            //await LogRequest(wynik);
            //var odp = wynik.StatusCode.ToString();
            //var odp2 = wynik.Content.ToString();
        }

        public async Task<string> AddIngredientsToRecipeAsync(List<IngridientDto> ingredients, string recipeTitle)
        {
            var wynik = await _httpClient.PostAsJsonAsync<List<IngridientDto>>("api/Ingridient/" + recipeTitle, ingredients);
            await LogRequest(wynik);

            if (!wynik.IsSuccessStatusCode)
            {
                var errorMessage = await wynik.Content.ReadAsStringAsync();
                return errorMessage;
            }
            else
                return null!;
        }

        public async Task<string> AddTagsToRecipeAsync(List<TagDto> tags, string recipeTitle)
        {
            var wynik = await _httpClient.PostAsJsonAsync<List<TagDto>>("api/Tag/" + recipeTitle, tags);
            await LogRequest(wynik);

            if (!wynik.IsSuccessStatusCode)
            {
                var errorMessage = await wynik.Content.ReadAsStringAsync();
                return errorMessage;
            }
            else
                return null!;
        }

        public async Task UpdateRecipeAsync(RecipeDto recipe, int id)
        {
            var wynik = await _httpClient.PutAsJsonAsync<RecipeDto>("api/Recipe/" + id, recipe);
            await LogRequest(wynik);
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
            //var wynik = await _httpClient.DeleteAsync("");
        }

//        private async Task LogRequest(HttpResponseMessage wynik)
//        {
//            var str = await wynik.RequestMessage!.Content!.ReadAsStringAsync();
//            var url = wynik.RequestMessage.RequestUri;
//#pragma warning disable CA2254 // Szablon powinien być wyrażeniem statycznym
//            _log.LogWarning(url + " " + str);
//#pragma warning restore CA2254 // Szablon powinien być wyrażeniem statycznym
//        }

        private async Task LogRequest(HttpResponseMessage response)
        {
            var url = response.RequestMessage!.RequestUri;

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                _log.LogWarning(url + " Error: " + errorMessage);
            }
            else
            {
                var str = await response.RequestMessage!.Content!.ReadAsStringAsync();
                _log.LogWarning(url + " " + str);
            }
        }

    }
}
